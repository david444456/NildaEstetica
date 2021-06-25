
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Est.Data;
using Est.Mobile.Save;

namespace Est.Interact
{
    [RequireComponent(typeof(SlotLocked))]
    public class SlotAdvertising : MonoBehaviour, ISlot, ISaveable
    {
        [SerializeField] int multiplicatorAdvertising = 2;

        [Header("UI")]
        [SerializeField] [TextArea] string stringAdvertising = null;
        [SerializeField] Sprite spriteAdvertising = null;
        [SerializeField] GameObject GOAdvertisingUIText = null;
        [SerializeField] Color colorAfterLockCoinsNeeded = Color.white;

        //general
        private ControlCoins controlCoins;
        private SlotLocked slotLocked;
        private SlotControlUI slotControlUI;
        private bool IsSlotLocked = true;

        public bool ThisSlotIsLocked { get => IsSlotLocked; }

        private IControlUI controlUI
        {
            get; set;
        }

        void Start()
        {
            //references
            controlCoins = ControlCoins.Instance;
            slotLocked = GetComponent<SlotLocked>();
            slotControlUI = GetComponent<SlotControlUI>();
            controlUI = ControlPrincipalUI.Instance;

            //ui
            slotControlUI.changeTextUpgradeCoin(slotLocked.costToUnlockedSlot, controlCoins.GetStringValueUnitWithIndex(slotLocked.indexActualLevelUnit));
            if(!IsSlotLocked) DesactiveUIGOAdTextInfo();


            //unlocked
            slotLocked.OnUnlocked += UnlockedSlot;
        }

        public void ChangeControlUI(IControlUI newControlUI) {
            controlUI = newControlUI;
        }

        public void OnTouchThisObject()
        {
            if (IsSlotLocked)
            {
                //information
                controlUI.ShowInformationSlot(stringAdvertising, spriteAdvertising, TypeSlot.SlotAdvertising);
            }
        }

        private void UnlockedSlot()
        {
            controlCoins.MultiplyCoinGenerationPerSecond(controlCoins.CoinGenerationSecond * multiplicatorAdvertising);
            IsSlotLocked = false;

            //ui
            DesactiveUIGOAdTextInfo();
        }

        private void DesactiveUIGOAdTextInfo()
        {
            GOAdvertisingUIText.SetActive(false);
        }

        public object CaptureState()
        {
            int value = 0;
            if (IsSlotLocked) value = 1;
            return value;
        }

        public void RestoreState(object state)
        {
            if ((int)state == 0) IsSlotLocked = false;
        }
    }
}
