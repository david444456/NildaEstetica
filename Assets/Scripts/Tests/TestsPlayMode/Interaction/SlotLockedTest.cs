using System.Collections;
using System.Collections.Generic;
using Est.Interact;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Interaction
{
    public class SlotLockedTest : MonoBehaviour
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
        public IEnumerator SlotLockedTest_OnTouchThisObject()
        {
            //prepare
            var Slot = Resources.Load("Prefab/SlotAdvertising") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            SlotAdvertising slotAdvertising = FindObjectOfType<SlotAdvertising>();
            SlotLocked slotLocked = slotAdvertising.GetComponent<SlotLocked>();

            yield return new WaitForEndOfFrame();

            controlCoins.SetCoinGenerationPerSecond(1); //2
            controlCoins.CoinsSinceLastSessionInMinutes(120); //2

            //act
            slotLocked.costToUnlockedSlot = 1;
            slotLocked.indexActualLevelUnit = 0;

            yield return new WaitForEndOfFrame();

            slotLocked.OnTouchThisObject();

            //assert
            Assert.IsFalse(slotAdvertising.ThisSlotIsLocked);
        }

        [UnityTest]
        public IEnumerator SlotLockedTest_SlotLockedBool()
        {
            //prepare
            var Slot = Resources.Load("Prefab/SlotAdvertising") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            SlotAdvertising slotAdvertising = FindObjectOfType<SlotAdvertising>();
            SlotLocked slotLocked = slotAdvertising.GetComponent<SlotLocked>();

            yield return new WaitForEndOfFrame();

            //act
            slotLocked.changeLockedToUnlocked();

            yield return new WaitForEndOfFrame();

            //assert
            Assert.IsFalse(slotLocked.GetSlotLocked());
        }

        [UnityTest]
        public IEnumerator SlotLockedTest_SlotLockedBoolNotCallMethod()
        {
            //prepare
            var Slot = Resources.Load("Prefab/SlotAdvertising") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            SlotAdvertising slotAdvertising = FindObjectOfType<SlotAdvertising>();
            SlotLocked slotLocked = slotAdvertising.GetComponent<SlotLocked>();

            yield return new WaitForEndOfFrame();

            //act

            yield return new WaitForEndOfFrame();

            //assert
            Assert.IsTrue(slotLocked.GetSlotLocked());
        }

        [UnityTest]
        public IEnumerator SlotLockedTest_SlotMainChangeValuesToUnlockObjectSlotMain()
        {
            //prepare
            var Slot = Resources.Load("Prefab/Slot") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            SlotMain slotMain = FindObjectOfType<SlotMain>();
            SlotLocked slotLocked = slotMain.GetComponent<SlotLocked>();

            yield return new WaitForEndOfFrame();

            //act
            //start is the act

            yield return new WaitForEndOfFrame();

            //assert
            Assert.IsTrue(slotLocked.costToUnlockedSlot == 10);
            Assert.IsTrue(slotLocked.indexActualLevelUnit == 2);
        }
    }
}
