using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Est.Control;

namespace Est.Mobile
{
    public class ControlRewardCoin : MonoBehaviour
    {
        private float m_multiplicatorControlCoinReward = 0;
        private float m_lastRewardCoin = 0;

        void Start()
        {

        }

        public void NewRewardCar() {
            ControlCoins.Instance.AugmentCoinRewardCoin(m_lastRewardCoin);
        }

        public void NewRewardCycleGoal(float coinToReward, int levelCoinToReward) {
            float rewardConvert = ControlCoins.Instance.WithDifUnits_ReturnNumberConvertedToTheMainUnit(coinToReward, levelCoinToReward);
            print(rewardConvert);
            ControlCoins.Instance.AugmentCoinRewardCoin(rewardConvert);
        }

        public float GetNewRewardCar() {
            m_lastRewardCoin = m_multiplicatorControlCoinReward * ControlCoins.Instance.Coins;
            return m_lastRewardCoin;
        }
    }
}
