using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Est.Interact;

namespace Est.Mobile
{
    public class AnimationImageSlot : MonoBehaviour
    {
        [SerializeField] Image imageBackGroundSlot;
        [SerializeField] SlotMain slotMainAnimation;
        [SerializeField] float timeAnimation = 0.5f;


        void Start()
        {
            DOTween.Init();

            slotMainAnimation.NewUpgradeLevelSlot += StartAnimationScale;
        }

        private void StartAnimationScale(int value, TypeSlotMainBusiness typeSlot) {
            imageBackGroundSlot.GetComponent<RectTransform>().DOScale(new Vector3(0.02f, 0.02f, 0.01f), timeAnimation/2);
            imageBackGroundSlot.GetComponent<RectTransform>().DOScale(new Vector3(0.01f, 0.01f, 0.01f), timeAnimation/2).SetDelay(timeAnimation/2);
        }
    }
}
