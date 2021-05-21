using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Est.CycleGoal
{
    [CreateAssetMenu(fileName = "ListCycleGoals", menuName = "DataGoal/ New List Cycle Goals", order = 1)]
    public class ListCycleGoals : ScriptableObject
    {
        [SerializeField] DataGoal[] DateGoalsArray;

        List<DataGoal> dataGoals = new List<DataGoal>();

        public List<DataGoal> GetListTotalCycleDataGoals() {
            dataGoals.Clear();
            for (int i = 0; i < DateGoalsArray.Length; i++) {
                dataGoals.Add(DateGoalsArray[i]);
            }
            return dataGoals;
        }
    }
}
