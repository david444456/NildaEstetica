using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Est.Mobile {
    public class ViewStorePremiumCoin : MonoBehaviour, IChangeTextCoinsToBuy
    {
        [Header("UI")]
        [SerializeField] Text[] textButtonTotalCost;
        [SerializeField] Text[] textTotalCoinsGetBuy;
        [SerializeField] AudioSource audioSource;

        public void UpdateTextButtonTotalCostInStartStore(int [] intCostTotal) {
            for (int i = 0; i < intCostTotal.Length; i++)
            {
                print(intCostTotal[i].ToString());
                textButtonTotalCost[i].text = intCostTotal[i].ToString();
            }
        }

        public void ChangeTextCoinsToBuy(int index, string newText) {
            textTotalCoinsGetBuy[index].text = newText;
        }

        public void PlayAudioSourceBuy() => audioSource.Play();

    }
}
