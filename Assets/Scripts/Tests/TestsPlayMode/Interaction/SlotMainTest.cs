using System.Collections;
using System.Collections.Generic;
using Est.AI;
using Est.Interact;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class SlotMainTest : MonoBehaviour
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
        public IEnumerator SlotMainTest_StartSlotLocked_CallForUnityEventToUnlock()
        {
            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(600000); //10k

            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(660000); //11M

            //prepare
            var Slot = Resources.Load("Prefab/Slot") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);
            FindObjectOfType<ControlSlotInformation>().UpdateDataSlotMainAndTypeSlot();
            SlotMain slotMain = FindObjectOfType<SlotMain>();
            SlotLocked slotLocked = slotMain.GetComponent<SlotLocked>();

            yield return new WaitForEndOfFrame();

            //act
            slotLocked.OnTouchThisObject();

            yield return new WaitForEndOfFrame();

            Assert.IsFalse(slotMain.GetSlotLockedMain);
        }

        [UnityTest]
        public IEnumerator SlotMainTest_StartSlotLocked_NotCallForUnityEvent()
        {
            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(600000); //10k

            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(660000); //11M

            //prepare
            var Slot = Resources.Load("Prefab/Slot") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            SlotMain slotMain = FindObjectOfType<SlotMain>();

            yield return new WaitForEndOfFrame();

            //act
            //slotLocked.OnTouchThisObject();

            yield return new WaitForEndOfFrame();

            Assert.IsTrue(slotMain.GetSlotLockedMain);
        }

        [UnityTest]
        public IEnumerator SlotMainTest_StartSlotLocked_CallForUnityEventToUnlockSetGenerationPerSecond()
        {
            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(600000); //10k

            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(660000); //11M

            //prepare
            var Slot = Resources.Load("Prefab/Slot") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);
            FindObjectOfType<ControlSlotInformation>().UpdateDataSlotMainAndTypeSlot();
            SlotMain slotMain = FindObjectOfType<SlotMain>();
            SlotLocked slotLocked = slotMain.GetComponent<SlotLocked>();

            yield return new WaitForEndOfFrame();

            //act
            slotLocked.OnTouchThisObject();

            yield return new WaitForEndOfFrame();

            Assert.AreEqual(3.001f, controlCoins.CoinGenerationSecond);
        }

        [UnityTest]
        public IEnumerator SlotMainTest_StartSlotLocked_CallForUnityEventToUnlockCostToUpgradeSlot()
        {
            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(600000); //10k

            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(660000); //11M

            //prepare
            var Slot = Resources.Load("Prefab/Slot") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);
            FindObjectOfType<ControlSlotInformation>().UpdateDataSlotMainAndTypeSlot();
            SlotMain slotMain = FindObjectOfType<SlotMain>();
            SlotLocked slotLocked = slotMain.GetComponent<SlotLocked>();

            yield return new WaitForEndOfFrame();

            //act
            slotLocked.OnTouchThisObject();

            yield return new WaitForEndOfFrame();

            Assert.AreEqual(1, slotMain.CostToUpgradeSlot, "The CostToUpgradeSlot was: " + slotMain.CostToUpgradeSlot);
        }

        [UnityTest]
        public IEnumerator SlotMainTest_StartSlotLocked_CallForUnityEventTextUpgradeSlot()
        {
            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(600000); //10k

            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(660000); //11M

            //prepare
            var Slot = Resources.Load("Prefab/Slot") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);
            FindObjectOfType<ControlSlotInformation>().UpdateDataSlotMainAndTypeSlot();
            SlotMain slotMain = FindObjectOfType<SlotMain>();
            SlotLocked slotLocked = slotMain.GetComponent<SlotLocked>();

            yield return new WaitForEndOfFrame();

            //act
            slotLocked.OnTouchThisObject();
            //act
            Text text = GameObject.Find("TextCoinUpgrade").GetComponent<Text>();

            yield return new WaitForEndOfFrame();

            Assert.AreEqual("1 K", text.text, "The text in the textSlot is: " + text.text);
        }

        [UnityTest]
        public IEnumerator SlotMainTest_StartSlotLocked_CallForUnityEventChangeMesh()
        {
            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(600000); //10k

            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(660000); //11M

            var slotControlUI = Substitute.For<ISlotControlUI>();

            //prepare
            var Slot = Resources.Load("Prefab/Slot") as GameObject;
            GameObject SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);
            FindObjectOfType<ControlSlotInformation>().UpdateDataSlotMainAndTypeSlot();

            SlotMain slotMain = FindObjectOfType<SlotMain>();
            SlotLocked slotLocked = slotMain.GetComponent<SlotLocked>();

            yield return new WaitForSeconds(2);

            //act
            slotMain.ChangeValueISlotControlUI(slotControlUI);
            slotLocked.OnTouchThisObject();

            yield return new WaitForEndOfFrame();

            slotControlUI.Received(1).changeMeshByUpdateLevel(Arg.Any<Mesh>());
        }

        [UnityTest]
        public IEnumerator SlotMainTest_StartSlotLocked_ControlDestinationObjectTrue()
        {
            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(600000); //10k

            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(660000); //11M

            //prepare
            var Slot = Resources.Load("Prefab/Slot") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            SlotMain slotMain = FindObjectOfType<SlotMain>();
            SlotLocked slotLocked = slotMain.GetComponent<SlotLocked>();

            yield return new WaitForEndOfFrame();

            //act

            yield return new WaitForEndOfFrame();

            Assert.IsTrue(slotMain.GetComponent<ControlTargetDestinationObject>().Locked);
        }

        [UnityTest]
        public IEnumerator SlotMainTest_StartSlotLocked_CallSlotControlUIBackGroundColorLocked()
        {
            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(600000); //10k

            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(660000); //11M

            //prepare
            var Slot = Resources.Load("Prefab/Slot") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            SlotMain slotMain = FindObjectOfType<SlotMain>();
            SlotLocked slotLocked = slotMain.GetComponent<SlotLocked>();

            yield return new WaitForEndOfFrame(); //start

            //act
            Image image = GameObject.Find("ImageBackGroundCoin").GetComponent<Image>() ;

            Assert.AreEqual(new Color(0.643f, 0.518f, 0.518f, 1), image.color);
        }

        [UnityTest]
        public IEnumerator SlotMainTest_StartSlotLocked_CallSlotControlUIChangeMaxValueSliderExp()
        {
            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(600000); //10k

            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(660000); //11M

            //prepare
            var Slot = Resources.Load("Prefab/Slot") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);
            FindObjectOfType<ControlSlotInformation>().UpdateDataSlotMainAndTypeSlot();
            SlotMain slotMain = FindObjectOfType<SlotMain>();
            SlotLocked slotLocked = slotMain.GetComponent<SlotLocked>();

            yield return new WaitForEndOfFrame();

            //act
            Slider text = GameObject.Find("SliderCoinUpgrade").GetComponent<Slider>();

            yield return new WaitForEndOfFrame();

            Assert.AreEqual(3, text.maxValue);
        }

        [UnityTest]
        public IEnumerator SlotMainTest_StartSlotUnLocked_CallSlotLockedToUnlocked()
        {
            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(600000); //10k

            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(660000); //11M

            //prepare
            var Slot = Resources.Load("Prefab/Test/SlotForTestIndex0") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            SlotMain slotMain = FindObjectOfType<SlotMain>();
            SlotLocked slotLocked = slotMain.GetComponent<SlotLocked>();

            yield return new WaitForEndOfFrame();

            //act

            yield return new WaitForEndOfFrame();

            Assert.IsFalse(slotLocked.GetSlotLocked());
        }

        [UnityTest]
        public IEnumerator SlotMainTest_StartSlotUnLocked_ChangeGenerationPerSecond()
        {
            //prepare
            var Slot = Resources.Load("Prefab/Test/SlotForTestIndex0") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            SlotMain slotMain = FindObjectOfType<SlotMain>();
            SlotLocked slotLocked = slotMain.GetComponent<SlotLocked>();

            yield return new WaitForEndOfFrame();

            //act

            yield return new WaitForEndOfFrame();

            Assert.AreEqual(1 , controlCoins.CoinGenerationSecond, "The coinGeneration per second was: " + controlCoins.CoinGenerationSecond);
        }

        [UnityTest]
        public IEnumerator SlotMainTest_StartSlotUnLocked_ChangeCostToUnlock()
        {
            //prepare
            var Slot = Resources.Load("Prefab/Test/SlotForTestIndex0") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            SlotMain slotMain = FindObjectOfType<SlotMain>();

            yield return new WaitForEndOfFrame();

            //act

            yield return new WaitForEndOfFrame();

            Assert.AreEqual(1, slotMain.CostToUpgradeSlot, "The CostToUpgradeSlot was: " + slotMain.CostToUpgradeSlot);
            Assert.AreEqual(1, slotMain.CostToUpgradeSlotIndex, "The CostToUpgradeSlot index was: " + slotMain.CostToUpgradeSlotIndex);
        }

        [UnityTest]
        public IEnumerator SlotMainTest_StartSlotUnLocked_ChangeValueTextInSlot()
        {
            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(600000); //10k

            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(660000); //11M

            //prepare
            var Slot = Resources.Load("Prefab/Test/SlotForTestIndex0") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            SlotMain slotMain = FindObjectOfType<SlotMain>();

            yield return new WaitForEndOfFrame();

            //act
            Text text = GameObject.Find("TextCoinUpgrade").GetComponent<Text>();

            yield return new WaitForEndOfFrame();

            Assert.AreEqual("1 K", text.text, "The text in the textSlot is: " + text.text);
        }

        [UnityTest]
        public IEnumerator SlotMainTest_OnTouchThisObject_ShowInformationSlot()
        {
            //prepare
            var controlUI = Substitute.For<IControlUI>();
            //prepare
            var Slot = Resources.Load("Prefab/Slot") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);

            FindObjectOfType<ControlSlotInformation>().UpdateDataSlotMainAndTypeSlot();

            SlotMain slotMain = FindObjectOfType<SlotMain>();

            yield return new WaitForEndOfFrame();

            //act
            //act
            slotMain.ChangeControlUI(controlUI);
            slotMain.OnTouchThisObject();

            yield return new WaitForEndOfFrame();

            //assert
            controlUI.
            Received(1).
            ShowInformationSlot(Arg.Any<string>(), Arg.Any<Sprite>(), Arg.Any<TypeSlot>());
        }

        [UnityTest]
        public IEnumerator SlotMainTest_OnTouchThisObject_UpgradeSlot_SetGenerationPerSecond()
        {
            //controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.AugmentCoinRewardCoin(10000); //10k

            //prepare

            var Slot = Resources.Load("Prefab/Test/SlotForTestIndex0") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);
            FindObjectOfType<ControlSlotInformation>().UpdateDataSlotMainAndTypeSlot();
            SlotMain slotMain = FindObjectOfType<SlotMain>();

            yield return new WaitForEndOfFrame();

            //act
            slotMain.OnTouchThisObject(); //0.1

            yield return new WaitForEndOfFrame();

            Assert.AreEqual(0.101f, controlCoins.CoinGenerationSecond);
        }

        [UnityTest]
        public IEnumerator SlotMainTest_OnTouchThisObject_UpgradeSlot_SetCostUpgradeCoin()
        {
            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(600000); //10k

            //prepare

            var Slot = Resources.Load("Prefab/Test/SlotForTestIndex0") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);
            FindObjectOfType<ControlSlotInformation>().UpdateDataSlotMainAndTypeSlot();
            SlotMain slotMain = FindObjectOfType<SlotMain>();

            yield return new WaitForEndOfFrame();

            //act
            slotMain.OnTouchThisObject(); //0.1

            yield return new WaitForEndOfFrame();

            Assert.AreEqual(6, slotMain.CostToUpgradeSlot, "The CostToUpgradeSlot was: " + slotMain.CostToUpgradeSlot);
            Assert.AreEqual(1, slotMain.CostToUpgradeSlotIndex, "The CostToUpgradeSlot index was: " + slotMain.CostToUpgradeSlotIndex);
        }

        [UnityTest]
        public IEnumerator SlotMainTest_OnTouchThisObject_UpgradeSlot_SetCostUpgradeCoinPassLevelUnit()
        {
            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(600000); //10k

            //prepare

            var Slot = Resources.Load("Prefab/Test/SlotForTestIndex0") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);
            FindObjectOfType<ControlSlotInformation>().UpdateDataSlotMainAndTypeSlot();
            SlotMain slotMain = FindObjectOfType<SlotMain>();

            yield return new WaitForSeconds(1);

            //act
            slotMain.OnTouchThisObject(); // 6

            yield return new WaitForSeconds(1);

            controlCoins.CoinsSinceLastSessionInMinutes(600000); //10M
            //act
            slotMain.OnTouchThisObject(); // 36

            yield return new WaitForEndOfFrame();

            slotMain.OnTouchThisObject(); // 216

            yield return new WaitForEndOfFrame();

            slotMain.OnTouchThisObject(); // 4320

            yield return new WaitForEndOfFrame();

            Assert.AreEqual(4, slotMain.CostToUpgradeSlot, "The CostToUpgradeSlot was: " + slotMain.CostToUpgradeSlot);
        }

        [UnityTest]
        public IEnumerator SlotMainTest_OnTouchThisObject_UpgradeSlot_changeTextUpgradeCoin()
        {
            //prepare
            var controlUI = Substitute.For<ISlotControlUI>();
            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(600000); //10k

            //prepare

            var Slot = Resources.Load("Prefab/Test/SlotForTestIndex0") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);
            FindObjectOfType<ControlSlotInformation>().UpdateDataSlotMainAndTypeSlot();
            SlotMain slotMain = FindObjectOfType<SlotMain>();

            yield return new WaitForEndOfFrame();

            //act
            slotMain.ChangeValueISlotControlUI(controlUI);
            slotMain.OnTouchThisObject(); //0.1

            yield return new WaitForEndOfFrame();

            //assert
            controlUI.
            Received(1).
            changeTextUpgradeCoin(Arg.Any<long>(), Arg.Any<string>());
        }

        [UnityTest]
        public IEnumerator SlotMainTest_OnTouchThisObject_UpgradeSlot_changeValueSliderExp()
        {
            //prepare
            var controlUI = Substitute.For<ISlotControlUI>();
            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(600000); //10k

            //prepare

            var Slot = Resources.Load("Prefab/Test/SlotForTestIndex0") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);
            FindObjectOfType<ControlSlotInformation>().UpdateDataSlotMainAndTypeSlot();
            SlotMain slotMain = FindObjectOfType<SlotMain>();

            yield return new WaitForEndOfFrame();

            //act
            slotMain.ChangeValueISlotControlUI(controlUI);
            slotMain.OnTouchThisObject(); //0.1

            yield return new WaitForEndOfFrame();

            //assert
            controlUI.
            Received(1).
            changeValueSliderExp(Arg.Any<int>());
        }

        [UnityTest]
        public IEnumerator SlotMainTest_OnTouchThisObject_UpgradeSlot_PassLevelChangeMaxValueSliderExp()
        {
            //prepare
            var controlUI = Substitute.For<ISlotControlUI>();
            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(600000); //10k

            //prepare

            var Slot = Resources.Load("Prefab/Test/SlotForTestIndex0") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);
            FindObjectOfType<ControlSlotInformation>().UpdateDataSlotMainAndTypeSlot();
            SlotMain slotMain = FindObjectOfType<SlotMain>();

            yield return new WaitForEndOfFrame();

            //act
            slotMain.ChangeValueISlotControlUI(controlUI);
            slotMain.OnTouchThisObject(); //6

            yield return new WaitForSeconds(1);

            controlCoins.CoinsSinceLastSessionInMinutes(600000); //10M
            //act
            slotMain.OnTouchThisObject(); // 36

            yield return new WaitForEndOfFrame();

            slotMain.OnTouchThisObject(); // 216

            yield return new WaitForEndOfFrame();

            slotMain.OnTouchThisObject(); // 4320

            yield return new WaitForEndOfFrame();

            //assert
            controlUI.
            Received(1).
            changeMaxValueSliderExp(Arg.Any<int>());
        }

        [UnityTest]
        public IEnumerator SlotMainTest_OnTouchThisObject_UpgradeSlot_PassLevelChangeMeshByUpdateLevel()
        {
            //prepare
            var controlUI = Substitute.For<ISlotControlUI>();
            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(600000); //10k

            //prepare

            var Slot = Resources.Load("Prefab/Test/SlotForTestIndex0") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);
            FindObjectOfType<ControlSlotInformation>().UpdateDataSlotMainAndTypeSlot();
            SlotMain slotMain = FindObjectOfType<SlotMain>();

            yield return new WaitForEndOfFrame();

            //act
            slotMain.ChangeValueISlotControlUI(controlUI);
            slotMain.OnTouchThisObject(); //6

            yield return new WaitForSeconds(1);

            controlCoins.CoinsSinceLastSessionInMinutes(600000); //10M
            //act
            slotMain.OnTouchThisObject(); // 36

            yield return new WaitForEndOfFrame();

            slotMain.OnTouchThisObject(); // 216

            yield return new WaitForEndOfFrame();

            slotMain.OnTouchThisObject(); // 4320

            yield return new WaitForEndOfFrame();

            //assert
            controlUI.
            Received(1).
            changeMeshByUpdateLevel(Arg.Any<Mesh>());
        }

        [UnityTest]
        public IEnumerator SlotMainTest_OnTouchThisObject_UpgradeSlot_PassThirdLevel()
        {
            //prepare
            var controlUI = Substitute.For<ISlotControlUI>();
            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(600000); //10k

            //prepare

            var Slot = Resources.Load("Prefab/Test/SlotForTestIndex0") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);
            FindObjectOfType<ControlSlotInformation>().UpdateDataSlotMainAndTypeSlot();
            SlotMain slotMain = FindObjectOfType<SlotMain>();

            yield return new WaitForEndOfFrame();

            //act
            slotMain.ChangeValueISlotControlUI(controlUI);
            slotMain.OnTouchThisObject(); //0

            yield return new WaitForSeconds(1);

            controlCoins.CoinsSinceLastSessionInMinutes(600000); //10M
            //act
            slotMain.OnTouchThisObject(); // 1

            yield return new WaitForEndOfFrame();
        
            slotMain.OnTouchThisObject(); // 2

            yield return new WaitForEndOfFrame();

            slotMain.OnTouchThisObject(); // 0

            yield return new WaitForEndOfFrame();

            slotMain.OnTouchThisObject(); // 1

            yield return new WaitForEndOfFrame();

            controlCoins.CoinsSinceLastSessionInMinutes(600000); //
            slotMain.OnTouchThisObject(); // 2

            yield return new WaitForEndOfFrame();

            slotMain.OnTouchThisObject(); // 3

            yield return new WaitForEndOfFrame();

            controlCoins.CoinsSinceLastSessionInMinutes(600000); //
            slotMain.OnTouchThisObject(); // 4

            yield return new WaitForEndOfFrame();

            slotMain.OnTouchThisObject(); // 5

            yield return new WaitForEndOfFrame();

            controlCoins.CoinsSinceLastSessionInMinutes(600000); //
            slotMain.OnTouchThisObject(); // 6

            yield return new WaitForEndOfFrame();

            slotMain.OnTouchThisObject(); // 7

            yield return new WaitForEndOfFrame();


            controlCoins.CoinsSinceLastSessionInMinutes(600000); //
            slotMain.OnTouchThisObject(); // 8

            yield return new WaitForEndOfFrame();

            slotMain.OnTouchThisObject(); // 9

            yield return new WaitForEndOfFrame();

            slotMain.OnTouchThisObject(); // 0

            yield return new WaitForEndOfFrame();

            //assert
            controlUI.
            Received(2).
            changeMeshByUpdateLevel(Arg.Any<Mesh>());
        }

    }
}
