using System.Collections;
using System.Collections.Generic;
using Est.Interact;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Interaction
{
    public class SlotMoneyTest : MonoBehaviour
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
            GameObject.Destroy(coreGO);
            Destroy(camera);
        }

        [UnityTest]
        public IEnumerator SlotMoneyTestWhenCallOnTouch_AddCoinInControlCoins()
        {
            //prepare
            controlCoins.SetCoinGenerationPerSecond(1);

            //prepare
            var Slot = Resources.Load("Prefab/SlotMoney") as GameObject;
            var SlotMoney = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            SlotMoney slotMoney = FindObjectOfType<SlotMoney>();

            yield return new WaitForEndOfFrame(); //start

            slotMoney.OnTouchThisObject();

            yield return new WaitForEndOfFrame();

            //asert/act
            Assert.AreEqual(2f, controlCoins.Coins
                , "Coins actual is: " + controlCoins.Coins + ", and compare: " + 1f);
        }
    }
}
