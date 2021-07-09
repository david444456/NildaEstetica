using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Est.Mobile.Save;

namespace Est.Mobile
{
    //this control everything in mobile,( ads ),
    public class AdsManager : MonoBehaviour, ISaveable, IVideoReward
    {
        public Action VideoIsComplete = delegate { };

        [SerializeField] int timeForSaveRecompenseHours = 24;
        [SerializeField] int timeMinimunInMinutesByReward = 0;

        [Header("Ads")]
        [SerializeField] string placementNormalVideoID = "video"; //ID del anuncio
        [SerializeField] string placementRewardedID = "rewardedVideo"; //ID del anuncio

        private PlayerSessionView playerSession;
        private RewardTimeGeneration rewardTimeGeneration;
        private ADSRewardedVideo aDSRewardedVideo;
        private bool adsRemove = false;

        void Start()
        {
            playerSession = GetComponent<PlayerSessionView>();
            rewardTimeGeneration = GetComponent<RewardTimeGeneration>();

            //set ads
            aDSRewardedVideo = new ADSRewardedVideo(placementRewardedID);
            aDSRewardedVideo.ICompleteVideo += CompleteVideoReward;

            //reward
            StartCoroutine(RewardVerification());
        }

        private void OnDisable()
        {
            if(aDSRewardedVideo != null) aDSRewardedVideo.ICompleteVideo -= CompleteVideoReward;
        }

        public bool GetRemoveAdsBool() => adsRemove;

        public void SetAdsBoolTrue() => adsRemove = true;

        public void InstantiateVideoReward() {
            aDSRewardedVideo.ShowVideo();
        }

        private void CompleteVideoReward() {
            print("Video complete");
            VideoIsComplete.Invoke();
        }

        IEnumerator RewardVerification() {
            yield return new WaitForSeconds(1/2);
            if (playerSession.GetTimeMinute() > timeMinimunInMinutesByReward && playerSession.GetTimeHour() >= timeForSaveRecompenseHours)
            {
                rewardTimeGeneration.rewardCoinsGeneration((int)Mathf.Round(playerSession.GetTimeHour())); //
            }
        }

        public object CaptureState()
        {
            return adsRemove;
        }

        public void RestoreState(object state)
        {
            adsRemove = (bool)state;
        }
    }
}
