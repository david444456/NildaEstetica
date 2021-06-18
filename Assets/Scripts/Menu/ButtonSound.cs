using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Est.Mobile.Menu
{
    [RequireComponent(typeof(AudioSource))]
    public class ButtonSound : MonoBehaviour
    {
        [SerializeField] AudioClip clip;

        void Start()
        {
            AudioSource audioS = GetComponent<AudioSource>();

            audioS.clip = clip;
            GetComponent<Button>().onClick.AddListener(audioS.Play);
        }
    }
}
