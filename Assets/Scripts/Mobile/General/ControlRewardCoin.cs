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

        IAugmentCoin augmentCoin;
        IReturnConvertNumber convertNumber;

        void Start()
        {
            augmentCoin = ControlCoins.Instance;
            convertNumber = ControlCoins.Instance;
        }

        public void ChangeAugmentCoin(IAugmentCoin augment) => augmentCoin = augment;

        public void ChangeConvertNumber(IReturnConvertNumber convert) => convertNumber = convert;

        public void NewRewardCar() {
            float rewardConvert = convertNumber.WithDifUnits_ReturnNumberConvertedToTheMainUnit(m_lastRewardCoin, m_lastIndexCoinReward);
            augmentCoin.AugmentCoinRewardCoin(rewardConvert);
        }

        public void NewRewardCycleGoal(float coinToReward, int levelCoinToReward) {
            float rewardConvert = convertNumber.WithDifUnits_ReturnNumberConvertedToTheMainUnit(coinToReward, levelCoinToReward);
            augmentCoin.AugmentCoinRewardCoin(rewardConvert);
        }

        public float GetNewRewardCar() {
            m_lastRewardCoin = m_multiplicatorControlCoinReward * ControlCoins.Instance.Coins;
            m_lastIndexCoinReward = (int)ControlCoins.Instance.ActualLevelUnits;
            return m_lastRewardCoin;
        }
    }
}
