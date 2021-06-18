using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] GameObject GOSettingsUI= null;

    AudioSource[] audioS = null;
    float[] controlVolume;
    private bool ActiveSound = true;

    private void Start()
    {
        audioS = FindObjectsOfType<AudioSource>();
        controlVolume = new float[audioS.Length];

        for (int i = 0; i < controlVolume.Length; i++) {
            controlVolume[i] = audioS[i].volume;
        }
    }

    public void changeStateSettingUI(bool changeBool) {
        GOSettingsUI.SetActive(changeBool);
    }

    public void RemoveAds() {
        ExitToSetting();
        print("RemoveAds");
    }

    public void ExitToSetting() {
        GOSettingsUI.SetActive(false);
    }

    public void ActiveOrDesactiveSound() {
        if (ActiveSound)
        {
            GetAudioSourcesAndChangeVolume(false);
        }
        else
            GetAudioSourcesAndChangeVolume(true);

        ActiveSound = !ActiveSound;
    }

    void GetAudioSourcesAndChangeVolume(bool vol) {
        print(vol);
        if (vol) ReturnVolumeObjects();
        else
        {
            foreach (AudioSource audio in audioS)
            {
                audio.volume = 0;
            }
        }
    }

    private void ReturnVolumeObjects() {
        for (int i = 0; i < controlVolume.Length; i++)
        {
            audioS[i].volume = controlVolume[i];
        }
    }
}
