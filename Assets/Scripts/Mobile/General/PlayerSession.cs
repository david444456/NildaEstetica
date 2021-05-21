using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Est.Mobile
{
    public class PlayerSession : MonoBehaviour
    {
        public static PlayerSession Instance { get; private set; }

        private MobileManager mobileManager = null;
        private TimeSpan timeSpan;
        private PlayerSessionView view;
        private ControlRewardCoin controlRewardCoin;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            mobileManager = GetComponent<MobileManager>();
            view = GetComponent<PlayerSessionView>();
            controlRewardCoin = GetComponent<ControlRewardCoin>();
        }

        void Start()
        {
            Debug.Log("app start");

            string dateQuitString = PlayerPrefs.GetString("dateQuit", "");
            if (!dateQuitString.Equals("")) {
                DateTime dateQuit = DateTime.Parse(dateQuitString);
                DateTime dateNow = DateTime.Now;

                if (dateNow > dateQuit) {
                    timeSpan = dateNow - dateQuit;
                    print(timeSpan.TotalSeconds);
                }

                PlayerPrefs.SetString("dateQuit", "");
            }

        }

        public void VideoRewardCar() {
            mobileManager.VideoIsComplete += OnRewardCar;
            mobileManager.InstantiateVideoReward(); //this is when touch in diff objects

        }

        public void ActiveMenuRewardCarPlayerSessionView() => view.ActiveMenuRewardCar(controlRewardCoin.GetNewRewardCar());

        public void NewReclaimedRewardCycleGoal(float coinToReward, int levelCoinToReward) => controlRewardCoin.NewRewardCycleGoal(coinToReward, levelCoinToReward);

        public float GetTimeHour() {
            return (float)timeSpan.TotalHours;
        }

        public float GetTimeMinute()
        {
            return (float)timeSpan.TotalSeconds;
        }

        private void OnApplicationQuit()
        {
            Debug.Log("app quit");
            DateTime quitDateTime = DateTime.Now;
            PlayerPrefs.SetString("dateQuit", quitDateTime.ToString());

            print(quitDateTime.ToString());
        }

        private void OnRewardCar() {
            print("OnRewardCar");
            mobileManager.VideoIsComplete -= OnRewardCar;
        }
    }
}
