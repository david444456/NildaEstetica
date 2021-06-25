
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Est.Interact
{
    public class SlotControlUI : MonoBehaviour, ISlotControlUI
    {
        [Header("General")]
        [SerializeField] SlotMain slotMain = null;
        [SerializeField] private bool boolSlotLocked = true;

        [Header("UI")]
        [SerializeField] Text textUpgradeCoin = null;
        [SerializeField] Slider sliderLevelExp = null;
        [SerializeField] Image imageBackGroundCoin = null;
        [SerializeField] Color colorBackGroundCoinLocked = Color.yellow;
        [SerializeField] Color colorBackGroundCoinAfterUnlocked = Color.white;
        [SerializeField] MeshFilter GOSlotGameObjectMesh = null;

        private ControlCoins controlCoins;
        private SlotLocked slotLocked;

        private bool colorIsGreen = false;
        private bool verifySlotLocked = false;

        private void Awake()
        {
            //gameManager
            controlCoins = ControlCoins.Instance;
            slotLocked = GetComponent<SlotLocked>();
            //event
            slotLocked.OnUnlocked += UnlockedSlot;
        }

        private void LateUpdate()
        {
            if (boolSlotLocked)
                verifySlotLocked = controlCoins.CompareWithUnits_IfCoinsIsMostBiggerThanPassParameter(slotLocked.costToUnlockedSlot, slotLocked.indexActualLevelUnit);
            else if (slotMain != null)
                verifySlotLocked = controlCoins.CompareWithUnits_IfCoinsIsMostBiggerThanPassParameter(slotMain.CostToUpgradeSlot, slotMain.CostToUpgradeSlotIndex);
            else
                verifySlotLocked = false;

            if (verifySlotLocked)
            {
                if (!colorIsGreen)
                {
                    changeBackGroundAfterUnlocked();
                }
            }
            else if (colorIsGreen)
            {
                changeBackGroundColorLocked();
            }

        }

        public void changeBackGroundColorLocked()
        {
            colorIsGreen = false;
            imageBackGroundCoin.color = colorBackGroundCoinLocked;
        }

        public void changeBackGroundAfterUnlocked()
        {
            colorIsGreen = true;
            imageBackGroundCoin.color = colorBackGroundCoinAfterUnlocked;
        }

        public void changeTextUpgradeCoin(long newValueCoin, string newStrinsUnit)
        {
            textUpgradeCoin.text = MathFunction.ChangeUnitNumberWithString(newValueCoin, newStrinsUnit);
        }

        public void changeMaxValueSliderExp(int maxValueSlider)
        {
            sliderLevelExp.maxValue = maxValueSlider;
        }

        public void changeValueSliderExp(int newValueSlider)
        {
            sliderLevelExp.value = newValueSlider;
        }

        public void changeMeshByUpdateLevel(Mesh meshFilter)
        {
            //mesh
            GOSlotGameObjectMesh.mesh = meshFilter;
        }

        private void UnlockedSlot()
        {
            //change color
            boolSlotLocked = false;
            changeBackGroundAfterUnlocked();
        }
    }

    public interface ISlotControlUI
    {
        void changeBackGroundColorLocked();
        void changeBackGroundAfterUnlocked();
        void changeTextUpgradeCoin(long newValueCoin, string newStrinsUnit);
        void changeMaxValueSliderExp(int maxValueSlider);
        void changeValueSliderExp(int newValueSlider);
        void changeMeshByUpdateLevel(Mesh meshFilter);
    }
}
