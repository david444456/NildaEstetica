using Est.Control;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Est.Mobile
{
    public class StorePremiumCoin : MonoBehaviour
    {

        [SerializeField] int[] costCoinPremium;
        [SerializeField] int _multiplicator = 10;

        [Header("UI")]
        [SerializeField] Button[] buttonsBuyStore;

        float[] totalCoinToBuy = new float[4];
        int[] unityCoinToBuy = new int[4];
        int limitItemsInStore = 4;

        ControlCoinPremium coinPremium;

        IChangeTextCoinsToBuy viewStore;
        IAugmentCoin ControlCoin;
        ISetControlPremiumCoin setControlPremium;
        void Start()
        {
            coinPremium = FindObjectOfType<ControlCoinPremium>();
            setControlPremium = FindObjectOfType<ControlCoinPremium>();
            viewStore = GetComponent<ViewStorePremiumCoin>();
            ControlCoin = ControlCoins.Instance;

            totalCoinToBuy = new float[4];

            viewStore.UpdateTextButtonTotalCostInStartStore(costCoinPremium);
            /*for (int i = 0; i < totalCoinToBuy.Length; i++) {
                textButtonTotalCost[i].text = costCoinPremium[i].ToString();
            }*/
        }

        /// <summary>
        /// Only use in tests
        /// </summary>
        /// <param name="augmentCoin"></param>
        public void ChangeControlCoin(IAugmentCoin augmentCoin) => ControlCoin = augmentCoin;

        /// <summary>
        /// Only use in tests
        /// </summary>
        /// <param name="augmentCoin"></param>
        public void ChangeSetControlPremiumCoin(ISetControlPremiumCoin setControlPremiumCoin) => setControlPremium = setControlPremiumCoin;

        public void ChangeViewStoreInterface(IChangeTextCoinsToBuy changeTextCoinsToBuy) => viewStore = changeTextCoinsToBuy;

        public void ActiveStore()
        {
            //buttons
            buttonsBuyStore[0].onClick.AddListener(() => TryToBuyPremiumCoin(0));
            buttonsBuyStore[1].onClick.AddListener(() => TryToBuyPremiumCoin(1));
            buttonsBuyStore[2].onClick.AddListener(() => TryToBuyPremiumCoin(2));
            buttonsBuyStore[3].onClick.AddListener(() => TryToBuyPremiumCoin(3));

            ResetTheValueForBuy();
        }

        public void TryToBuyPremiumCoin(int index) {
            if (costCoinPremium[index] <= coinPremium.CoinsPremium) {
                ResetTheValueForBuy();
                setControlPremium.SetAugmentPremiumCoin(-costCoinPremium[index]);
                ControlCoin.AugmentCoinRewardCoin(ControlCoins.Instance.WithDifUnits_ReturnNumberConvertedToTheMainUnit( 
                                                    totalCoinToBuy[index], unityCoinToBuy[index]));
                viewStore.PlayAudioSourceBuy();
            }
        }

        private void ResetTheValueForBuy()
        {
            for (int i = 0; i < limitItemsInStore; i++)
            {
                SetTheValueCoinsToBuy(i);
            }
        }

        private void SetTheValueCoinsToBuy(int index)
        {
            totalCoinToBuy[index] = costCoinPremium[index] * ControlCoins.Instance.CoinGenerationSecond;
            totalCoinToBuy[index] = (float)Math.Round(totalCoinToBuy[index], 3);
            unityCoinToBuy[index] = (int)ControlCoins.Instance.ActualLevelUnits;

            //view
            viewStore.ChangeTextCoinsToBuy(index, totalCoinToBuy[index].ToString() + " " + ControlCoins.Instance.ActualLevelUnitsString);
        }

        private void BuyFirstPositionOnStore() => TryToBuyPremiumCoin(0);
        private void BuySecondPositionOnStore() => TryToBuyPremiumCoin(1);
        private void BuyThirdPositionOnStore() => TryToBuyPremiumCoin(2);
        private void BuyForthPositionOnStore() => TryToBuyPremiumCoin(3);
    }
}
