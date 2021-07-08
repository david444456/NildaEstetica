using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Est.Mobile
{
    public interface IChangeTextCoinsToBuy 
    {
        void UpdateTextButtonTotalCostInStartStore(float[] intCostTotal);
        void ChangeTextCoinsToBuy(int index, string newText);

        void PlayAudioSourceBuy();
    }
}