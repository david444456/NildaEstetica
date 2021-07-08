using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Est.Mobile
{
    public class MediatorMobile : MonoBehaviour
    {
        public static MediatorMobile Instance { get; private set; }

        private AdsManager AdsManager = null;
        private IVideoReward adsInstantiate;
        private IMenuReward view;
        private IControlRewardCoin controlRewardCoin;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }


            AdsManager = GetComponent<AdsManager>();
            adsInstantiate = AdsManager;
            view = GetComponent<View>();
            controlRewardCoin = GetComponent<ControlRewardCoin>();

        }

        public void ChangeVideoRewardInterface(IVideoReward reward) => adsInstantiate = reward;

        public void ChangeControlRewardCoinInterface(IControlRewardCoin rewardCoin) => controlRewardCoin = rewardCoin;

        public void ChangeIMenuRewardInterface(IMenuReward menuReward) => view = menuReward;

        public void VideoRewardCar()
        {
            AdsManager.VideoIsComplete += OnRewardCar;
            adsInstantiate.InstantiateVideoReward(); //this is when touch in diff objects
        }

        public void ActiveMenuRewardCarPlayerSessionView() => view.ActiveMenuRewardCar(controlRewardCoin.GetNewRewardCar());

        public void NewReclaimedRewardCycleGoal(float coinToReward, int levelCoinToReward) => controlRewardCoin.NewRewardCycleGoal(coinToReward, levelCoinToReward);

        private void OnRewardCar()
        {
            print("OnRewardCar");
            controlRewardCoin.NewRewardCar();
            AdsManager.VideoIsComplete -= OnRewardCar;
        }
    }
}
