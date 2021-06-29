using System.Collections;
using System.Collections.Generic;
using Est.CycleGoal;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Mobile
{
    public class ControlCycleGoalTest
    {

        [UnityTest]
        public IEnumerator ControlCycleGoal_DictionaryFalse()
        {

            ListCycleGoals listCycle = Resources.Load("Data/CycleGoal/PrincipalCycle") as ListCycleGoals;

            yield return new WaitForEndOfFrame(); //start

            int max = 3;
            List<DataGoal> listCyclegoal = new List<DataGoal>();

            ControlCycleGoals controlCycle = new ControlCycleGoals(max, listCycle.GetListTotalCycleDataGoals());
            Dictionary<string, bool> dic = controlCycle.NewDataGoalIsPurchased();
            listCyclegoal = controlCycle.LoadDataActualGoalService();


            Debug.Log(dic.Count + " " + listCyclegoal.Count);
            Assert.IsFalse(dic[listCyclegoal[0].name]);
        }

        [UnityTest]
        public IEnumerator ControlCycleGoal_CountServiceZero()
        {

            ListCycleGoals listCycle = Resources.Load("Data/CycleGoal/PrincipalCycle") as ListCycleGoals;

            yield return new WaitForEndOfFrame(); //start

            int max = 3;
            List<DataGoal> listCyclegoal = new List<DataGoal>();

            ControlCycleGoals controlCycle = new ControlCycleGoals(max, listCycle.GetListTotalCycleDataGoals());

            Assert.AreEqual(controlCycle.GetCountGoalsInService(), 0);
        }

        [UnityTest]
        public IEnumerator ControlCycleGoal_CountServiceThree()
        {

            ListCycleGoals listCycle = Resources.Load("Data/CycleGoal/PrincipalCycle") as ListCycleGoals;

            yield return new WaitForEndOfFrame(); //start

            int max = 3;
            List<DataGoal> listCyclegoal = new List<DataGoal>();

            ControlCycleGoals controlCycle = new ControlCycleGoals(max, listCycle.GetListTotalCycleDataGoals());
            Dictionary<string, bool> dic = controlCycle.NewDataGoalIsPurchased();
            listCyclegoal = controlCycle.LoadDataActualGoalService();

            Assert.AreEqual(controlCycle.GetCountGoalsInService(), 3, 
                "The actual value of the index is: " + controlCycle.GetCountGoalsInService() + ", but I expected " + 3);
        }

        [UnityTest]
        public IEnumerator ControlCycleGoal_DictionaryTrue_SetDataGoalIsPurchased()
        {

            ListCycleGoals listCycle = Resources.Load("Data/CycleGoal/PrincipalCycle") as ListCycleGoals;

            yield return new WaitForEndOfFrame(); //start

            int max = 3;
            List<DataGoal> listCyclegoal = new List<DataGoal>();

            ControlCycleGoals controlCycle = new ControlCycleGoals(max, listCycle.GetListTotalCycleDataGoals());
           controlCycle.NewDataGoalIsPurchased();
            listCyclegoal = controlCycle.LoadDataActualGoalService();
            controlCycle.SetDataGoalIsPurchased(0);

            Dictionary<string, bool> dic = controlCycle.GetDataGoalIsPurchased();
            Assert.IsTrue(dic[listCyclegoal[0].name]);
        }

        [UnityTest]
        public IEnumerator ControlCycleGoal_DictionaryFalse_SetDataGoalIsPurchased()
        {

            ListCycleGoals listCycle = Resources.Load("Data/CycleGoal/PrincipalCycle") as ListCycleGoals;

            yield return new WaitForEndOfFrame(); //start

            int max = 3;
            List<DataGoal> listCyclegoal = new List<DataGoal>();

            ControlCycleGoals controlCycle = new ControlCycleGoals(max, listCycle.GetListTotalCycleDataGoals());
            controlCycle.NewDataGoalIsPurchased();
            listCyclegoal = controlCycle.LoadDataActualGoalService();
            controlCycle.SetDataGoalIsPurchased(1);

            Dictionary<string, bool> dic = controlCycle.GetDataGoalIsPurchased();
            Assert.IsFalse(dic[listCyclegoal[0].name]);
        }

        [UnityTest]
        public IEnumerator ControlCycleGoal_GetDataGoalIsPurchased_AllFalse()
        {

            ListCycleGoals listCycle = Resources.Load("Data/CycleGoal/PrincipalCycle") as ListCycleGoals;

            yield return new WaitForEndOfFrame(); //start

            int max = 3;
            List<DataGoal> listCyclegoal = new List<DataGoal>();

            ControlCycleGoals controlCycle = new ControlCycleGoals(max, listCycle.GetListTotalCycleDataGoals());
            controlCycle.NewDataGoalIsPurchased();
            listCyclegoal = controlCycle.LoadDataActualGoalService();

            Dictionary<string, bool> dic = controlCycle.GetDataGoalIsPurchased();
            Assert.IsFalse(dic[listCycle.GetListTotalCycleDataGoals()[0].name]);
            Assert.IsFalse(dic[listCycle.GetListTotalCycleDataGoals()[1].name]);
            Assert.IsFalse(dic[listCycle.GetListTotalCycleDataGoals()[2].name]);
            Assert.IsFalse(dic[listCycle.GetListTotalCycleDataGoals()[3].name]);
            Assert.IsFalse(dic[listCycle.GetListTotalCycleDataGoals()[4].name]);

        }

        [UnityTest]
        public IEnumerator ControlCycleGoal_GetDataGoalIsPurchased_FirstThreeTrue()
        {

            ListCycleGoals listCycle = Resources.Load("Data/CycleGoal/PrincipalCycle") as ListCycleGoals;

            yield return new WaitForEndOfFrame(); //start

            int max = 3;
            List<DataGoal> listCyclegoal = new List<DataGoal>();

            ControlCycleGoals controlCycle = new ControlCycleGoals(max, listCycle.GetListTotalCycleDataGoals());
            controlCycle.NewDataGoalIsPurchased();
            listCyclegoal = controlCycle.LoadDataActualGoalService();
            controlCycle.SetDataGoalIsPurchased(0);
            controlCycle.SetDataGoalIsPurchased(1);
            controlCycle.SetDataGoalIsPurchased(2);

            yield return new WaitForEndOfFrame(); //update

            Dictionary<string, bool> dic = controlCycle.GetDataGoalIsPurchased();
            Assert.IsTrue(dic[listCycle.GetListTotalCycleDataGoals()[0].name]);
            Assert.IsTrue(dic[listCycle.GetListTotalCycleDataGoals()[1].name]);
            Assert.IsTrue(dic[listCycle.GetListTotalCycleDataGoals()[2].name]);
            Assert.IsFalse(dic[listCycle.GetListTotalCycleDataGoals()[3].name]);
            Assert.IsFalse(dic[listCycle.GetListTotalCycleDataGoals()[4].name]);
        }

        [UnityTest]
        public IEnumerator ControlCycleGoal_LoadDataActualGoalService_FirstThree()
        {

            ListCycleGoals listCycle = Resources.Load("Data/CycleGoal/PrincipalCycle") as ListCycleGoals;

            yield return new WaitForEndOfFrame(); //start

            int max = 3;
            List<DataGoal> listCyclegoal = new List<DataGoal>();

            ControlCycleGoals controlCycle = new ControlCycleGoals(max, listCycle.GetListTotalCycleDataGoals());
            controlCycle.NewDataGoalIsPurchased();
            listCyclegoal = controlCycle.LoadDataActualGoalService();
            controlCycle.SetDataGoalIsPurchased(1);

            for (int i = 0; i < listCyclegoal.Count; i++) {
                Assert.AreEqual(listCyclegoal[i], listCycle.GetListTotalCycleDataGoals()[i]);
            }
        }

        [UnityTest]
        public IEnumerator ControlCycleGoal_SetActualGoalsService_Index0()
        {

            ListCycleGoals listCycle = Resources.Load("Data/CycleGoal/PrincipalCycle") as ListCycleGoals;

            yield return new WaitForEndOfFrame(); //start

            int max = 3;
            List<DataGoal> listCyclegoal = new List<DataGoal>();

            ControlCycleGoals controlCycle = new ControlCycleGoals(max, listCycle.GetListTotalCycleDataGoals());
            controlCycle.NewDataGoalIsPurchased();
            listCyclegoal = controlCycle.LoadDataActualGoalService();
            controlCycle.SetDataGoalIsPurchased(0);
            listCyclegoal = controlCycle.SetActualGoalsService(0, listCycle.GetListTotalCycleDataGoals()[3]);

            Assert.AreEqual(listCyclegoal[0], listCycle.GetListTotalCycleDataGoals()[3]);
            Assert.AreEqual(listCyclegoal[1], listCycle.GetListTotalCycleDataGoals()[1]);
            Assert.AreEqual(listCyclegoal[2], listCycle.GetListTotalCycleDataGoals()[2]);
        }

        [UnityTest]
        public IEnumerator ControlCycleGoal_SetActualGoalsService_Index2()
        {

            ListCycleGoals listCycle = Resources.Load("Data/CycleGoal/PrincipalCycle") as ListCycleGoals;

            yield return new WaitForEndOfFrame(); //start

            int max = 3;
            List<DataGoal> listCyclegoal = new List<DataGoal>();

            ControlCycleGoals controlCycle = new ControlCycleGoals(max, listCycle.GetListTotalCycleDataGoals());
            controlCycle.NewDataGoalIsPurchased();
            listCyclegoal = controlCycle.LoadDataActualGoalService();
            controlCycle.SetDataGoalIsPurchased(2);
            listCyclegoal = controlCycle.SetActualGoalsService(2, listCycle.GetListTotalCycleDataGoals()[4]);

            Assert.AreEqual(listCyclegoal[2], listCycle.GetListTotalCycleDataGoals()[4]);
            Assert.AreEqual(listCyclegoal[1], listCycle.GetListTotalCycleDataGoals()[1]);
            Assert.AreEqual(listCyclegoal[0], listCycle.GetListTotalCycleDataGoals()[0]);
        }

        [UnityTest]
        public IEnumerator ControlCycleGoal_NewDataGoalInActualGoal()
        {

            ListCycleGoals listCycle = Resources.Load("Data/CycleGoal/PrincipalCycle") as ListCycleGoals;

            yield return new WaitForEndOfFrame(); //start

            int max = 3;
            List<DataGoal> listCyclegoal = new List<DataGoal>();

            ControlCycleGoals controlCycle = new ControlCycleGoals(max, listCycle.GetListTotalCycleDataGoals());
            controlCycle.NewDataGoalIsPurchased();
            listCyclegoal = controlCycle.LoadDataActualGoalService();
            controlCycle.SetDataGoalIsPurchased(0);
            listCyclegoal = controlCycle.SetActualGoalsService(0, controlCycle.NewDataGoalInActualGoal(0));

            Assert.AreEqual(listCyclegoal[0], listCycle.GetListTotalCycleDataGoals()[3]);
            Assert.AreEqual(listCyclegoal[1], listCycle.GetListTotalCycleDataGoals()[1]);
            Assert.AreEqual(listCyclegoal[2], listCycle.GetListTotalCycleDataGoals()[2]);
        }
    }
}
