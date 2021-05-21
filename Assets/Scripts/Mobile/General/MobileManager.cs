using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Est.Mobile
{
    //this control everything in mobile,( ads ),
    public class MobileManager : MonoBehaviour
    {
        public Action VideoIsComplete = delegate { };

        [SerializeField] int timeForSaveRecompenseHours = 24;

        private PlayerSession playerSession;
        private RewardTimeGeneration rewardTimeGeneration;

        void Start()
        {
            playerSession = GetComponent<PlayerSession>();
            rewardTimeGeneration = GetComponent<RewardTimeGeneration>();

            //reward
            StartCoroutine(RewardVerification());
        }

        public void InstantiateVideoReward() {
            print("Video reward");
            VideoIsComplete.Invoke();
        }

        IEnumerator RewardVerification() {
            yield return new WaitForSeconds(1/2);
            if (playerSession.GetTimeMinute() > 0 && playerSession.GetTimeHour() < timeForSaveRecompenseHours)
            {
                rewardTimeGeneration.rewardCoinsGeneration((int)Mathf.Round(playerSession.GetTimeMinute()));
            }
            else if (playerSession.GetTimeHour() >= 24) {
                //daily reward
            }
        }
    }
}
