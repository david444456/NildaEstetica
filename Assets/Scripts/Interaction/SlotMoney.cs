using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Est.Data;
using UnityEngine.UI;

namespace Est.Interact
{
    public class SlotMoney : MonoBehaviour, ISlot
    {

        private ControlCoins controlCoins;
        private int m_coinGenerationIndex;


        private void Start()
        {
            controlCoins = ControlCoins.Instance;
        }

        public void OnTouchThisObject() {
            controlCoins.ClickCoinPerSecond();
        }
    }
}
