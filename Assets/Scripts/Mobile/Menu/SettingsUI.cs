using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] GameObject GOSettingsUI= null;

    AudioSource[] audioS = null;
    private bool ActiveSound = true;

    private void Start()
    {
        audioS = FindObjectsOfType<AudioSource>();
    }

    public void changeStateSettingUI(bool changeBool) {
        GOSettingsUI.SetActive(changeBool);
    }

    public void ExitToSetting() {
        GOSettingsUI.SetActive(false);
    }

    public void ActiveOrDesactiveSound() {
        if (ActiveSound)
        {
            GetAudioSourcesAndChangeVolume(0);
        }
        else
            GetAudioSourcesAndChangeVolume(1);

        ActiveSound = !ActiveSound;
    }

    void GetAudioSourcesAndChangeVolume(int vol) {
        print(vol);
        foreach (AudioSource audio in audioS) {
            audio.volume = vol;
        }
    }
}
