using System.Collections;
using System.Collections.Generic;
using Est.CycleGoal;
using Est.Interact;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class CheckCompleteGoalTest : MonoBehaviour
    {
        ViewControlCycleGoals view;
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

            view = FindObjectOfType<ViewControlCycleGoals>();
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.Destroy(coreGO);
            Destroy(camera);
        }

        [UnityTest]
        public IEnumerator CheckCoinCompleteGoal_VerifyCompleteGoal()
        {
            yield return new WaitForEndOfFrame(); //start
            List<DataGoal> dat = view.GetActualGoalsInService();

            FindObjectOfType<ControlCoins>().SetCoinGenerationPerSecond(1);
            FindObjectOfType<ControlCoins>().CoinsSinceLastSessionInMinutes(600000);
            yield return new WaitForEndOfFrame(); //update

            yield return new WaitForEndOfFrame(); //update

            CheckCoinCompleteGoal checkCoin = FindObjectOfType<CheckCoinCompleteGoal>();

            yield return new WaitForEndOfFrame(); //update

            yield return new WaitForEndOfFrame();

            Assert.IsTrue(checkCoin.VerifyCompleteGoal(dat[0]), "The value is: " + checkCoin.VerifyCompleteGoal(dat[0]) + " with datagoal: " + dat[0].name);
        }

        [UnityTest]
        public IEnumerator CheckStore3DCompleteGoal_VerifyCompleteGoal()
        {
            //prepare
            yield return new WaitForEndOfFrame(); //start
            List<DataGoal> dat = view.GetActualGoalsInService();

            var goStore3D = Resources.Load("Prefab/Core/Store3D") as GameObject;
            GameObject store = GameObject.Instantiate(goStore3D, new Vector3(1, 0, 1), Quaternion.identity);

            FindObjectOfType<ControlCoins>().SetCoinGenerationPerSecond(1);
            FindObjectOfType<ControlCoins>().CoinsSinceLastSessionInMinutes(10000);
            yield return new WaitForEndOfFrame(); //update

            FindObjectOfType<ControlCoins>().CoinsSinceLastSessionInMinutes(10000);

            yield return new WaitForEndOfFrame(); //update

            FindObjectOfType<ControlCoins>().CoinsSinceLastSessionInMinutes(600000); //10k coin

            CheckStore3DCompleteGoal checkStor = FindObjectOfType<CheckStore3DCompleteGoal>();

            store.GetComponent<ControlStore3D>().activeOrDesactiveStore(true);

            yield return new WaitForEndOfFrame(); //update

            Button buttonRight = GameObject.Find("RightButtonStore").GetComponent<Button>();

            buttonRight.onClick.Invoke();

            checkStor.SetControlStore(store.GetComponent<ControlStore3D>());

            yield return new WaitForEndOfFrame();

            //action
            store.GetComponent<ControlStore3D>().TryBuyActualObject();

            yield return new WaitForEndOfFrame();

            Assert.IsTrue(checkStor.VerifyCompleteGoal(dat[2]), "The value is: " + checkStor.VerifyCompleteGoal(dat[2]) + " with datagoal: " + dat[2].name);
        }

        [UnityTest]
        public IEnumerator CheckSlotCompleteGoal_VerifyCompleteGoal()
        {
            yield return new WaitForEndOfFrame(); //start
            List<DataGoal> dat = view.GetActualGoalsInService();

            FindObjectOfType<ControlCoins>().SetCoinGenerationPerSecond(1);
            FindObjectOfType<ControlCoins>().CoinsSinceLastSessionInMinutes(600000);
            yield return new WaitForEndOfFrame(); //update

            var Slot = Resources.Load("Prefab/Slot") as GameObject;
            var SlotAd = GameObject.Instantiate(Slot, new Vector3(0, 0, 0), Quaternion.identity);
            FindObjectOfType<ControlSlotInformation>().UpdateDataSlotMainAndTypeSlot();

            yield return new WaitForEndOfFrame(); //update

            CheckSlotCompleteGoal checkSlot = FindObjectOfType<CheckSlotCompleteGoal>();

            FindObjectOfType<ControlCoins>().AugmentCoinRewardCoin(600000);
            yield return new WaitForEndOfFrame(); //update
            FindObjectOfType<ControlCoins>().AugmentCoinRewardCoin(600000);
            yield return new WaitForEndOfFrame(); //update
            FindObjectOfType<ControlCoins>().AugmentCoinRewardCoin(600000);
            yield return new WaitForEndOfFrame(); //update

            FindObjectOfType<SlotLocked>().OnTouchThisObject();

            yield return new WaitForEndOfFrame(); //update

            FindObjectOfType<SlotMain>().OnTouchThisObject();

            yield return new WaitForEndOfFrame();


            FindObjectOfType<SlotMain>().OnTouchThisObject();
            FindObjectOfType<ControlCoins>().CoinsSinceLastSessionInMinutes(10000);

            yield return new WaitForEndOfFrame();


            FindObjectOfType<SlotMain>().OnTouchThisObject();
            FindObjectOfType<ControlCoins>().CoinsSinceLastSessionInMinutes(10000);

            yield return new WaitForEndOfFrame();


            FindObjectOfType<SlotMain>().OnTouchThisObject();
            FindObjectOfType<ControlCoins>().CoinsSinceLastSessionInMinutes(600000);

            yield return new WaitForEndOfFrame();


            FindObjectOfType<SlotMain>().OnTouchThisObject();

            yield return new WaitForEndOfFrame();


            FindObjectOfType<SlotMain>().OnTouchThisObject();

            yield return new WaitForEndOfFrame();


            FindObjectOfType<SlotMain>().OnTouchThisObject();

            yield return new WaitForEndOfFrame();

            FindObjectOfType<SlotMain>().OnTouchThisObject();

            yield return new WaitForEndOfFrame();

            FindObjectOfType<SlotMain>().OnTouchThisObject();

            yield return new WaitForEndOfFrame();

            FindObjectOfType<SlotMain>().OnTouchThisObject();

            print("Aca es 8");

            yield return new WaitForEndOfFrame();

            FindObjectOfType<SlotMain>().OnTouchThisObject();

            yield return new WaitForEndOfFrame();

            Assert.IsTrue(checkSlot.VerifyCompleteGoal(dat[1]), "The value is: " + checkSlot.VerifyCompleteGoal(dat[1]) + " with datagoal: " + dat[1].name);
        }

    }
}
