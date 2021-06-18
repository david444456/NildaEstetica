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
        [SerializeField] AudioSource audio;
        [SerializeField] AudioClip[] clips;

        private bool IsUsed = false;

        public void OnTouchThisObject()
        {
            if (IsUsed) return;

            UIImageInfo.SetActive(false);
            IsUsed = true;
            MediatorMobile.Instance.ActiveMenuRewardCarPlayerSessionView();

            audio.clip = clips[Random.Range(0, clips.Length)];
            audio.Play();
        }

        private void OnEnable()
        {
            audio = GetComponent<AudioSource>();
        }

        private void OnDisable()
        {
            IsUsed = false;
        }

    }
}
