using UnityEngine;

// DTO == Data Transformation Object ie convert Unity references into metadata
namespace PixelWizards.DTO
{
    [System.Serializable]
    public class WeaponConfig
    {
        public string name;         // the ID for this item
        public GameObject prefab;   // the game object that is associated with it
    }
}