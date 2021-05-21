using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Est.Data;
using Est.AI;

namespace Est.Interact
{
    public class SlotLocked : MonoBehaviour, ISlot
    {
        public event Action OnUnlocked = delegate { };
        public long costToUnlockedSlot = 100;
        public int indexActualLevelUnit = 0;

        //save
        private bool slotLocked = true;
        private ControlCoins controlCoins;
        private SlotMain slotMain;

        void Start()
        {
            if (GetComponent<SlotMain>() != null)
            {
                slotMain = GetComponent<SlotMain>();

                ChangeValuesToUnlockObjectSlotMain();
            }

            //
            controlCoins = ControlCoins.Instance;


            if (!slotLocked)
            {
                ActivatedUnlockEvent();
            }
            else
            {
                GetComponent<SlotControlUI>().changeTextUpgradeCoin(costToUnlockedSlot, controlCoins.GetStringValueUnitWithIndex(indexActualLevelUnit));
                //locked
            }
        }

        public void OnTouchThisObject()
        {
            if (slotLocked)
            {
                print(controlCoins.Coins + " actual lock:" + costToUnlockedSlot + " :" + indexActualLevelUnit);
                if (controlCoins.BuyAThing_CompateWithUnits(costToUnlockedSlot, indexActualLevelUnit))
                {
                    ActivatedUnlockEvent();
                    print("SlotLocked");
                    //upgrade coin
                    //controlCoins.SetCoin(-costToUnlockedSlot);

                    slotLocked = false;

                }

            }
        }

        public void ActivatedUnlockEvent() {
            OnUnlocked();
        }

        public void changeLockedToUnlocked() {
            slotLocked = false;
        }

        public bool GetSlotLocked() {
            return slotLocked;
        }

        private void ChangeValuesToUnlockObjectSlotMain()
        {
            //locked {
            costToUnlockedSlot = slotMain.slotInformation.StatLevel.costsToUnlockedSlot;
            indexActualLevelUnit = slotMain.slotInformation.StatLevel.levelUnitUnlockedSlot;
        }
    }
}
