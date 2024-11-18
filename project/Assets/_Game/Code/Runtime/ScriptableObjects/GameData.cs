using System.Collections.Generic;
using PixelWizards.DTO;
using UnityEngine;

namespace PixelWizards.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Metadata/Create New GameData", order = 1)]
    public class GameData : ScriptableObject
    {
        [Header("Weapon Configs")]
        public List<WeaponConfig> weaponEntries = new();

        public void AddItem(WeaponConfig weaponConfig)
        {
            weaponEntries.Add(weaponConfig);
        }
    }
}