using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Est.Control
{
    public class SwipeDetection : MonoBehaviour
    {
        [SerializeField] Text textTestVelocity;
        [Header("Limits")]
        [SerializeField] private float maxPositionVertical = 5;
        [SerializeField] private float maxPositionHorizontal = 5;

        [Header("Move")]
        [SerializeField] private float speedMoveCamera = 0.02f;
        [SerializeField] private float minimumDistance = 0.2f;
        [SerializeField] private float maximumDistance = 2f;
        [SerializeField] private float maximumTime = 1f;

        [Range(0f,1f)]
        [SerializeField] private float directionThreshold = 0.9f; 

        private InputManager inputManager;
        private Camera cameraMain;

        private Vector2 startPosition;
        private float startTime;
        private Vector2 endPosition;
        private float endTime;

        private int horizontal = 0;
        private int vertical = 0;

        Vector2 directionMoveCamera;

        private void Awake()
        {
            inputManager = FindObjectOfType<InputManager>();
            cameraMain = Camera.main;
        }

        private void Update()
        {
#if UNITY_STANDOLE || UNITY_WEBGL || UNITY_EDITOR
            PcInputDetection();
#else //mobile
            MobileInputDetection();
#endif
            if (horizontal != 0 || vertical != 0) MoveCameraSwide(horizontal, vertical);
        }

        private void PcInputDetection()
        {
            horizontal = inputManager.HorizontalDetection();
            vertical = inputManager.VerticalDetection();
            if (horizontal != 0) vertical = 0;
        }

        private void MobileInputDetection() {
            inputManager.MobileSwipeDetection(ref horizontal, ref vertical);
        }

        private void MoveCameraSwide(int horizontal, int vertical)
        {
            float dirX = horizontal;
            float dirY = vertical;


            //limits
            if (LimitMovementCamera(horizontal, cameraMain.transform.position.x, -maxPositionHorizontal, maxPositionHorizontal)) dirX = 0;
            if (LimitMovementCamera(vertical, cameraMain.transform.position.z, -maxPositionVertical, maxPositionVertical)) dirY = 0;

            textTestVelocity.text = dirX + " y: " + dirY;

            //move
            cameraMain.transform.position = Vector3.MoveTowards(
                cameraMain.transform.position,
                new Vector3(cameraMain.transform.position.x + dirX, 
                            cameraMain.transform.position.y,
                            cameraMain.transform.position.z + dirY), 
                speedMoveCamera * Time.deltaTime);
        }

        private bool LimitMovementCamera(int direction, float cameraPosition_X, float min, float max) {
            return (direction > 0) ? cameraPosition_X >= max : cameraPosition_X  <= min;
        }

        /*private void OnEnable()
        {
            inputManager.OnStartTouch += SwipeStart;
            inputManager.OnEndTouch += SwipeEnd;
        }

        private void OnDisable()
        {
            inputManager.OnStartTouch -= SwipeStart;
            inputManager.OnEndTouch -= SwipeEnd;
        }*/
    }
}
