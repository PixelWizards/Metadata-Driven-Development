using PixelWizards.Interfaces.Interfaces;
using PixelWizards.Models;
using PixelWizards.Views;
using UnityEngine;

namespace PixelWizards.Controllers
{
    public class WeaponMetadataController : MonoBehaviour, IController<WeaponModel>
    {
        [SerializeField]
        private WeaponInfoView view;
        
        [SerializeField]
        private WeaponModel thisWeapon;

        public void Init(WeaponModel weapon)
        {
            thisWeapon = weapon;

            // spawn the item UI
            view = Instantiate(Resources.Load<WeaponInfoView>("UIPanels/WeaponInfoCanvas"), transform);
            view.transform.localPosition = new Vector3(0, 1.5f, 0);
            
            // and initialize it
            view.Init(weapon);
        }
    }    
}

