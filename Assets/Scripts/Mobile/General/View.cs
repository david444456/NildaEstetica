using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Est.Mobile
{
    public class View : MonoBehaviour
    {

        [SerializeField] GameObject MenuRewardCar = null;

        void Start()
        {

        }

        public void ActiveMenuRewardCar(float valueCoin)
        {
            print("The value to reward's : " + valueCoin);
            MenuRewardCar.SetActive(true);
        }

    }
}
