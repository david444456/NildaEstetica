using System.Collections;
using System.Collections.Generic;
using Est.Mobile;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Mobile
{
    public class PlayerSessionViewTest : MonoBehaviour
    {
        PlayerSessionView playerSession;
        GameObject camera;
        GameObject coreGO;

        [SetUp]
        public void SetUp()
        {
            //camera by not crash
            var loadCamera = Resources.Load("Prefab/Core/Main Camera") as GameObject;
            camera = GameObject.Instantiate(loadCamera, new Vector3(1, 0, 1), Quaternion.identity);

            //prepare
            var core = Resources.Load("Prefab/Core/Core") as GameObject;
            coreGO = GameObject.Instantiate(core, new Vector3(0, 0, 0), Quaternion.identity);


            playerSession = FindObjectOfType<PlayerSessionView>();
        }

        [TearDown]
        public void TearDown()
        {
            foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
            {
                Destroy(o);
            }
        }

        [UnityTest]
        public IEnumerator PlayerSessionView_CreateNewPlayerSession_GetTimeHour()
        {
            yield return new WaitForEndOfFrame();

            Assert.IsTrue(playerSession.GetTimeHour() == 0, "The time hour is: " + playerSession.GetTimeHour());
        }

        [UnityTest]
        public IEnumerator PlayerSessionView_CreateNewPlayerSession_GetTimeMinute()
        {
            yield return new WaitForEndOfFrame();

            Assert.IsTrue(playerSession.GetTimeMinute() == 0, "The time minute is: " + playerSession.GetTimeMinute());
        }
    }
}
