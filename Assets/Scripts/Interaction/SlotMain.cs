using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Est.Data;
using Est.AI;
using Est.Core;
using System;
using Est.Mobile.Save;

namespace Est.Interact
{
    [RequireComponent(typeof(SlotLocked))]
    public class SlotMain : MonoBehaviour, ISlot, ISlotMain, ISaveable
    {
        public event Action<int, TypeSlotMainBusiness> NewUpgradeLevelSlot = delegate { };
        public SlotInformation slotInformation;

        [SerializeField] int indexSlot = 1;
        [SerializeField] MeshFilter GOSlotGameObjectMesh = null;
        [SerializeField] float _multiplicatorPastValueCostToSlotInformation = 0.1f;

        private float m_lastValueCostUpgradeSlot = 0;
        private int m_lastIndexCostUpgradeSlot = 0;

        //private var

        //general
        private ControlCoins controlCoins;
        private IControlUI controlUI
        {
            get; set;
        }
        private int m_actualLevel = 0;
        private int m_actualExp = 0;
        private int m_coinsPerSecond = 1;
        private long m_CostToUpgradeSlot = 0;
        private int m_indexLevelStringToChangeUnit;

        private SlotLocked slotLocked;
        private ISlotControlUI slotControlUI;
        private SlotType slotType;

        //save
        private bool IsSlotLocked = true;

        private void Start()
        {
            //GetComponent
            controlCoins = ControlCoins.Instance;
            slotLocked = GetComponent<SlotLocked>();
            slotControlUI = GetComponent<SlotControlUI>();
            controlUI = ControlPrincipalUI.Instance;
            slotType = GetComponent<SlotType>();

            //save
            if (indexSlot == 0) IsSlotLocked = false;

            //lock
            if (!IsSlotLocked)
            {
                //unlocked
                slotLocked.changeLockedToUnlocked();
                //controlCoins.SetCoinGenerationPerSecond((int)slotInformation.StatLevel.multiplicatorLevels[m_actualLevel] * m_coinsPerSecond);
                //gameobject
                slotControlUI.changeMeshByUpdateLevel(slotInformation.StatLevel.gameObjectsByLevel[m_actualLevel]);

                //cost
                if (m_CostToUpgradeSlot <= 0)
                {
                    UpgradeDataForThisSlot_DataForSlotInformation();

                }
                slotControlUI.changeTextUpgradeCoin(m_CostToUpgradeSlot, controlCoins.GetStringValueUnitWithIndex(m_indexLevelStringToChangeUnit));
            }
            else
            {
                StartIsSlotLocked_Config();
            }

            //slider
            slotControlUI.changeMaxValueSliderExp(slotInformation.StatLevel.countToUpgradeLevelExp[m_actualLevel]);
            slotControlUI.changeValueSliderExp(m_actualExp);
        }

        public long CostToUpgradeSlot { get => m_CostToUpgradeSlot; }

        public int CostToUpgradeSlotIndex { get => m_indexLevelStringToChangeUnit; }

        public bool GetSlotLockedMain { get { return IsSlotLocked; } }

        public int GetLevel()
        {
            return m_actualLevel;
        }

        public void ChangeValueISlotControlUI(ISlotControlUI uI) {
            slotControlUI = uI;
        }

        public void OnTouchThisObject() {
            if (!IsSlotLocked)
                UpgradeSlot();

            print("OnTouchThisObject" + IsSlotLocked);
            //information
            controlUI.ShowInformationSlot(slotInformation.textInformation, slotInformation.spriteBossSlot, TypeSlot.SlotMainCoin);
        }

        public void ChangeControlUI(IControlUI newControlUI)
        {
            controlUI = newControlUI;
        }

        private void UpgradeSlot()
        {
            if (controlCoins.BuyAThing_CompateWithUnits(m_CostToUpgradeSlot, m_indexLevelStringToChangeUnit))
            {
                //gamemanager
                //controlCoins.SetCoinGenerationPerSecond(controlCoins.WithChangeUnits_ReturnNumberConvertedToTheMainUnit(m_CostToUpgradeSlot, m_indexLevelStringToChangeUnit) * 0.1f);
                CallControlSlotInformationPassInformation(
                    controlCoins.WithDifUnits_ReturnNumberConvertedToTheMainUnit(m_CostToUpgradeSlot, m_indexLevelStringToChangeUnit) * _multiplicatorPastValueCostToSlotInformation,
                    controlCoins.WithDifUnits_ReturnNumberConvertedToTheMainUnit(m_lastValueCostUpgradeSlot, m_lastIndexCostUpgradeSlot) * _multiplicatorPastValueCostToSlotInformation
                    );

                //upgrade coin, save data
                m_lastValueCostUpgradeSlot = CostToUpgradeSlot;
                m_lastIndexCostUpgradeSlot = m_indexLevelStringToChangeUnit;

                //augment cost
                m_CostToUpgradeSlot = m_CostToUpgradeSlot * 2 * (int)slotInformation.StatLevel.multiplicatorLevels[m_actualLevel];

                while (m_CostToUpgradeSlot > 1000)
                {
                    m_CostToUpgradeSlot = (long)m_CostToUpgradeSlot / 1000;

                    m_indexLevelStringToChangeUnit++;
                }

                //change text upgrade
                slotControlUI.changeTextUpgradeCoin(m_CostToUpgradeSlot, controlCoins.GetStringValueUnitWithIndex(m_indexLevelStringToChangeUnit));

                //exp and level
                m_actualExp++;
                slotControlUI.changeValueSliderExp(m_actualExp);
                print("The actual exp is: " + m_actualExp);

                //level
                if (m_actualExp >= slotInformation.StatLevel.countToUpgradeLevelExp[m_actualLevel] &&
                    m_actualLevel < slotInformation.StatLevel.countToUpgradeLevelExp.Length - 1) //level max
                {
                    m_actualLevel++;
                    m_actualExp = 0;

                    //event
                    NewUpgradeLevelSlot.Invoke(m_actualLevel, slotType.GetTypeSlot());

                    //slider
                    slotControlUI.changeMaxValueSliderExp(slotInformation.StatLevel.countToUpgradeLevelExp[m_actualLevel]);

                    //gameobject
                    slotControlUI.changeMeshByUpdateLevel(slotInformation.StatLevel.gameObjectsByLevel[m_actualLevel]);
                }
            }
            else print("Not enough money");
        }

        private void UnlockedSlot()
        {

            //gamemanager
            //controlCoins.SetCoinGenerationPerSecond((int)slotInformation.StatLevel.multiplicatorLevels[m_actualLevel] * m_coinsPerSecond);
            CallControlSlotInformationPassInformation(
                (int)slotInformation.StatLevel.multiplicatorLevels[m_actualLevel] * m_coinsPerSecond, 
                controlCoins.WithDifUnits_ReturnNumberConvertedToTheMainUnit(m_lastValueCostUpgradeSlot, m_lastIndexCostUpgradeSlot)
                    );

            //change value by upgrade
            UpgradeDataForThisSlot_DataForSlotInformation();
            slotControlUI.changeTextUpgradeCoin(m_CostToUpgradeSlot, controlCoins.GetStringValueUnitWithIndex(m_indexLevelStringToChangeUnit));

            //unlocked
            IsSlotLocked = false;

            //mesh
            slotControlUI.changeMeshByUpdateLevel(slotInformation.StatLevel.gameObjectsByLevel[m_actualLevel]);
        }

        private void StartIsSlotLocked_Config()
        {
            //destination locked
            GetComponent<ControlTargetDestinationObject>().Locked = true;

            //change color
            slotControlUI.changeBackGroundColorLocked();

            //event
            slotLocked.OnUnlocked += UnlockedSlot;
        }

        private void UpgradeDataForThisSlot_DataForSlotInformation()
        {
            m_CostToUpgradeSlot = slotInformation.StatLevel.costsToUpgradeLevel;
            m_indexLevelStringToChangeUnit = slotInformation.StatLevel.levelUnitUpgradeLevel;
        }

        private void CallControlSlotInformationPassInformation(float newValue, float oldCost) =>
            ControlSlotInformation.Instance.AugmentGenerationInTypeSlot(slotType.GetTypeSlot(), newValue, oldCost);

        public object CaptureState()
        {
            int[] save = new int[5];
            save[0] = (int)m_CostToUpgradeSlot; //because the number is < 1000, then i can convert it
            save[1] = m_indexLevelStringToChangeUnit;
            save[2] = m_actualExp;
            save[3] = m_actualLevel;
            save[4] = IsSlotLocked ? 1 : 0;

            return save;
        }

        public void RestoreState(object state)
        {
            int[] save = (int[])state;

            m_CostToUpgradeSlot = save[0];
            m_indexLevelStringToChangeUnit =save[1];
            m_actualExp = save[2];
            m_actualLevel = save[3];

            if (save[4] == 0) {
                IsSlotLocked = false;
                StartCoroutine(StartActiveUnlockEventWaitOneFrame());
                GetComponent<SlotControlUI>().changeBackGroundColorLocked();
            }
        }

        public IEnumerator StartActiveUnlockEventWaitOneFrame()
        {
            yield return new WaitForEndOfFrame();
            GetComponent<SlotLocked>().ActivatedUnlockEvent();
        }
    }

    public interface ISlotMain{
    }
}
