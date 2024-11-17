using System.Linq;
using PixelWizards.DTO;
using PixelWizards.ScriptableObjects;
using PixelWizards.Shared.Base;
using UnityEngine;

namespace PixelWizards.Services
{
    public class MetadataService : MonoBehaviour
    {
        public static MetadataService Instance => ServiceLocator.Get<MetadataService>();
        
        [Header("Data Container")]
        [SerializeField]
        private GameData data;
        
        public void Init()
        {
            ParseMetadata();
        }

        private void ParseMetadata()
        {
            Debug.Log("Parsing Game Data");
        }

        /// <summary>
        /// Get the weapon config for the given item name
        /// </summary>
        /// <param name="weaponName">name of the weapon, should match weapon metadata table</param>
        /// <returns>Weapon config for the given weapon, if not found, returns null</returns>
        public WeaponConfig GetWeaponConfig(string weaponName)
        {
            var weaponConfig = data.itemEntries.First(t => t.name == weaponName);
            if (weaponConfig == null)
            {
                Debug.Log("No Item: " + weaponName + " in Data Container?");
            }

            return weaponConfig;
        }
    }
}