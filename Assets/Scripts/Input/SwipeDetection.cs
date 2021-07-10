using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Est.Control
{
    public class SwipeDetection : MonoBehaviour
    {
        [SerializeField] Text textTestVelocity;
        [SerializeField] Text textTestVelocity2;
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
        [SerializeField] private float speedMultiplicatorMovement = 2.2f;

        private InputManager inputManager;
        private Camera cameraMain;

        private Vector3 startPosition;
        private float startTime;
        private Vector3 endPosition;
        private float endTime;

        private float horizontal = 0;
        private float vertical = 0;

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
            horizontal = 0;
            vertical = 0;
            MobileInputDetection();
#endif
            if (horizontal != 0 || vertical != 0) MoveCameraSwide(horizontal, vertical);

            if (endPosition != startPosition) {
                if (LimitMovementCamera(horizontal, cameraMain.transform.position.x, -maxPositionHorizontal, maxPositionHorizontal)) return;
                else if (LimitMovementCamera(vertical, cameraMain.transform.position.z, -maxPositionVertical, maxPositionVertical)) return;
                cameraMain.transform.position = Vector3.MoveTowards(
                    cameraMain.transform.position,
                    endPosition,
                    speedMoveCamera * Time.deltaTime);
            }
        }

        private void PcInputDetection()
        {
            horizontal = inputManager.HorizontalDetection();
            vertical = inputManager.VerticalDetection();
            if (horizontal != 0) vertical = 0;
            textTestVelocity2.text = horizontal + " y: " + vertical;
        }

        private void MobileInputDetection() {
            inputManager.MobileSwipeDetection(ref horizontal, ref vertical);
            textTestVelocity2.text = horizontal + " y: " + vertical;
        }

        private void MoveCameraSwide(float horizontal, float vertical)
        {
            float dirX = horizontal;
            float dirY = vertical;


            //limits
            if (LimitMovementCamera(horizontal, cameraMain.transform.position.x, -maxPositionHorizontal, maxPositionHorizontal)) dirX = 0;
            if (LimitMovementCamera(vertical, cameraMain.transform.position.z, -maxPositionVertical, maxPositionVertical)) dirY = 0;

            textTestVelocity.text = dirX + " y: " + dirY;

            //move
            endPosition = new Vector3(cameraMain.transform.position.x + dirX * speedMultiplicatorMovement,
                            cameraMain.transform.position.y,
                            cameraMain.transform.position.z + dirY * speedMultiplicatorMovement);
        }

        private bool LimitMovementCamera(float direction, float cameraPosition_X, float min, float max) {
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
