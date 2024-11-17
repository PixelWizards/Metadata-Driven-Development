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
            Debug.Log("Parsing metadata");
        }

        /// <summary>
        /// Get the item config for the given item name
        /// </summary>
        /// <param name="itemName">name of the item, should match item metadata table</param>
        /// <returns>Item config for the given item, if not found, returns null</returns>
        public WeaponConfig GetItemConfig(string itemName)
        {
            var itemConfig = data.itemEntries.First(t => t.name == itemName);
            if (itemConfig == null)
            {
                Debug.Log("No Item: " + itemName + " in Data Container?");
            }

            return itemConfig;
        }
    }
}