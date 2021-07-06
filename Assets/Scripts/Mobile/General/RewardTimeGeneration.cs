using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Est.Data;
using Est.Control;

namespace Est.Mobile
{
    public class RewardTimeGeneration : MonoBehaviour
    {
        [SerializeField] int timeMinimunQuitGame = 20;

        private ICoinsSinceLastTimeConnect controlCoins;
        private IRewardUITime rewardView;
        private int timeLastQuitGame = 0;
        private int rewardByBeingAFK = 0;
        private int limitMaxRewardByBeingAFK = 24;

        private void Awake()
        {
            rewardView = GetComponent<View>();
        }

        // Start is called before the first frame update
        void Start()
        {
            controlCoins = ControlCoins.Instance;
            GetComponent<AdsManager>().VideoIsComplete += ClaimRewardVideoGeneration;
        }

        public void ChangeControlCoin(ICoinsSinceLastTimeConnect newControlCoin) => controlCoins = newControlCoin;

        public void ChangeRewardUIView(IRewardUITime newRewardUITime) => rewardView = newRewardUITime;

        public void rewardCoinsGeneration(int timeQuitGame) {
            print(timeQuitGame + " " + timeMinimunQuitGame);
            if (timeQuitGame >= timeMinimunQuitGame) {
                rewardView.ChangeActiveStateRewardPoster(true);

                timeLastQuitGame = timeQuitGame;
                if (timeLastQuitGame >= limitMaxRewardByBeingAFK) rewardByBeingAFK = limitMaxRewardByBeingAFK;
                else rewardByBeingAFK = timeLastQuitGame;

                rewardView.ChangeTextCoinsRewardPosterTime(
                    MathFunction.ChangeUnitNumberWithString(MultiplicatorNumberByCoinGeneration(rewardByBeingAFK), ""));
            }
        }

        public void ClaimRewardVideoGeneration() {
            print("Time");
            RewardCoinDesactiveObject(MultiplicatorNumberByCoinGeneration(rewardByBeingAFK) * 2);
        }

        public void ClaimRewardNormalGeneration()
        {
            RewardCoinDesactiveObject(MultiplicatorNumberByCoinGeneration(rewardByBeingAFK));
        }

        private void RewardCoinDesactiveObject(float valueToIncrease)
        {
            rewardView.ChangeActiveStateRewardPoster(false);
            UnsubscribeVideoRewardEvent();
            controlCoins.CoinsSinceLastSessionInMinutes(valueToIncrease);
        }

        private float MultiplicatorNumberByCoinGeneration(int number) => (number * ControlCoins.Instance.CoinGenerationSecond);

        private void UnsubscribeVideoRewardEvent() => GetComponent<AdsManager>().VideoIsComplete -= ClaimRewardVideoGeneration;
    }
}
