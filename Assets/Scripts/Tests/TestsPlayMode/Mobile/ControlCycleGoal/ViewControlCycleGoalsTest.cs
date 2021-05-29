using System.Collections;
using System.Collections.Generic;
using Est.CycleGoal;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ViewControlCycleGoalsTest : MonoBehaviour
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
        public IEnumerator StartMethod_CreateControlCycleGoals_GetActualGoalsInService_False()
        {
            yield return new WaitForEndOfFrame(); //start
            Dictionary<string, bool> doc = view.GetDataBoolPurchased();
            List<DataGoal> dat = view.GetActualGoalsInService();


            Assert.IsFalse(doc[dat[0].name]);
        }

                [UnityTest]
        public IEnumerator ReclaimedRewardCycleGoal_GetDataBoolPurchased_False()
        {
            yield return new WaitForEndOfFrame(); //start
            Dictionary<string, bool> doc = view.GetDataBoolPurchased();
            List<DataGoal> dat = view.GetActualGoalsInService();
            view.ReclaimedRewardCycleGoal(0);
            doc = view.GetDataBoolPurchased();

            Assert.IsTrue(doc[dat[0].name]);
        }
    }
}
