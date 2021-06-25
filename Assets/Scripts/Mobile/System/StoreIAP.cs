using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Est.Mobile {
    public class StoreIAP : MonoBehaviour
    {
        ControlCoinPremium coinPremium;

        // Start is called before the first frame update
        void Start()
        {
            coinPremium = FindObjectOfType<ControlCoinPremium>();
        }

        public void AugmentPreCoin(int augmentCoin) {
            coinPremium.SetAugmentPremiumCoin(augmentCoin);
            print("Augment premium coin: " + augmentCoin);
        }

    }
}
