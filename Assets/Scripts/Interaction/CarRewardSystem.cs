using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Est.Mobile;
using UnityEngine.UI;

namespace Est.Interact
{
    public class CarRewardSystem : MonoBehaviour, ISlot
    {
        [SerializeField] GameObject UIImageInfo = null;
        [SerializeField] Text textUIImageInfoAboutCoin;

        private bool IsUsed = false;

        public void OnTouchThisObject()
        {
            if (IsUsed) return;

            UIImageInfo.SetActive(false);
            IsUsed = true;
            MediatorMobile.Instance.ActiveMenuRewardCarPlayerSessionView();
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            IsUsed = false;
        }

    }
}
