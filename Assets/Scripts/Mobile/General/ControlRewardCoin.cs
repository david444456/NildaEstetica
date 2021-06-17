using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Est.Control;

namespace Est.Mobile
{
    public class ControlRewardCoin : MonoBehaviour
    {
        [SerializeField] private float m_multiplicatorControlCoinReward = 0;
        private float m_lastRewardCoin = 0;
        private int m_lastIndexCoinReward = 0;

        void Start()
        {

        }

        public void NewRewardCar() {
            float rewardConvert = ControlCoins.Instance.WithDifUnits_ReturnNumberConvertedToTheMainUnit(m_lastRewardCoin, m_lastIndexCoinReward);
            ControlCoins.Instance.AugmentCoinRewardCoin(m_lastRewardCoin);
        }

        public void NewRewardCycleGoal(float coinToReward, int levelCoinToReward) {
            float rewardConvert = ControlCoins.Instance.WithDifUnits_ReturnNumberConvertedToTheMainUnit(coinToReward, levelCoinToReward);
            print(rewardConvert);
            ControlCoins.Instance.AugmentCoinRewardCoin(rewardConvert);
        }

        public float GetNewRewardCar() {
            m_lastRewardCoin = m_multiplicatorControlCoinReward * ControlCoins.Instance.Coins;
            m_lastIndexCoinReward = (int)ControlCoins.Instance.ActualLevelUnits;
            return m_lastRewardCoin;
        }
    }
}
