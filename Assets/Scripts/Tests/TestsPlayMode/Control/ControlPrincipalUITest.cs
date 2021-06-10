using System.Collections;
using System.Collections.Generic;
using Est.Interact;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class ControlPrincipalUITest : MonoBehaviour
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
        public IEnumerator ControlPrincipalUICallTextCoins_ReceivedCall()
        {            //prepare

            var controlUI = Substitute.For<IControlUI>();
            controlCoins.SetCoinGenerationPerSecond(1);

            yield return new WaitForEndOfFrame(); //start

            //prepare
            controlCoins.ChangePrincipalUI(controlUI);
            controlCoins.CoinsSinceLastSessionInMinutes(60000); //1

            yield return new WaitForEndOfFrame();


            controlUI.
            Received(1).
            changeTextCoins(Arg.Any<float>(), Arg.Any<string>());
        }

        [UnityTest]
        public IEnumerator ControlCoinPremium_ChangeValueOFthePremiumCoin_SetUI_ReceivedCall()
        {            //prepare

            var controlUI = Substitute.For<IControlUI>();
            controlCoins.SetCoinGenerationPerSecond(1);

            yield return new WaitForEndOfFrame(); //start

            //prepare
            controlCoins.GetComponent<ControlCoinPremium>().ChangePrincipalUI(controlUI);
            controlCoins.GetComponent<ControlCoinPremium>().SetAugmentPremiumCoin(60000); //1

            yield return new WaitForEndOfFrame();


            controlUI.
            Received(1).
            changeTextCoinPremium(Arg.Any<int>());
        }

        [UnityTest]
        public IEnumerator ControlPrincipalUICallTextGenerationCoins_ReceivedCall()
        {        
            //prepare
            var controlUI = Substitute.For<IControlUI>();

            yield return new WaitForEndOfFrame(); //start

            //act
            controlCoins.ChangePrincipalUI(controlUI);
            controlCoins.SetCoinGenerationPerSecond(1); //1

            yield return new WaitForEndOfFrame();

            //assert
            controlUI.
            Received(1).
            changeTextGenerationCoins(Arg.Any<float>(), Arg.Any<string>());
        }

        [UnityTest]
        public IEnumerator ControlPrincipalUICallShowInformationSlot_ReceivedCall()
        {
            //prepare
            var controlUI = Substitute.For<IControlUI>();
            var canvasSlotInfo = GameObject.Find("BackGroundSlotInformation");

            SlotAdvertising slotAdvertising = FindObjectOfType<SlotAdvertising>();

            yield return new WaitForEndOfFrame(); //start

            //act
            slotAdvertising.ChangeControlUI(controlUI);
            slotAdvertising.OnTouchThisObject();

            yield return new WaitForEndOfFrame();

            //assert
            controlUI.
            Received(1).
            ShowInformationSlot(Arg.Any<string>(), Arg.Any<Sprite>(), Arg.Any<TypeSlot>());
        }

        [UnityTest]
        public IEnumerator ControlPrincipalUICallShowInformationSlot_GameObjectActivatedSlotInformation()
        {
            var Slot = Resources.Load("Prefab/SlotAdvertising") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            SlotAdvertising slotAdvertising = FindObjectOfType<SlotAdvertising>();

            yield return new WaitForEndOfFrame(); //start

            //act
            slotAdvertising.OnTouchThisObject();

            //prepare
            var canvasSlotInfo = GameObject.Find("BackGroundSlotInformation");

            yield return new WaitForEndOfFrame();

            //assert
            Assert.IsTrue(canvasSlotInfo.gameObject.activeSelf);
        }

        [UnityTest]
        public IEnumerator ControlPrincipalUICallShowInformationSlot_GameObjectActivatedSlotInformationUpdateDesactiveAfterTime()
        {
            var Slot = Resources.Load("Prefab/SlotAdvertising") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            SlotAdvertising slotAdvertising = FindObjectOfType<SlotAdvertising>();

            yield return new WaitForEndOfFrame(); //start

            //act
            slotAdvertising.OnTouchThisObject();

            //prepare
            var canvasSlotInfo = GameObject.Find("BackGroundSlotInformation");

            yield return new WaitForSeconds(10);

            //assert
            Assert.IsFalse(canvasSlotInfo.gameObject.activeSelf);
        }

        [UnityTest]
        public IEnumerator ControlPrincipalUICallShowInformationSlot_VerificatedText()
        {
            var Slot = Resources.Load("Prefab/SlotAdvertising") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            SlotAdvertising slotAdvertising = FindObjectOfType<SlotAdvertising>();

            string stringValue = "¡Publicitate y gana el X2 en generación por segundo!";

            yield return new WaitForEndOfFrame(); //start



            //act
            slotAdvertising.OnTouchThisObject();

            //prepare
            var canvasTextInfo = GameObject.Find("TextSlotInf");

            yield return new WaitForEndOfFrame();

            //assert
            Assert.AreEqual(canvasTextInfo.GetComponent<Text>().text, stringValue);
        }

        [UnityTest]
        public IEnumerator ControlPrincipalUICallShowInformationSlot_VerificatedImageSprite()
        {
            var Slot = Resources.Load("Prefab/SlotAdvertising") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            SlotAdvertising slotAdvertising = FindObjectOfType<SlotAdvertising>();

            yield return new WaitForEndOfFrame(); //start

            //act
            slotAdvertising.OnTouchThisObject();

            //prepare
            var canvasImageInfo = GameObject.Find("ImageSlotInf");

            yield return new WaitForEndOfFrame();

            //assert
            Assert.AreEqual(canvasImageInfo.GetComponent<Image>().sprite.name, "Knob",
                "The name is: " + canvasImageInfo.GetComponent<Image>().sprite.name);
        }

        [UnityTest]
        public IEnumerator ControlPrincipalUICallShowInformationSlot_VerificatedChangeValueUIType()
        {
            var Slot = Resources.Load("Prefab/SlotAdvertising") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            SlotAdvertising slotAdvertising = FindObjectOfType<SlotAdvertising>();

            yield return new WaitForEndOfFrame(); //start

            //act
            slotAdvertising.OnTouchThisObject();

            //prepare
            var imageTypeSlot = GameObject.Find("TypeSlotUpgradeImage");
            var textTypeSlot = GameObject.Find("TextTypeSlotUpgradeImage");


            yield return new WaitForEndOfFrame();

            //assert
            Assert.AreEqual(imageTypeSlot.GetComponent<Image>().sprite.name, "Knob",
                "The name is: " + imageTypeSlot.GetComponent<Image>().sprite.name);
            Assert.AreEqual(textTypeSlot.GetComponent<Text>().text, "Generation",
                "The text is: " + textTypeSlot.GetComponent<Text>().text);
        }
    }
}
