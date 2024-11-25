﻿using System;
using System.Collections.Generic;
using System.Linq;
using BansheeGz.BGDatabase;
using PixelWizards.Shared.Base;
using PixelWizards.Interfaces.Interfaces;
using PixelWizards.Models;
using UnityEngine;

namespace PixelWizards.Services
{
    public class WeaponMetadataService : MonoBehaviour, IMetadataService<WeaponModel>
    {
        public static WeaponMetadataService Instance => ServiceLocator.Get<WeaponMetadataService>();

        [SerializeField]
        private string weaponTable = "Weapons";
        
        private Dictionary<string, WeaponModel> weapons = new();
        
        public void Init()
        {
            ParseMetadata();
        }

        public WeaponModel Get(string id)
        {
            if (weapons.TryGetValue(id, out var item))
            {
                return item;
            }

            Debug.Log("No Item : " + id);
            return null;
        }

        public List<WeaponModel> GetAll()
        {
            return weapons.Select(entry => entry.Value).ToList();
        }
        
        private void ParseMetadata()
        {
            Debug.Log("Parsing weapon metadata");
            weapons.Clear();
            
            // get a reference to the table
            var meta = BGRepo.I[weaponTable];

            // how many rows do we have in this table?
            var count = meta.CountEntities;

            for (var i = 0; i < count; i++)
            {
                var attrRow = meta.GetEntity(i);
                
                // read in the data from google sheets
                var entry = new WeaponModel
                {
                    name = attrRow.Get<string>("name"),
                    displayName = attrRow.Get<string>("displayName"),
                    maxAmmo = attrRow.Get<int>("maxAmmo"),
                    magSize = attrRow.Get<int>("magSize"),
                };
                
                // parse the Weapon Type from GoogleSheets
                var type = attrRow.Get<string>("type");
                if (Enum.TryParse(typeof(WeaponType), type, false, out var output))
                {
                    entry.type = (WeaponType)output;    
                }
                else
                {
                    Debug.Log("Invalid Weapon type? " + type);
                }
                
                // parse the AmmoType from GoogleSheets
                var ammoType = attrRow.Get<string>("ammoType");
                if (Enum.TryParse(typeof(AmmoType), ammoType, false, out var ammoTypeOutput))
                {
                    entry.ammoType = (AmmoType)ammoTypeOutput;    
                }
                else
                {
                    Debug.Log("Invalid Ammo type? " + ammoType);
                }
                
                // get the item config for this item
                entry.config = MetadataService.Instance.GetWeaponConfig(entry.name);
                if (entry.config == null)
                {
                    Debug.Log("Weapon: " + entry.name + " not configured correctly, missing entry in GameData?");
                }

                weapons.Add(entry.name, entry);
            }

            Debug.Log($"Parse Weapon Metadata COMPLETE - parsed {weapons.Count} entries...");
        }
    }
}