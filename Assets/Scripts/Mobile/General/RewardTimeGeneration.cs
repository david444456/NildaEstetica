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

        // Start is called before the first frame update
        void Start()
        {
            controlCoins = ControlCoins.Instance;
        }

        public void rewardCoinsGeneration(int timeQuitGame) {
            if (timeQuitGame >= timeMinimunQuitGame) {
                gameObjectRewardPoster.SetActive(true);
                
                timeLastQuitGame = timeQuitGame;

                textCoinsReward.text = MathFunction.ChangeUnitNumberWithString(timeLastQuitGame * 60 * controlCoins.CoinGenerationSecond, "");
            }
        }

        public void ClaimRewardVideoGeneration() {
            gameObjectRewardPoster.SetActive(false);
            controlCoins.CoinsSinceLastSessionInMinutes(timeLastQuitGame*2);
        }

        public void ClaimRewardNormalGeneration() {
            gameObjectRewardPoster.SetActive(false);
            controlCoins.CoinsSinceLastSessionInMinutes(timeLastQuitGame);
        }
    }
}
