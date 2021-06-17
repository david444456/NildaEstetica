using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Est.Mobile
{
    public class MediatorMobile : MonoBehaviour
    {
        public static MediatorMobile Instance { get; private set; }

        private AdsManager mobileManager = null;
        private View view;
        private ControlRewardCoin controlRewardCoin;

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


            mobileManager = GetComponent<AdsManager>();
            view = GetComponent<View>();
            controlRewardCoin = GetComponent<ControlRewardCoin>();

        }

        public void VideoRewardCar()
        {
            mobileManager.VideoIsComplete += OnRewardCar;
            mobileManager.InstantiateVideoReward(); //this is when touch in diff objects

        }

        public void ActiveMenuRewardCarPlayerSessionView() => view.ActiveMenuRewardCar(controlRewardCoin.GetNewRewardCar());

        public void NewReclaimedRewardCycleGoal(float coinToReward, int levelCoinToReward) => controlRewardCoin.NewRewardCycleGoal(coinToReward, levelCoinToReward);

        private void OnRewardCar()
        {
            print("OnRewardCar");
            controlRewardCoin.NewRewardCar();
            mobileManager.VideoIsComplete -= OnRewardCar;
        }
    }
}
