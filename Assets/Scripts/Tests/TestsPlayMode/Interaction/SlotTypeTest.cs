using System.Collections;
using System.Collections.Generic;
using Est.Interact;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Interaction
{
    public class SlotTypeTest : MonoBehaviour
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
            var core = Resources.Load("Prefab/Core/Core") as GameObject;
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
        public IEnumerator SlotType_GetTypeSlot_ReturnCorrect()
        {
            //prepare
            var Slot = Resources.Load("Prefab/Test/SlotForTestIndex0") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            yield return new WaitForEndOfFrame();

            var newTypeSlot = Est.Interact.TypeSlotMainBusiness.Hairdressing;

            yield return new WaitForEndOfFrame();

            var slotT = FindObjectOfType<SlotType>();

            yield return new WaitForEndOfFrame();

            Assert.AreEqual(newTypeSlot, slotT.GetTypeSlot(), //0.1 by upgrade that slot.
                "The value is " + newTypeSlot + ", but the real value is: " + slotT.GetTypeSlot());
        }
    }
}
