using System.Collections;
using System.Collections.Generic;
using Est.Interact;
using UnityEngine;


namespace Est.CycleGoal
{
    [CreateAssetMenu(fileName = "StoreDataGoal", menuName = "DataGoal/ New Data Goal Slot", order = 0)]
    public class SlotDataGoal : DataGoal
    {
        [Header("Data type Goal")]
        [SerializeField] private TypeSlotMainBusiness m_typeSlotGoal;
        [SerializeField] private int m_levelSlotToGoal = 0;

        public override TypeSlotMainBusiness TypeSlotMain => m_typeSlotGoal;
        public override float GetLevelTypeSlotToCheck => m_levelSlotToGoal;
    }
}
