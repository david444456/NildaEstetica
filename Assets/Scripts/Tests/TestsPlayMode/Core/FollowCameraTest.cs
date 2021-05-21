using System.Collections;
using System.Collections.Generic;
using Est.Core;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class FollowCameraTest
    {
        [UnityTest]
        public IEnumerator FollowCameraTestWithEnumeratorPasses()
        {
            var loadCamera = Resources.Load("Prefab/Core/Main Camera") as GameObject;
            var camera = GameObject.Instantiate(loadCamera, new Vector3(1, 0, 1), Quaternion.identity);
            var follow = new GameObject("follow").AddComponent<FollowCamera>();

            yield return new WaitForEndOfFrame(); // start

            //act 
            follow.LookCameraMethod();

            yield return new WaitForEndOfFrame(); // update

            Vector3 vectorRotationFollow = new Vector3(follow.transform.eulerAngles.x, follow.transform.eulerAngles.y, follow.transform.eulerAngles.z);

            Assert.AreEqual(vectorRotationFollow, new Vector3(0,45,0)
                , "Follow rotation: " + follow.transform.rotation);
        }
    }
}
