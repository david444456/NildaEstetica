using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Est.Mobile
{
    public class StorePremiumCoin : MonoBehaviour
    {
        [SerializeField] int _multiplicator = 10;

        ControlCoinPremium coinPremium;
        IAugmentCoin ControlCoin;
        void Start()
        {
            coinPremium = FindObjectOfType<ControlCoinPremium>();
            ControlCoin = ControlCoins.Instance;
        }

        public void ChangeControlCoin(IAugmentCoin augmentCoin) {
            ControlCoin = augmentCoin;
        }

        public void TryToBuyPremiumCoin(int gemsByBuy) {

            if (gemsByBuy <= coinPremium.CoinsPremium) {
                print("Desactive store UI, ");
                ControlCoin.AugmentCoinRewardCoin(gemsByBuy * _multiplicator);
            }
        }

    }
}
