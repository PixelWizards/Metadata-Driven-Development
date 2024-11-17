using UnityEngine;

namespace PixelWizards.Utilities
{
    public class BillboardUIElement : MonoBehaviour
    {
        private Camera mainCam;

        // Use this for initialization
        private void Start()
        {
            mainCam = Camera.main;
        }

        // Update is called once per frame
        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - mainCam.transform.position).normalized;
        }
    }
}