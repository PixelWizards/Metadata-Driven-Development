using System.Collections.Generic;
using PixelWizards.DTO;
using UnityEngine;

namespace PixelWizards.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Cloudhead/Create New GameData", order = 1)]
    public class GameData : ScriptableObject
    {
        [Header("Item Configs")]
        public List<WeaponConfig> itemEntries = new();

        public void AddItem(WeaponConfig weaponConfig)
        {
            itemEntries.Add(weaponConfig);
        }
    }
}