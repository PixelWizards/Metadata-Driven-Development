using PixelWizards.DTO;
using UnityEngine.Serialization;

namespace PixelWizards.Models
{
    public enum ItemType
    {
        Equippable,
        Power,
        Ammo,
        Interactive,
        Prop
    }
    
    [System.Serializable]
    public class WeaponModel
    {
        /// <summary>
        /// these are all parsed from GoogleSheets
        /// </summary>
        public string name;
        public string displayName;
        public int level;
        public int cost;
        public float power;

        /// <summary>
        /// we parse this from the string on google sheets into the enum in the code
        /// </summary>
        public ItemType type;
        
        /// <summary>
        /// this is loaded from game data scriptable object after we load the metadata
        /// </summary>
        public WeaponConfig config;
    } 
}
