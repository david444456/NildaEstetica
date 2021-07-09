using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Est.Mobile
{
    public interface IChangeTextCoinsToBuy 
    {
        void UpdateTextButtonTotalCostInStartStore(int[] intCostTotal);
        void ChangeTextCoinsToBuy(int index, string newText);

        void PlayAudioSourceBuy();
    }
}