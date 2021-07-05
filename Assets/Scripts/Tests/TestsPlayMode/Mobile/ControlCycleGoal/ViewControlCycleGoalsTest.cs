using System;
using System.Collections;
using System.Collections.Generic;
using Est.CycleGoal;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Mobile
{
    public class ViewControlCycleGoalsTest : MonoBehaviour
    {
        ViewControlCycleGoals view;
        GameObject Maincamera;
        GameObject coreGO;

        [SetUp]
        public void SetUp()
        {
            //camera by not crash
            var loadCamera = Resources.Load("Prefab/Core/Main Camera") as GameObject;
            Maincamera = GameObject.Instantiate(loadCamera, new Vector3(1, 0, 1), Quaternion.identity);


            //prepare
            var core = Resources.Load("Prefab/Core/Core") as GameObject;
            coreGO = GameObject.Instantiate(core, new Vector3(0, 0, 0), Quaternion.identity);

            view = FindObjectOfType<ViewControlCycleGoals>();
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.Destroy(coreGO);
            Destroy(Maincamera);
        }

        [UnityTest]
        public IEnumerator StartMethod_CreateControlCycleGoals_GetActualGoalsInService_False()
        {
            yield return new WaitForEndOfFrame(); //start
            Dictionary<string, bool> doc = view.GetDataBoolPurchased();
            List<DataGoal> dat = view.GetActualGoalsInService();


            Assert.IsFalse(doc[dat[0].name]);
        }

        [UnityTest]
        public IEnumerator ReclaimedRewardCycleGoal_GetDataBoolPurchased_True()
        {
            yield return new WaitForEndOfFrame(); //start
            Dictionary<string, bool> doc = view.GetDataBoolPurchased();
            List<DataGoal> dat = view.GetActualGoalsInService();

            string info = dat[0].name;
            yield return new WaitForEndOfFrame(); //update
            view.ReclaimedRewardCycleGoal(0);

            yield return new WaitForEndOfFrame(); //update

            doc = view.GetDataBoolPurchased();

            Assert.IsTrue(view.GetDataBoolPurchased()[info]);
        }

        [UnityTest]
        public IEnumerator ReclaimedRewardCycleGoal_GetDataBoolPurchased_False()
        {
            yield return new WaitForEndOfFrame(); //start
            Dictionary<string, bool> doc = view.GetDataBoolPurchased();
            List<DataGoal> dat = view.GetActualGoalsInService();
            view.ReclaimedRewardCycleGoal(0);
            doc = view.GetDataBoolPurchased();

            Assert.IsFalse(doc[dat[1].name]);
        }

        [UnityTest]
        public IEnumerator ReclaimedRewardCycleGoal_AugmentCoin()
        {
            yield return new WaitForEndOfFrame(); //start
            Dictionary<string, bool> doc = view.GetDataBoolPurchased();
            List<DataGoal> dat = view.GetActualGoalsInService();
            view.ReclaimedRewardCycleGoal(0);
            ControlCoins controlC = FindObjectOfType<ControlCoins>();

            Assert.AreEqual(850.0f, controlC.Coins, "The actual coin is: " +controlC.Coins + " but the expected is ; " +850 );
        }

        [UnityTest]
        public IEnumerator ReclaimedRewardCycleGoal_NewData()
        {
            yield return new WaitForEndOfFrame(); //start
            Dictionary<string, bool> doc = view.GetDataBoolPurchased();

            List<DataGoal> dat = view.GetActualGoalsInService();

            view.ReclaimedRewardCycleGoal(0);
            ControlCoins controlC = FindObjectOfType<ControlCoins>();

            yield return new WaitForEndOfFrame(); //start


            Assert.AreEqual(view.GetListCycleGoals().GetListTotalCycleDataGoals()[3], view.GetActualGoalsInService()[0] , 
                "The actual data is: " + view.GetActualGoalsInService()[0] + " but the expected is ; " +
                view.GetListCycleGoals().GetListTotalCycleDataGoals()[3]);
        }

    }
}
