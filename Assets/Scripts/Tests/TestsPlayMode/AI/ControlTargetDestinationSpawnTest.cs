using System.Collections;
using System.Collections.Generic;
using Est.AI;
using Est.Interact;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ControlTargetDestinationSpawnTest
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
            var core = Resources.Load("Prefab/Test/CoreTest") as GameObject;
            coreGO = GameObject.Instantiate(core, new Vector3(0, 0, 0), Quaternion.identity);


            controlCoins = GameObject.FindObjectOfType<ControlCoins>();
            controlPrincipalUI = GameObject.FindObjectOfType<ControlPrincipalUI>();
        }

        [TearDown]
        public void TearDown()
        {
            foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
            {
                GameObject.Destroy(o);
            }
        }

        [UnityTest]
        public IEnumerator SlotLockedOnTouchThisObject_lockedFalse()
        {
            //prepare
            var Slot = Resources.Load("Prefab/Slot") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            var controlTargetDestination = GameObject.FindObjectOfType<ControlTargetDestinationObject>();
            SlotLocked slotLocked = controlTargetDestination.GetComponent<SlotLocked>();

            yield return new WaitForEndOfFrame();

            controlCoins.SetCoinGenerationPerSecond(1); //2
            controlCoins.CoinsSinceLastSessionInMinutes(120); //2

            //act
            slotLocked.costToUnlockedSlot = 1;
            slotLocked.indexActualLevelUnit = 0;

            yield return new WaitForEndOfFrame();

            slotLocked.OnTouchThisObject();

            //assert
            Assert.IsFalse(controlTargetDestination.Locked);
        }

        [UnityTest]
        public IEnumerator SlotLockedOnTouchThisObject_lockedTrue()
        {
            //prepare
            var Slot = Resources.Load("Prefab/Slot") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            var controlTargetDestination = GameObject.FindObjectOfType<ControlTargetDestinationObject>();
            SlotLocked slotLocked = controlTargetDestination.GetComponent<SlotLocked>();

            yield return new WaitForEndOfFrame();

            controlCoins.SetCoinGenerationPerSecond(1); //2
            controlCoins.CoinsSinceLastSessionInMinutes(120); //2

            //act
            slotLocked.costToUnlockedSlot = 1;
            slotLocked.indexActualLevelUnit = 0;

            yield return new WaitForEndOfFrame();

            //assert
            Assert.IsTrue(controlTargetDestination.Locked);
        }

        [UnityTest]
        public IEnumerator CreateNewClient_ServiceBoolTrue()
        {
            //prepare
            var Slot = Resources.Load("Prefab/Test/TestSpawnClient") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            var controlTargetDestination = GameObject.FindObjectOfType<ControlTargetDestinationObject>();
            SlotLocked slotLocked = controlTargetDestination.GetComponent<SlotLocked>();


            yield return new WaitForEndOfFrame();

            yield return new WaitForSeconds(3.5f);


            var womanCharacter = GameObject.FindObjectOfType<AICharacter>();
            womanCharacter.CanSetDestinationInCharacterAI = false;

            yield return new WaitForSeconds(1);

            //assert
            Assert.IsTrue(controlTargetDestination.ServiceBool);
        }

        [UnityTest]
        public IEnumerator CreateNewClient_ServiceBoolFalse()
        {
            //prepare
            var Slot = Resources.Load("Prefab/Test/TestSpawnClient") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            var controlTargetDestination = GameObject.FindObjectOfType<ControlTargetDestinationObject>();
            SlotLocked slotLocked = controlTargetDestination.GetComponent<SlotLocked>();


            yield return new WaitForEndOfFrame();

            yield return new WaitForSeconds(2.5f);

            yield return new WaitForEndOfFrame();
            //assert
            Assert.IsFalse(controlTargetDestination.ServiceBool);
        }


    }
}
