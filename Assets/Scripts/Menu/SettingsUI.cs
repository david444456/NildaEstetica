using Est.Mobile;
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

    [Header("Remove Ads")]
    [SerializeField] Image imageButtonRemoveAds;

    AdsManager adsManager;
    bool removeAds = false;

    private void Start()
    {
        //ads
         adsManager = FindObjectOfType<AdsManager>();
        removeAds = adsManager.GetRemoveAdsBool();

        if(removeAds) ChangeButtonAdsToUntouchable();

        //audio
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
        ChangeButtonAdsToUntouchable();
        adsManager.SetAdsBoolTrue();
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

    private void ChangeButtonAdsToUntouchable() {
        imageButtonRemoveAds.color = new Color(imageButtonRemoveAds.color.r, imageButtonRemoveAds.color.g, imageButtonRemoveAds.color.b,  0.5f);
        imageButtonRemoveAds.GetComponent<Button>().enabled = false;
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
