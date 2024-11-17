using PixelWizards.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelWizards.Controllers
{
    public class StartupController : MonoBehaviour
    {
        [SerializeField] private string gameplaySceneName = "Gameplay";
        private void Awake()
        {
            ServiceManager.onServiceInitialized += Init;
        }

        private void OnDisable()
        {
            ServiceManager.onServiceInitialized -= Init;
        }

        private void Init()
        {
            Debug.Log("Services Initialized, loading gameplay scene");
            SceneManager.LoadScene(gameplaySceneName);
        }
    }
}