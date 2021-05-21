using System.Collections;
using System.Collections.Generic;
using Est.AI;
using Est.Interact;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class CarRewardSystemTest : MonoBehaviour
    {
        ControlCoins controlCoins;
        ControlPrincipalUI controlPrincipalUI;
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


            controlCoins = FindObjectOfType<ControlCoins>();
            controlPrincipalUI = FindObjectOfType<ControlPrincipalUI>();
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
        public IEnumerator CarRewardSystem_OnTouchThisObject_ActiveMenuReward()
        {
            //prepare
            var CarObject = Resources.Load("Prefab/Character/CarReward") as GameObject;
            GameObject CarReward = GameObject.Instantiate(CarObject, new Vector3(0, 0, 0), Quaternion.identity);

            var carRewardSystem = CarReward.GetComponent<CarRewardSystem>();

            var CarAI = GameObject.FindObjectOfType<AICharacter>();
            CarAI.CanSetDestinationInCharacterAI = false;

            yield return new WaitForEndOfFrame();

            //act
            carRewardSystem.OnTouchThisObject();

            yield return new WaitForEndOfFrame();

            GameObject menuReward = GameObject.Find("BackGroundRewardCar");

            //assert
            Assert.IsTrue(menuReward.activeInHierarchy);
        }

        [UnityTest]
        public IEnumerator CarRewardSystem_OnTouchThisObject_OnlyOneClickPerLife()
        {
            //prepare
            var CarObject = Resources.Load("Prefab/Character/CarReward") as GameObject;
            GameObject CarReward = GameObject.Instantiate(CarObject, new Vector3(0, 0, 0), Quaternion.identity);

            var carRewardSystem = CarReward.GetComponent<CarRewardSystem>();

            var CarAI = GameObject.FindObjectOfType<AICharacter>();
            CarAI.CanSetDestinationInCharacterAI = false;

            yield return new WaitForEndOfFrame();

            //act
            carRewardSystem.OnTouchThisObject();

            yield return new WaitForEndOfFrame();

            GameObject menuReward = GameObject.Find("BackGroundRewardCar");
            menuReward.SetActive(false);

            yield return new WaitForEndOfFrame();

            //act
            carRewardSystem.OnTouchThisObject();

            //assert
            Assert.IsFalse(menuReward.activeInHierarchy);
        }

        [UnityTest]
        public IEnumerator CarRewardSystem_OnTouchThisObject_DesactiveUIHeadInfo()
        {
            //prepare
            var CarObject = Resources.Load("Prefab/Character/CarReward") as GameObject;
            GameObject CarReward = GameObject.Instantiate(CarObject, new Vector3(0, 0, 0), Quaternion.identity);

            var carRewardSystem = CarReward.GetComponent<CarRewardSystem>();

            var CarAI = GameObject.FindObjectOfType<AICharacter>();
            CarAI.CanSetDestinationInCharacterAI = false;

            yield return new WaitForEndOfFrame();

            GameObject MenuHead = GameObject.Find("InfoCarAdsAvailable");

            //act
            carRewardSystem.OnTouchThisObject();

            yield return new WaitForEndOfFrame();

            //assert
            Assert.IsFalse(MenuHead.activeInHierarchy);
        }
    }
}
