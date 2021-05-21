using System.Collections;
//using Est.Control;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;

namespace Tests
{
    public class InputManagerTest 
    {
        public class TestForTestEvents : MonoBehaviour, IBool
        {
            private bool isATestWorking = false;

            public bool GetBool() {
                return isATestWorking;
            }

            public void changeValueBool(Vector2 screenPosition, float time) {
                isATestWorking = true;
            }

        }

        public interface IBool {
            bool GetBool();
        }
        /*
        GameObject camera;
        InputManager inputMan;

        [SetUp]
        public void SetUp() {
            var loadCamera = Resources.Load("Prefab/Core/Main Camera") as GameObject;
            camera = GameObject.Instantiate(loadCamera, new Vector3(0, 0, 0), Quaternion.identity);
            //camera.gameObject.tag = "MainCamera";
            inputMan = new GameObject("input").AddComponent<InputManager>();
        }

        [TearDown]
        public void TearDown() {
            GameObject.Destroy(camera);
            GameObject.Destroy(inputMan);
        }

        //dont test private methods because it used unity class, only call two events

        [UnityTest]
        public IEnumerator CallAMethodInputManager_NotErrorNullCamera()
        {
            //prepare 

            yield return new WaitForEndOfFrame(); //start

            //act
            Assert.IsNotNull(inputMan.PrimeryContact());
        }

        [UnityTest]
        public IEnumerator CallAPrimaryContactInputManager_ReturnTheCorrectVector2()
        {
            //prepare 

            yield return new WaitForEndOfFrame(); //start

            //act
            Vector2 vectorExpectedTouchControlPrimaryPositionDefault = new Vector2(-0.9840746f, -0.5534256f);

            //assert
            Assert.IsTrue(inputMan.PrimeryContact() == vectorExpectedTouchControlPrimaryPositionDefault,
                "Primary return: " + inputMan.PrimeryContact().x + " and I expected: " + vectorExpectedTouchControlPrimaryPositionDefault.x);
        }

        [UnityTest]
        public IEnumerator CallAPrimaryContactInputManager_()
        {
            //prepare 

            TestForTestEvents forTestEvents = inputMan.gameObject.AddComponent<TestForTestEvents>();
            inputMan.OnStartTouch += forTestEvents.changeValueBool;
            

            yield return new WaitForEndOfFrame(); //start

//act

            yield return new WaitForEndOfFrame(); //update

            //assert
            Assert.IsTrue(forTestEvents.GetBool(),
                "Primary return: " + inputMan.PrimeryContact() + " and I expected: ");
        }*/
    }
}
