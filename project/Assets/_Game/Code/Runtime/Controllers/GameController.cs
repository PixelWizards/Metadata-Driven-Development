using PixelWizards.Controllers;
using PixelWizards.Services;
using UnityEngine;

namespace PixelWizards.Gameplay
{
    public class GameController : MonoBehaviour
    {
        private void Start()
        {
            SpawnAllItems();
        }

        private void SpawnAllItems()
        {
            Debug.Log("GameController::Spawning all items in database");
            // grab all of the items in our database
            var weapons = WeaponMetadataService.Instance.GetAll();
            Debug.Log($"We have {weapons.Count} weapons");
            
            var x = 0;
            var y = 0;
            var itemsPerRow = 4;
            
            // spawn them in rows
            foreach (var item in weapons)
            {
                // spawn the object
                var go = Instantiate(item.config.prefab);
                go.transform.position = new Vector3(x * 4, 0.5f, y * 4);
                go.transform.rotation = Quaternion.Euler(0, 45, 0);
                
                // and initialize it
                var controller = go.GetComponent<WeaponMetadataController>();
                controller.Init(item);
                
                x++;
                
                // reset the row
                if (x < itemsPerRow) continue;
                
                x = 0;
                y++;
            }
        }
    }
}