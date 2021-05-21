
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Est.Control
{
    public class InputManager : MonoBehaviour //SingletonInInspector<InputManager>
    {

        public delegate void StartTouchEvent(Vector2 position, float time);
        public event StartTouchEvent OnStartTouch;
        public delegate void EndTouchEvent(Vector2 position, float time);
        public event EndTouchEvent OnEndTouch;

        Vector2 touchOrigin = -Vector2.one;
        Camera mainCamera;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
            {
                if (EventSystem.current.IsPointerOverGameObject()) return;
                Vector3 positionTouch = Input.GetMouseButtonDown(0) ? Input.mousePosition : new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0);

                //event
                OnStartTouch(positionTouch, Time.time);
            }
        }

        public void MobileSwipeDetection(ref int horizontal, ref int vertical) {
            if (Input.touchCount > 0)
            {
                Touch myTouch = Input.touches[0];
                if (myTouch.phase == TouchPhase.Began)
                {
                    touchOrigin = myTouch.position;
                }
                else if (myTouch.phase == TouchPhase.Ended && touchOrigin != -Vector2.one)
                {
                    Vector2 touchEnd = myTouch.position;
                    float x = touchEnd.x - touchOrigin.x;
                    float y = touchEnd.y - touchOrigin.y;
                    if (x != 0 || y != 0)
                    {
                        if (Mathf.Abs(x) >= Mathf.Abs(y))
                        {
                            horizontal = x > 0 ? 1 : -1;
                        }
                        else
                        {
                            vertical = y > 0 ? 1 : -1;
                        }
                    }
                }
            }
        }

        public int HorizontalDetection() => (int)Input.GetAxisRaw("Horizontal");

        public int VerticalDetection() => (int)Input.GetAxisRaw("Vertical");

        /*
        public void Awake()
        {
            //base.Awake();

            touchControls = new TouchControls();
           // touchControls.asset = inputActions;
            //FindObjectOfType<InputSystemUIInputModule>().actionsAsset = inputActions;
            //camera
            mainCamera = Camera.main;

        }

        private void OnEnable()
        {
            touchControls.Enable();
        }

        private void OnDisable()
        {
            touchControls.Disable();
        }

        private void Start()
        {

            //touch
            touchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);
            touchControls.Touch.TouchPress.canceled += ctx => EndTouch(ctx);

            print("asociando "+ touchControls.asset);
            //swipw
            touchControls.Touch.PrimeryContact.started += ctx => StartTouchPrimary(ctx);
            touchControls.Touch.PrimeryContact.canceled += ctx => EndTouchPrimary(ctx);
        }

        private void StartTouch(InputAction.CallbackContext context)
        {
            print("Start Touch" + touchControls.Touch.TouchPosition.ReadValue<Vector2>());

            if (OnStartTouch != null) OnStartTouch(touchControls.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
        }

        private void EndTouch(InputAction.CallbackContext context)
        {
            print("End Touch");
            if (OnEndTouch != null) OnEndTouch(touchControls.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.time);
        }
        
        //swipe

        private void StartTouchPrimary(InputAction.CallbackContext ctx)
        {
            if (OnStartTouch != null) OnStartTouch(Utils.ScreenToWorld(mainCamera, touchControls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)ctx.startTime);
        }

        private void EndTouchPrimary(InputAction.CallbackContext ctx)
        {
            if (OnEndTouch != null) OnEndTouch(Utils.ScreenToWorld(mainCamera, touchControls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)ctx.time);
        }

        public Vector2 PrimeryContact() {
            return Utils.ScreenToWorld(mainCamera, touchControls.Touch.PrimaryPosition.ReadValue<Vector2>());
        }
        */
    }
}
