using System.Collections;
using System.Collections.Generic;
using Est.Interact;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Interaction
{
    public class SlotAdvertisingTest : MonoBehaviour
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
        public IEnumerator SlotAdvertisingTest_UnlockEventIsFalseUnlock()
        {
            //prepare
            var Slot = Resources.Load("Prefab/SlotAdvertising") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            SlotAdvertising slotAdvertising = FindObjectOfType<SlotAdvertising>();
            SlotLocked slotLocked = slotAdvertising.GetComponent<SlotLocked>();

            yield return new WaitForEndOfFrame();

            //act
            slotLocked.ActivatedUnlockEvent();

            //assert
            Assert.IsFalse(slotAdvertising.ThisSlotIsLocked);
        }

        [UnityTest]
        public IEnumerator SlotAdvertisingTest_UnlockEventGOAdvertisingUIText()
        {
            //prepare
            var Slot = Resources.Load("Prefab/SlotAdvertising") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            SlotAdvertising slotAdvertising = FindObjectOfType<SlotAdvertising>();
            SlotLocked slotLocked = slotAdvertising.GetComponent<SlotLocked>();

            //prepare
            var coinSlotAdvertising = GameObject.Find("CoinSlotAdvertising");

            yield return new WaitForEndOfFrame();

            //act
            slotLocked.ActivatedUnlockEvent();



            //assert
            Assert.IsFalse(coinSlotAdvertising.gameObject.activeSelf);

        }

        [UnityTest]
        public IEnumerator SlotAdvertisingTest_CallShowInformationSlotWhenIsLockIsTrue()
        {
            //prepare
            var controlUI = Substitute.For<IControlUI>();
            //prepare
            var Slot = Resources.Load("Prefab/SlotAdvertising") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            SlotAdvertising slotAdvertising = FindObjectOfType<SlotAdvertising>();

            yield return new WaitForEndOfFrame();

            //act
            slotAdvertising.ChangeControlUI(controlUI);
            slotAdvertising.OnTouchThisObject();

            //assert
            controlUI.
            Received(1).
            ShowInformationSlot(Arg.Any<string>(), Arg.Any<Sprite>(), Arg.Any<TypeSlot>());

        }
    }
}
