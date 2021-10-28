using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Est.Mobile;
using UnityEngine.UI;

namespace Est.Interact
{
    [RequireComponent(typeof(AudioSource))]
    public class CarRewardSystem : MonoBehaviour, ISlot
    {
        [SerializeField] GameObject UIImageInfo = null;
        [SerializeField] Text textUIImageInfoAboutCoin;

        [Header("Audio")]
        [SerializeField] AudioSource audioSourceReward;
        [SerializeField] AudioClip[] clipsReward;

        private bool IsUsed = false;

        public void OnTouchThisObject()
        {
            if (IsUsed) return;

            UIImageInfo.SetActive(false);
            IsUsed = true;
            MediatorMobile.Instance.ActiveMenuRewardCarPlayerSessionView();

            audioSourceReward.clip = clipsReward[Random.Range(0, clipsReward.Length)];
            audioSourceReward.Play();
        }

        private void OnEnable()
        {
            audioSourceReward = GetComponent<AudioSource>();
        }

        private void OnDisable()
        {
            IsUsed = false;
        }

    }
}
