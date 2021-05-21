using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Est.CycleGoal
{
    [CreateAssetMenu(fileName = "CoinDataGoal", menuName = "DataGoal/ New Data Goal Coin", order = 0)]
    public class CoinDataGoal : DataGoal
    {
        [Header("Data type Goal")]
        [SerializeField] private float m_coinLevelToGoal = 0;
        [SerializeField] private float m_generationCoinToGoal = 0;
        [SerializeField] private bool m_IsCoinGoal = false;

        public override float GetValueToGoalCoin
        {
            get
            {
                if (m_IsCoinGoal)
                    return m_coinLevelToGoal;
                else
                    return m_generationCoinToGoal;
            }
        }
    }
}
