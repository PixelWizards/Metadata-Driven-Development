using System;
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
        private string itemConfigTable = "Items";
        
        private Dictionary<string, WeaponModel> items = new();
        
        public void Init()
        {
            ParseMetadata();
        }

        public WeaponModel Get(string id)
        {
            if (items.TryGetValue(id, out var item))
            {
                return item;
            }

            Debug.Log("No Item : " + id);
            return null;
        }

        public List<WeaponModel> GetAll()
        {
            return items.Select(entry => entry.Value).ToList();
        }
        
        private void ParseMetadata()
        {
            Debug.Log("Parsing metadata");
            items.Clear();
            
            // get a reference to the table
            var meta = BGRepo.I[itemConfigTable];

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
                    level = attrRow.Get<int>("level"),
                    cost = attrRow.Get<int>("cost"),
                    power = attrRow.Get<float>("power"),
                };
                
                // parse the ItemType from GoogleSheets
                var type = attrRow.Get<string>("type");
                if (Enum.TryParse(typeof(ItemType), type, out var output))
                {
                    entry.type = (ItemType)output;    
                }
                else
                {
                    Debug.Log("Invalid Item type?" + type);
                }
                

                // get the item config for this item
                entry.config = MetadataService.Instance.GetItemConfig(entry.name);
                if (entry.config == null)
                {
                    Debug.Log("Item: " + entry.name + " not configured correctly, missing entry in GameData?");
                }

                items.Add(entry.name, entry);
            }

            Debug.Log($"Parse Item Metadata COMPLETE - parsed {items.Count} entries...");
        }
    }
}