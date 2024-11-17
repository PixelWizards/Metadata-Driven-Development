using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelWizards.Utilities
{
    public class WorldCanvasFindMainCamera : MonoBehaviour
    {
        private Canvas thisCanvas;
        
        void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoad;
            InitSceneCamera();
        }

        private void OnSceneLoad(Scene thisScene, LoadSceneMode args)
        {
            InitSceneCamera();
        }
        
        private void InitSceneCamera()
        {
            thisCanvas = gameObject.GetComponent<Canvas>();
            thisCanvas.worldCamera = Camera.main;
        }
    }
}
