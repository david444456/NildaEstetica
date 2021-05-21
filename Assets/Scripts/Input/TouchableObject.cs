using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Est.Control
{
    public class TouchableObject : MonoBehaviour
    {
        public UnityEvent OnTouch = new UnityEvent();

        private InputManager inputManager;
        private Camera cameraMain;

        private void Awake()
        {
            inputManager = FindObjectOfType<InputManager>();
            cameraMain = Camera.main;
        }

        private void OnEnable()
        {
            inputManager.OnStartTouch += Touchable;
        }

        private void OnDisable()
        {
            inputManager.OnStartTouch -= Touchable;
        }

        public void Touchable(Vector2 screenPosition, float time)
        {
            Vector3 screenCoordinates = new Vector3(screenPosition.x, screenPosition.y, cameraMain.nearClipPlane);
            Ray ray = cameraMain.ScreenPointToRay(screenCoordinates);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.gameObject == gameObject)
                {
                    OnTouch.Invoke();
                }
            }
        }
    }
}
