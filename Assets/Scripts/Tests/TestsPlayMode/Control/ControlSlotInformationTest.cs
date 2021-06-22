using System.Collections;
using System.Collections.Generic;
using Est.Interact;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ControlSlotInformationTest : MonoBehaviour
    {
        ControlCoins controlCoins;
        ControlPrincipalUI controlPrincipalUI;
        ControlSlotInformation slotInformation;
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


            controlCoins = FindObjectOfType<ControlCoins>();
            controlPrincipalUI = FindObjectOfType<ControlPrincipalUI>();
            slotInformation = FindObjectOfType<ControlSlotInformation>();
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
        public IEnumerator AugmentGenerationInTypeSlot_SumCorrectlyInIndex()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return new WaitForEndOfFrame();

            var newTypeSlot = Est.Interact.TypeSlotMainBusiness.Esthetic;

            slotInformation.AugmentGenerationInTypeSlot(newTypeSlot, 5, 0);

            yield return new WaitForEndOfFrame();


            Assert.AreEqual(5, slotInformation.GetGenerationByIndex(newTypeSlot),
                "The value is " + 5 + ", but the real value is: " + slotInformation.GetGenerationByIndex(newTypeSlot));
            Assert.AreEqual(0, slotInformation.GetGenerationByIndex(Est.Interact.TypeSlotMainBusiness.Massages),
                "The value is " + 0 + ", but the real value is: " + slotInformation.GetGenerationByIndex(newTypeSlot));
            Assert.AreEqual(0, slotInformation.GetGenerationByIndex(Est.Interact.TypeSlotMainBusiness.Hairdressing),
                "The value is " + 0 + ", but the real value is: " + slotInformation.GetGenerationByIndex(newTypeSlot));
        }

        [UnityTest]
        public IEnumerator AugmentGenerationInTypeSlot_ControlCoins_GetCorrectValue()
        {
            yield return new WaitForEndOfFrame();

            var newTypeSlot = Est.Interact.TypeSlotMainBusiness.Esthetic;



            yield return new WaitForEndOfFrame();


            slotInformation.AugmentGenerationInTypeSlot(newTypeSlot, 5, 0);

            yield return new WaitForEndOfFrame();


            Assert.AreEqual(5, controlCoins.CoinGenerationSecond,
                "The value is " + 5 + ", but the real value is: " + controlCoins.CoinGenerationSecond);
        }

        [UnityTest]
        public IEnumerator AugmentGenerationInTypeSlot_ControlCoins_GetCorrectValue_WithSlot()
        {
            //prepare
            var Slot = Resources.Load("Prefab/Test/SlotForTestIndex0") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            yield return new WaitForEndOfFrame();

            var newTypeSlot = Est.Interact.TypeSlotMainBusiness.Esthetic;

            yield return new WaitForEndOfFrame();

            Debug.Log(controlCoins.CoinGenerationSecond);
            slotInformation.AugmentGenerationInTypeSlot(newTypeSlot, 5, 0);

            yield return new WaitForEndOfFrame();

            Assert.AreEqual(8, controlCoins.CoinGenerationSecond,
                "The value is " + 8 + ", but the real value is: " + controlCoins.CoinGenerationSecond);
        }

        [UnityTest]
        public IEnumerator UpgradeSlot_ControlCoins_GetCorrectValueGeneration()
        {
            //prepare
            var Slot = Resources.Load("Prefab/Test/SlotForTestIndex0") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            yield return new WaitForEndOfFrame();



            yield return new WaitForEndOfFrame();

            controlCoins.CoinsSinceLastSessionInMinutes(600000); //10k

            Debug.Log(controlCoins.CoinGenerationSecond);
            SlotMain slot = FindObjectOfType<SlotMain>();
            slot.OnTouchThisObject();

            yield return new WaitForEndOfFrame();

            Assert.AreEqual(0.103f, controlCoins.CoinGenerationSecond, //0.003 for initial value slot, and 0.1 by upgrade that slot.
                "The value is " + 0.103f + ", but the real value is: " + controlCoins.CoinGenerationSecond);
        }

        [UnityTest]
        public IEnumerator UpgradeSlot_SlotInformation_GetCorrectValueGenerationByIndex()
        {
            //prepare
            var Slot = Resources.Load("Prefab/Test/SlotForTestIndex0") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            yield return new WaitForEndOfFrame();

            var newTypeSlot = Est.Interact.TypeSlotMainBusiness.Hairdressing;

            yield return new WaitForEndOfFrame();

            controlCoins.CoinsSinceLastSessionInMinutes(600000); //10k

            SlotMain slot = FindObjectOfType<SlotMain>();
            slot.OnTouchThisObject();

            yield return new WaitForEndOfFrame();

            Assert.AreEqual(0.1f, slotInformation.GetGenerationByIndex(newTypeSlot), //0.1 by upgrade that slot.
                "The value is " + 0.1f + ", but the real value is: " + controlCoins.CoinGenerationSecond);
        }

        [UnityTest]
        public IEnumerator UpgradeSlot_SlotInformation_GetLevelOfSlotByIndex0()
        {
            //prepare
            var Slot = Resources.Load("Prefab/Test/CoreTest") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            yield return new WaitForEndOfFrame();

            var newTypeSlot = Est.Interact.TypeSlotMainBusiness.Hairdressing;

            yield return new WaitForEndOfFrame();

            var slotInf = FindObjectOfType<ControlSlotInformation>();

            yield return new WaitForEndOfFrame();

            Assert.AreEqual(0, slotInf.GetLevelOfSlotByIndex(newTypeSlot),
                "The level is " + 0 + ", but the real value is: " + slotInf.GetLevelOfSlotByIndex(newTypeSlot));
        }

        [UnityTest]
        public IEnumerator UpgradeSlot_SlotInformation_GetLevelOfSlotByIndex2()
        {
            //prepare
            var Slot = Resources.Load("Prefab/Test/CoreTest") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            yield return new WaitForEndOfFrame();

            var newTypeSlot = Est.Interact.TypeSlotMainBusiness.Hairdressing;

            controlCoins.CoinsSinceLastSessionInMinutes(600000); //10k

            yield return new WaitForEndOfFrame();

            var slotInf = FindObjectOfType<ControlSlotInformation>();
            var slotMain = FindObjectOfType<SlotMain>();
            slotMain.OnTouchThisObject();

            yield return new WaitForEndOfFrame();

            slotMain.OnTouchThisObject();

            controlCoins.CoinsSinceLastSessionInMinutes(600000); //10M

            yield return new WaitForEndOfFrame();

            slotMain.OnTouchThisObject();

            yield return new WaitForEndOfFrame();

            slotMain.OnTouchThisObject();

            yield return new WaitForEndOfFrame();

            Assert.AreEqual(0, slotInf.GetLevelOfSlotByIndex(newTypeSlot),
                "The level is " + 0 + ", but the real value is: " + slotInf.GetLevelOfSlotByIndex(newTypeSlot));
        }
    }
}
