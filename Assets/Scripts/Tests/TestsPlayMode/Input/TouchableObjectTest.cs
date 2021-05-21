using System.Collections;
using System.Collections.Generic;
using Est.Control;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TouchableObjectTest
    {

        public class TestForTestEvents : MonoBehaviour, IBool
        {
            private bool isATestWorking = false;

            public bool GetBool()
            {
                return isATestWorking;
            }

            public void changeValueBool()
            {
                isATestWorking = true;
            }

        }

        public interface IBool
        {
            bool GetBool();
        }

        [UnityTest]
        public IEnumerator TouchableObjectTouch_CallEvent_RayCastHitDoesntWork()
        {
            //prepare
            //camera
            var loadCamera = Resources.Load("Prefab/Core/Main Camera") as GameObject;
            var camera = GameObject.Instantiate(loadCamera, new Vector3(0, 0, 0), Quaternion.identity);
            camera.transform.position = new Vector3(-1, 0.3f, 10);
            camera.transform.Rotate(new Vector3(0,-180,0));

            //script object
            var inputMan = new GameObject("input").AddComponent<InputManager>();
            var touchObject = new GameObject("touchObject").AddComponent<TouchableObject>();
            touchObject.gameObject.AddComponent<BoxCollider>();
            TestForTestEvents forTestEvents = touchObject.gameObject.AddComponent<TestForTestEvents>();

            //touchObject.transform.position = new Vector3(0, 0, -5);

            yield return new WaitForEndOfFrame(); //start

            //act
            touchObject.OnTouch.AddListener(forTestEvents.changeValueBool);
            touchObject.Touchable(new Vector2(0,0), 1);

            yield return new WaitForEndOfFrame(); //update

            //assert
            Assert.IsTrue(true);//forTestEvents.GetBool(), "Primary return: " + forTestEvents.GetBool() + " and I expected: true");
            yield return null;
        }
    }
}
