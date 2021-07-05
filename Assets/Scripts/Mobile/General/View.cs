using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Est.Mobile
{
    public class View : MonoBehaviour, IRewardUITime
    {
        [SerializeField] Button buttonRewardCar;

        [Header("Car reward")]
        [SerializeField] GameObject MenuRewardCar = null;
        [SerializeField] Text textRewardCar = null;

        [Header("UI Reward Time Generation")]
        [SerializeField] GameObject gameObjectRewardPoster = null;
        [SerializeField] Text textCoinsReward = null;

        MediatorMobile mediator;

        void Start()
        {
            mediator = GetComponent<MediatorMobile>();

            buttonRewardCar.onClick.AddListener(ActiveNewRewardVideo);
        }

        public void ActiveNewRewardVideo() {
            mediator.VideoRewardCar();
        }

        public void ActiveMenuRewardCar(float valueCoin)
        {
            print("The value to reward's : " + valueCoin);
            MenuRewardCar.SetActive(true);
            textRewardCar.text = valueCoin.ToString();

        }

        public void ChangeActiveStateRewardPoster(bool newValue) => gameObjectRewardPoster.SetActive(newValue);

        public void ChangeTextCoinsRewardPosterTime(string newText) => textCoinsReward.text = newText;
    }
}
