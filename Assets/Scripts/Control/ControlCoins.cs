using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Est.Interact;
using UnityEngine.UI;
using Est.Data;
using Est.Mobile;
using System;
using Est.Mobile.Save;

//gamemanager control coins
public class ControlCoins : SingletonInInspector<ControlCoins>, ISaveable
{
    public static event Action<int> PassLevelCoin = delegate { };

    #region Private Var

    [Header("General")]
    [SerializeField] private float _coins = 0f;

    [Header("Change units")]
    [SerializeField] string[] unitsStringValue = null; //needs change to scriptableObjects

    private float _coinGenerationPerSecond = 1;
    private int actualLevelOfCoinUnits = 0;

    private float m_actualTime = 0;

    private IControlUI controlUI
    {
        get; set;
    }

    #endregion

    #region Unity Functions

    public override void Awake()
    {
        base.Awake();
        controlUI = GetComponent<ControlPrincipalUI>();
    }

    private void Start()
    {
        if (CoinGenerationSecond == 0) SetCoinGenerationPerSecond(1);

        ///ui
        controlUI.changeTextCoins(_coins, unitsStringValue[actualLevelOfCoinUnits]);
        controlUI.changeTextGenerationCoins(_coinGenerationPerSecond, unitsStringValue[actualLevelOfCoinUnits]);
    }

    private void Update()
    {
        //generate coins per second
        m_actualTime += Time.deltaTime;
        if (m_actualTime >= 1)
        {
            m_actualTime = 0;
            SetAugmentCoin(_coins + _coinGenerationPerSecond);

            ControlPrincipalUI.Instance.changeTextCoins(_coins, unitsStringValue[actualLevelOfCoinUnits]);
        }
    }

    public void ChangePrincipalUI(IControlUI newControlUI) {
        controlUI = newControlUI;
    }

    #endregion

    #region Methods called for other scripts by calculate coins and generateCoins

    /// <summary>
    /// This method compare a number with the actualCoin and return true if the yourCoin > actualCoin, and buy if true
    /// </summary>
    /// <returns></returns>
    public bool BuyAThing_CompateWithUnits(float newCoinToCompare, int numberCoinLevelUnits) {
        if (MathFunction.CompareTwoValuesWithUnitsIfFirstIsBiggerThanSecond(_coins, newCoinToCompare, actualLevelOfCoinUnits, numberCoinLevelUnits)) {
            Coins = MathFunction.
                    CompareTwoValuesWithUnits_ReturnTheCoinsAfterSubtractValue
                    (_coins, newCoinToCompare, actualLevelOfCoinUnits, numberCoinLevelUnits); //convert this unit
            return true;
        }
        return false;
    }

    public bool CompareWithUnits_IfCoinsIsMostBiggerThanPassParameter(float newCoinToCompare, int numberCoinLevelUnits) {
        return MathFunction.CompareTwoValuesWithUnitsIfFirstIsBiggerThanSecond(_coins, newCoinToCompare, actualLevelOfCoinUnits, numberCoinLevelUnits);
    }

    public float WithDifUnits_ReturnNumberConvertedToTheMainUnit(float newCoinToCompare, int numberCoinLevelUnits) {
        return MathFunction.ChangeUnits_ReturnNumberToConvertValueToCoinsUnits(_coins, newCoinToCompare, actualLevelOfCoinUnits, numberCoinLevelUnits);
    }

    public string GetStringValueUnitWithIndex(int index) {
        if (index < unitsStringValue.Length) {
            return unitsStringValue[index];
        }
        return "";
    }

    #endregion

    #region Control coins and generation

    /// <summary>
    /// Principal generation in the game
    /// </summary>
    public float CoinGenerationSecond
    {
        get => _coinGenerationPerSecond;
        private set
        {
            _coinGenerationPerSecond = value;
            _coinGenerationPerSecond = (float)Math.Round(_coinGenerationPerSecond, 3);
            if (controlUI == null) controlUI = GetComponent<ControlPrincipalUI>();
            if(controlUI != null) controlUI.changeTextGenerationCoins(_coinGenerationPerSecond, unitsStringValue[actualLevelOfCoinUnits]);
        }

    }

    /// <summary>
    /// Principal coins in the game.
    /// </summary>
    public float Coins
    {
        get => _coins;
        private set
        {
            SetAugmentCoin(value);
            controlUI.changeTextCoins(_coins, unitsStringValue[actualLevelOfCoinUnits]);
        }

    }

    /// <summary>
    /// Principal level coin in the game.
    /// </summary>
    public float ActualLevelUnits
    {
        get => actualLevelOfCoinUnits;
    }

    public void AugmentCoinRewardCoin(float newValue) => SetAugmentCoin(_coins+newValue);

    /// <summary>
    /// Click coins generation per second
    /// </summary>
    public void ClickCoinPerSecond()
    {
        Coins = _coins + _coinGenerationPerSecond;
    }

    /// <summary>
    /// Set coins with last session in minutes / 60 (hours)
    /// </summary>
    /// <param name="TimeLastQuitGame">Time in minutes</param>
    public void CoinsSinceLastSessionInMinutes(float TimeLastQuitGame)
    {
        print(_coins + " " + TimeLastQuitGame);
        Coins = _coins + TimeLastQuitGame;
        print(Coins + " units: " + actualLevelOfCoinUnits + ", time last quit game: " + TimeLastQuitGame +", new coin is: " + _coins + (TimeLastQuitGame));
    }

    /// <summary>
    /// Sum new coin generation and actual generation, only call when calculated index level 
    /// </summary>
    /// <param name="newCoinGeneration"></param>
    public void SetCoinGenerationPerSecond(float newCoinGeneration)
    {
        CoinGenerationSecond = newCoinGeneration + _coinGenerationPerSecond;
    }

    /// <summary>
    /// Not call directly. Only for ControlSlotInformation. Change all generation per second.
    /// </summary>
    /// <param name="newCoinGeneration"></param>
    public void ChangeCoinGenerationPerSecond(float newCoinGeneration)
    {
        CoinGenerationSecond = newCoinGeneration;
    }

    /// <summary>
    /// Multiply new coin generation and actual generation
    /// </summary>
    /// <param name="multiplicatorValue"></param>
    public void MultiplyCoinGenerationPerSecond(float multiplicatorValue)
    {
        CoinGenerationSecond = multiplicatorValue * _coinGenerationPerSecond;
    }

    private void SetAugmentCoin(float newCoin) {
        //limit to augmentCoins
        _coins = (float)Math.Round(newCoin, 3);

        //upgrade level
        if (_coins >= 1000 && actualLevelOfCoinUnits < unitsStringValue.Length)
        {
            //coins
            actualLevelOfCoinUnits++;

            _coins = (float) Math.Round(_coins / 1000, 3);

            //coin
            PassLevelCoin.Invoke(actualLevelOfCoinUnits);

            //call update text generation
            _coinGenerationPerSecond = notBrokeGameWithChangeUnitInGenerationPerSecond(3);
            controlUI.changeTextGenerationCoins(_coinGenerationPerSecond, unitsStringValue[actualLevelOfCoinUnits]);//generation
        }
    }

    private float notBrokeGameWithChangeUnitInGenerationPerSecond(int multiplicator) {
        print(_coinGenerationPerSecond/1000 +" "+  multiplicator);
        float newGenerationPerSecond = (float)Math.Round(_coinGenerationPerSecond / 1000, multiplicator);
        if (newGenerationPerSecond == 0) {
            return notBrokeGameWithChangeUnitInGenerationPerSecond(multiplicator + 3);
        }
        return newGenerationPerSecond;
    }

    //save system
    public object CaptureState()
    {
        float[] save = new float[4];
        save[0] = Coins; //because the number is < 1000, then i can convert it
        save[1] = ActualLevelUnits;
        save[2] = CoinGenerationSecond;

        print((int)save[1] + " " + save[2] + " " + save[0]);
        //save[3] = m_actualLevel;

        return save;
    }

    public void RestoreState(object state)
    {
        float[] save = (float[])state;

        print((int)save[1] + " " + save[2] + " " + save[0]);
        actualLevelOfCoinUnits = (int)save[1];
        AugmentCoinRewardCoin(save[0]);
        CoinGenerationSecond = save[2];

    }

    #endregion
}

public enum TypeSlot
{
    SlotMainCoin,
    SlotAdvertising
}

