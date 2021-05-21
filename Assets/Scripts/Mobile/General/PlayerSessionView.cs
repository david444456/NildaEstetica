using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Est.Mobile
{
    public class PlayerSessionView : MonoBehaviour
    {
        //public static PlayerSessionView Instance { get; private set; }

        [SerializeField] GameObject MenuRewardCar = null;

        /*private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }*/

        public void ActiveMenuRewardCar(float valueCoin) {
            print("The value to reward's : " +valueCoin);
            MenuRewardCar.SetActive(true);
        }
    }
}
