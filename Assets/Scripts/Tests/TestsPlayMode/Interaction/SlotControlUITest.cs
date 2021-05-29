using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Est.Interact;
using UnityEngine.UI;

namespace Tests
{
    public class SlotControlUITest : MonoBehaviour
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
        public IEnumerator SlotControlUITest_TextUpgradeSlot()
        {
            //prepare
            var Slot = Resources.Load("Prefab/Slot") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            SlotMain slotMain = FindObjectOfType<SlotMain>();
            SlotControlUI slotControlUI = slotMain.GetComponent<SlotControlUI>();

            yield return new WaitForEndOfFrame();

            //act
            slotControlUI.changeTextUpgradeCoin(1, " K");
            Text text = GameObject.Find("TextCoinUpgrade").GetComponent<Text>();

            yield return new WaitForEndOfFrame();

            Assert.AreEqual("1  K", text.text, "The text in the textSlot is: " + text.text);
        }

        [UnityTest]
        public IEnumerator SlotControlUITest_changeMaxValueSliderExp()
        {
            //prepare
            var Slot = Resources.Load("Prefab/Slot") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            SlotMain slotMain = FindObjectOfType<SlotMain>();
            SlotControlUI slotControlUI = slotMain.GetComponent<SlotControlUI>();

            yield return new WaitForEndOfFrame();

            //act
            slotControlUI.changeMaxValueSliderExp(1000);
            //act
            Slider text = GameObject.Find("SliderCoinUpgrade").GetComponent<Slider>();

            yield return new WaitForEndOfFrame();

            Assert.AreEqual(1000, text.maxValue, "The text in the slider is: " + text.maxValue);
        }

        [UnityTest]
        public IEnumerator SlotControlUITest_changeValueSliderExp()
        {
            //prepare
            var Slot = Resources.Load("Prefab/Slot") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            SlotMain slotMain = FindObjectOfType<SlotMain>();

            SlotControlUI slotControlUI = slotMain.GetComponent<SlotControlUI>();

            yield return new WaitForEndOfFrame();

            //act
            slotMain.enabled = false;
            slotControlUI.changeMaxValueSliderExp(1000);
            slotControlUI.changeValueSliderExp(100);

            Slider text = GameObject.Find("SliderCoinUpgrade").GetComponent<Slider>();

            Assert.AreEqual(100, text.value, "The text in the slider is: " + text.value);
        }

        [UnityTest]
        public IEnumerator SlotControlUITest_ChangeMeshByUpdateLevel()
        {
            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(600000); //10k

            //prepare

            var Slot = Resources.Load("Prefab/Slot") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            SlotMain slotMain = FindObjectOfType<SlotMain>();
            SlotControlUI slotControlUI = slotMain.GetComponent<SlotControlUI>();
            Mesh meshTest = new Mesh();

            yield return new WaitForEndOfFrame();

            MeshFilter mesh = slotMain.GetComponent<MeshFilter>();
            slotControlUI.changeMeshByUpdateLevel(meshTest);

            yield return new WaitForEndOfFrame();

            Assert.AreEqual(meshTest, mesh.mesh, "The text in the slider is: " + mesh.mesh);
        }

        [UnityTest]
        public IEnumerator SlotControlUITest_changeBackGroundAfterLocked_Method()
        {
            //prepare
            var Slot = Resources.Load("Prefab/Slot") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            SlotMain slotMain = FindObjectOfType<SlotMain>();
            SlotControlUI slotControlUI = slotMain.GetComponent<SlotControlUI>();

            yield return new WaitForEndOfFrame();

            //act
            slotControlUI.changeBackGroundColorLocked();
            Image image = GameObject.Find("ImageBackGroundCoin").GetComponent<Image>();

            yield return new WaitForEndOfFrame();

            Assert.AreEqual(new Color(0.643f, 0.518f, 0.518f, 1), image.color);
        }

        [UnityTest]
        public IEnumerator SlotControlUITest_changeBackGroundAfterUnlocked_Method()
        {
            //prepare
            var Slot = Resources.Load("Prefab/Slot") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            SlotMain slotMain = FindObjectOfType<SlotMain>();
            SlotControlUI slotControlUI = slotMain.GetComponent<SlotControlUI>();

            controlCoins.CoinsSinceLastSessionInMinutes(1800000); //30k

            yield return new WaitForEndOfFrame();

            //act
            slotControlUI.changeBackGroundAfterUnlocked();
            Image image = GameObject.Find("ImageBackGroundCoin").GetComponent<Image>();

            Assert.AreEqual
                (new Color(0.839f, 1f, 0.365f, 1f), image.color, 
                "The color is: " + image.color);
        }

        [UnityTest]
        public IEnumerator SlotControlUITest_changeBackGroundAfterUnlocked_Update_AfterUnlockCanBuyAndVerifyChangeBackGround()
        {

            var Slot = Resources.Load("Prefab/Slot") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            FindObjectOfType<ControlSlotInformation>().UpdateDataSlotMainAndTypeSlot();

            yield return new WaitForEndOfFrame();

            SlotMain slotMain = FindObjectOfType<SlotMain>();
            SlotControlUI slotControlUI = slotMain.GetComponent<SlotControlUI>();

            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(60000); //1k

            yield return new WaitForEndOfFrame();

            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(60000); //1M

            yield return new WaitForEndOfFrame();

            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(60000); //1Bi

            yield return new WaitForEndOfFrame();

            //act
            slotMain.GetComponent<SlotLocked>().OnTouchThisObject();
            Image image = GameObject.Find("ImageBackGroundCoin").GetComponent<Image>();

            yield return new WaitForEndOfFrame();

            Assert.AreEqual
                (new Color(0.839f, 1f, 0.365f, 1f), image.color,
                "The color is: " + image.color);
        }

        [UnityTest]
        public IEnumerator SlotControlUITest_changeBackGroundAfterUnlocked_Update_AfterUnlockCantBuyAndVerifyNotChangeBackGround()
        {

            //prepare
            var Slot = Resources.Load("Prefab/Slot") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            FindObjectOfType<ControlSlotInformation>().UpdateDataSlotMainAndTypeSlot();

            yield return new WaitForEndOfFrame();

            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(60000); //1k

            SlotMain slotMain = FindObjectOfType<SlotMain>();
            SlotControlUI slotControlUI = slotMain.GetComponent<SlotControlUI>();

            yield return new WaitForEndOfFrame();

            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(600000); //10M

            yield return new WaitForEndOfFrame();

            //act
            slotMain.GetComponent<SlotLocked>().OnTouchThisObject();
            Image image = GameObject.Find("ImageBackGroundCoin").GetComponent<Image>();

            yield return new WaitForEndOfFrame();

            Assert.AreEqual
                (new Color(0.839f, 1f, 0.365f, 1f), image.color,
                "The color is: " + image.color);
        }

        [UnityTest]
        public IEnumerator SlotControlUITest_changeBackGroundAfterUnlocked_Update_CanBuy()
        {
            //prepare
            var Slot = Resources.Load("Prefab/Slot") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(60000); //1k

            SlotMain slotMain = FindObjectOfType<SlotMain>();
            SlotControlUI slotControlUI = slotMain.GetComponent<SlotControlUI>();

            yield return new WaitForEndOfFrame();

            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(600000); //10M

            //act
            Image image = GameObject.Find("ImageBackGroundCoin").GetComponent<Image>();

            yield return new WaitForEndOfFrame();

            yield return new WaitForSeconds(1);

            Assert.AreEqual
                (new Color(0.839f, 1f, 0.365f, 1f), image.color,
                "The color is: " + image.color);
        }

        [UnityTest]
        public IEnumerator SlotControlUITest_changeBackGroundAfterUnlocked_Update_CantBuy()
        {
            //prepare
            var Slot = Resources.Load("Prefab/Slot") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(60000); //1k

            SlotMain slotMain = FindObjectOfType<SlotMain>();
            SlotControlUI slotControlUI = slotMain.GetComponent<SlotControlUI>();

            yield return new WaitForEndOfFrame();

            //act
            Image image = GameObject.Find("ImageBackGroundCoin").GetComponent<Image>();

            yield return new WaitForEndOfFrame();

            yield return new WaitForSeconds(1);

            Assert.AreEqual(new Color(0.643f, 0.518f, 0.518f, 1), image.color, "The color is: " + image.color);
        }
    }
}
