using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Est.CycleGoal
{
    [CreateAssetMenu(fileName = "StoreDataGoal", menuName = "DataGoal/ New Data Goal Store", order = 0)]
    public class StoreDataGoal : DataGoal
    {
        [Header("Data type Goal")]
        [SerializeField] private int m_indexItemStore3D = 0;

        public override int IndexItemStore3D => m_indexItemStore3D;
    }
}
