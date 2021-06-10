using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCoinPremium : MonoBehaviour
{
    private int _actualPremiumCoin = 0;

    IControlUI principalUI;

    void Start()
    {
        principalUI = GetComponent<ControlPrincipalUI>();
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
}
