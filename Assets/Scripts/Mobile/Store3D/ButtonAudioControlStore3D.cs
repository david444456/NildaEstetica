using Est.CycleGoal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Est.Mobile.Store
{
    public class ButtonAudioControlStore3D : MonoBehaviour
    {
        [SerializeField] AudioSource audioSource;
        [SerializeField] AudioClip audioClip;
        void Start()
        {
            if (audioSource == null) audioSource = GetComponent<AudioSource>();
            audioSource.clip = audioClip;
            GetComponent<ControlStore3D>().NewPurchasedItem3D += PlayAudioSource;
        }

        private void PlayAudioSource(TypeGoal typeGoal) {
            audioSource.Play();
        }
    }
}