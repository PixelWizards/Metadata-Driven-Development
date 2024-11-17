using PixelWizards.DTO;
using UnityEngine.Serialization;

namespace PixelWizards.Models
{
    public enum WeaponType
    {
        AssaultRifle,
        Pistol,
        Revolver,
        Shotgun,
        SniperRifle,
        SMG,
    }

    public enum AmmoType
    {
        Rifle,
        Pistol,
        Revolver,
        Shotgun,
    }
    
    [System.Serializable]
    public class WeaponModel
    {
        /// <summary>
        /// these are all parsed from GoogleSheets
        /// </summary>
        public string name;
        public string displayName;
        public int maxAmmo;
        public int magSize;
        public float power;

        /// <summary>
        /// we parse these from the string on google sheets into the enum in the code
        /// </summary>
        public WeaponType type;
        public AmmoType ammoType;
        
        /// <summary>
        /// this is loaded from game data scriptable object after we load the metadata
        /// </summary>
        public WeaponConfig config;
    } 
}
