using System;
using PixelWizards.Shared.Base;
using PixelWizards.Shared.Extensions;
using UnityEngine;

namespace PixelWizards.Services
{
    public class ServiceManager : MonoBehaviour
    {
        public static event Action onServiceInitialized;
        
        /// <summary>
        /// Our master entry point for the entire application
        /// </summary>
        private void Start()
        {
            RegisterServices();
            InitServices();
            onServiceInitialized?.Invoke();
        }
        
        private void RegisterServices()
        {
            // our primary scriptable object service. 
            var metadataService = gameObject.GetOrAddComponent<MetadataService>();
            ServiceLocator.Add(metadataService);
            
            // each google sheet has their own service to parse the appropriate data
            var weaponService = gameObject.GetOrAddComponent<WeaponMetadataService>();
            ServiceLocator.Add(weaponService);
        }

        private void InitServices()
        {
            // init the services
            MetadataService.Instance.Init();
            WeaponMetadataService.Instance.Init();
        }
    }
}