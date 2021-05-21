using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Est.Core
{
    public class FollowCamera : MonoBehaviour
    {
        Transform transformCamera;

        void Start()
        {
            transformCamera = Camera.main.transform;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            LookCameraMethod();
        }

        public void LookCameraMethod() {
            transform.LookAt(transformCamera);

        }
    }
}
