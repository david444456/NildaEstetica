using Est.Control;
using Est.Mobile.Save;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCoinPremium : MonoBehaviour, ISaveable, ISetControlPremiumCoin
{
    private int _actualPremiumCoin = 0;

    IControlUI principalUI;

    void Start()
    {
        principalUI = FindObjectOfType<ControlPrincipalUI>();
        principalUI.changeTextCoinPremium(_actualPremiumCoin);
    }

    public void ChangePrincipalUI(IControlUI newControlUI)
    {
        principalUI = newControlUI;
    }

    public int CoinsPremium { get => _actualPremiumCoin; }

    public void SetAugmentPremiumCoin(int augmentValue)
    {
        _actualPremiumCoin += augmentValue;
        principalUI.changeTextCoinPremium(_actualPremiumCoin);
    }

    public object CaptureState()
    {
        return _actualPremiumCoin;
    }

    public void RestoreState(object state)
    {
        _actualPremiumCoin = (int)state;
    }
}
