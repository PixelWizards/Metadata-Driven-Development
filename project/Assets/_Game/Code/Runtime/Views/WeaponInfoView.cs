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
            sb.AppendLine("Cost : " + entry.cost);
            sb.AppendLine("Level : " + entry.level);
            
            itemInfoLabel.text = sb.ToString();
        }
    }
}