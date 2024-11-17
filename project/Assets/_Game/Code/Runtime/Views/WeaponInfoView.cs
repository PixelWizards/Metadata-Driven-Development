using System.Text;
using PixelWizards.Interfaces.Interfaces;
using PixelWizards.Models;
using TMPro;
using UnityEngine;

namespace PixelWizards.Views
{
    public class WeaponInfoView : MonoBehaviour, IEntryView<WeaponModel>
    {
        [SerializeField]
        private TextMeshProUGUI itemInfoLabel;
        
        public void Init(WeaponModel entry)
        {
            // we'll just output this items info for now
            var sb = new StringBuilder();
            sb.AppendLine("Name: " + entry.displayName);
            sb.AppendLine("Type : " + entry.type);
            sb.AppendLine("Mag Size : " + entry.magSize);
            sb.AppendLine("Max Ammo : " + entry.maxAmmo);
            sb.AppendLine("Ammo Type: " + entry.ammoType);
            
            itemInfoLabel.text = sb.ToString();
        }
    }
}