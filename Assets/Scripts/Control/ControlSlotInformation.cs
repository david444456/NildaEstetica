using System;
using System.Collections.Generic;
using UnityEngine;
using Est.Interact;
using Est.CycleGoal;

public class ControlSlotInformation : SingletonInInspector<ControlSlotInformation>
{
    public Action<TypeGoal> NewPassLevelSlot = delegate { };

    [Header("Type Slot")]
    [SerializeField] TypeSlotMainBusiness[] countMaxSlot;

    [Header("Count different Slots")]
    [SerializeField] SlotMain[] allSlotsMain;

    Dictionary<TypeSlotMainBusiness, float> dataSlotInfoGeneration = new Dictionary<TypeSlotMainBusiness, float>();
    Dictionary<TypeSlotMainBusiness, int> dataSlotInfoLevelSlotMain = new Dictionary<TypeSlotMainBusiness, int>();
    int actualLevel = 0;

    void Start()
    {
        UpdateDataSlotMainAndTypeSlot();
    }

    public void UpdateDataSlotMainAndTypeSlot()
    {
        if (allSlotsMain.Length <= 0 || allSlotsMain[0] == null)
        {
            allSlotsMain = FindObjectsOfType<SlotMain>();
            if (allSlotsMain.Length <= 0)
                return;
        }
        actualLevel = (int)ControlCoins.Instance.ActualLevelUnits;

        //control change units
        ControlCoins.PassLevelCoin += AugmentIntLevelCoin;

        //control slot when upgrade
        for (int i = 0; i < allSlotsMain.Length; i++)
        {
            allSlotsMain[i].NewUpgradeLevelSlot += AugmentLevelSlotMainByType;
        }

        //when no data
        if (dataSlotInfoGeneration.Count <= 0)
            for (int i = 0; i < countMaxSlot.Length; i++)
            {
                dataSlotInfoGeneration.Add(countMaxSlot[i], 0);
                dataSlotInfoLevelSlotMain.Add(countMaxSlot[i], 0);
            }

        //cycle
        ViewControlCycleGoals.Instance.SubscriptionToEvent(ref NewPassLevelSlot);
    }

    private void OnDisable()
    {
        ControlCoins.PassLevelCoin -= AugmentIntLevelCoin;

        if (allSlotsMain.Length <= 0 || allSlotsMain[0] == null) return;

        //control slot when upgrade
        for (int i = 0; i < allSlotsMain.Length; i++)
        {
            allSlotsMain[i].NewUpgradeLevelSlot -= AugmentLevelSlotMainByType;
        }
    }

    public void AugmentGenerationInTypeSlot(TypeSlotMainBusiness typeSlotMain, float newValue) {
        float oldValueCoin = ControlCoins.Instance.CoinGenerationSecond - dataSlotInfoGeneration[typeSlotMain];
        dataSlotInfoGeneration[typeSlotMain] = newValue;
        ControlCoins.Instance.ChangeCoinGenerationPerSecond(oldValueCoin + dataSlotInfoGeneration[typeSlotMain]);
    }

    public float GetGenerationByIndex(TypeSlotMainBusiness typeSlotMain) =>
        dataSlotInfoGeneration[typeSlotMain];

    public float GetLevelOfSlotByIndex(TypeSlotMainBusiness typeSlotMain) =>
        dataSlotInfoGeneration[typeSlotMain];

    private void AugmentIntLevelCoin(int newLevel) {
        for (int i = 0; i < countMaxSlot.Length; i++)
        {
            dataSlotInfoGeneration[countMaxSlot[i]] = 
                ControlCoins.Instance.WithDifUnits_ReturnNumberConvertedToTheMainUnit(dataSlotInfoGeneration[countMaxSlot[i]], actualLevel);
        }

        actualLevel = newLevel;
    }

    private void AugmentLevelSlotMainByType(int newLevel, TypeSlotMainBusiness typeSlotMainBusiness)
    {
        if (newLevel < dataSlotInfoLevelSlotMain[typeSlotMainBusiness]) Debug.LogError("Error level int: the new level is smaller than the actual value.");


        dataSlotInfoLevelSlotMain[typeSlotMainBusiness] = newLevel;
        NewPassLevelSlot.Invoke(TypeGoal.Slot);
    }
}
