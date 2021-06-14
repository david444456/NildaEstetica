using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Est.Data;

namespace Est.Mobile
{
    public class RewardTimeGeneration : MonoBehaviour
    {
        [SerializeField] int timeMinimunQuitGame = 20;

        [Header("UI")]
        [SerializeField] GameObject gameObjectRewardPoster = null;
        [SerializeField] Text textCoinsReward = null;

        private ControlCoins controlCoins;
        private int timeLastQuitGame = 0;
        private int rewardByBeingAFK = 0;
        private int limitMaxRewardByBeingAFK = 24;

        // Start is called before the first frame update
        void Start()
        {
            controlCoins = ControlCoins.Instance;
        }

        public void rewardCoinsGeneration(int timeQuitGame) {
            if (timeQuitGame >= timeMinimunQuitGame) {
                gameObjectRewardPoster.SetActive(true);
                
                timeLastQuitGame = timeQuitGame;
                if (timeLastQuitGame >= limitMaxRewardByBeingAFK) rewardByBeingAFK = limitMaxRewardByBeingAFK;
                else rewardByBeingAFK = timeLastQuitGame;

                textCoinsReward.text = MathFunction.ChangeUnitNumberWithString(MultiplicatorNumberByCoinGeneration(rewardByBeingAFK), "");
            }
        }

        public void ClaimRewardVideoGeneration() {
            gameObjectRewardPoster.SetActive(false);
            controlCoins.CoinsSinceLastSessionInMinutes(MultiplicatorNumberByCoinGeneration(rewardByBeingAFK) * 2);
        }

        public void ClaimRewardNormalGeneration() {
            gameObjectRewardPoster.SetActive(false);
            controlCoins.CoinsSinceLastSessionInMinutes(MultiplicatorNumberByCoinGeneration(rewardByBeingAFK));
        }

        private float MultiplicatorNumberByCoinGeneration(int number) => (number * controlCoins.CoinGenerationSecond);
    }
}
