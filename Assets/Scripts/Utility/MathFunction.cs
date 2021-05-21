using System;
using UnityEngine;

[System.Serializable]
public struct MathFunction
{
    /// <summary>
    /// Take a number and a string and returns the union of the two in a string
    /// </summary>
    /// <param name="number">first data in string</param>
    /// <param name="keyUnit">second data in string</param>
    /// <returns></returns>
    public static string ChangeUnitNumberWithString(float number, string keyUnit)
    {
        string numberUnit = number.ToString();

        numberUnit = ((float)number).ToString() + " " + keyUnit;

        return numberUnit;
    }

    /// <summary>
    ///  Compare two values and return the value of coins in the currentcy unit after subtract second value,
    /// </summary>
    /// <param name="coins">Value to bigger than second value</param>
    /// <param name="value">second value</param>
    /// <param name="indexLevelActual">unit to first number</param>
    /// <param name="indexActualValueToCompare">unit second number</param>
    /// <returns></returns>
    public static float CompareTwoValuesWithUnits_ReturnTheCoinsAfterSubtractValue(float coins, float value, int indexLevelActual, int indexActualValueToCompare)
    {
        int difLevel = indexLevelActual - indexActualValueToCompare;
        if (difLevel < 0) Debug.LogError($"This number {value} is more big that other {coins}");
        if (difLevel == 0) return (coins - value);
        float finalNumber = (coins*Mathf.Pow(10, 3*difLevel)) - value;
        return (float)Math.Round(finalNumber/ Mathf.Pow(10, 3 * difLevel), 3);
    }

    /// <summary>
    ///  Compare two values and return the second value one in the units first one, when the units of the first value is most bigger than the second one
    /// </summary>
    /// <param name="coins">Value to bigger than second value</param>
    /// <param name="value">second value</param>
    /// <param name="indexLevelActual">unit to first number</param>
    /// <param name="indexActualValueToCompare">unit second number</param>
    /// <returns></returns>
    public static float ChangeUnits_ReturnNumberToConvertValueToCoinsUnits(float coins, float value, int indexLevelActual, int indexActualValueToCompare)
    {
        int difLevel = indexLevelActual - indexActualValueToCompare;
        //if (difLevel < 0) Debug.LogError($"This number {value} is more big that other {coins}, the difLevel is {difLevel}, and the indexLevelActual is {indexLevelActual}, compare index is {indexActualValueToCompare}");
        if (difLevel == 0) return value;
        float finalNumber = (float) Mathf.Pow(10, (-1)*3 * difLevel) * value;
        return finalNumber;
    }

    /// <summary>
    /// Compare value of two value and returns true if the first one is larger than second one
    /// </summary>
    /// <param name="coins">Value to bigger than second value</param>
    /// <param name="value">second value</param>
    /// <param name="indexLevelActual">unit to first number</param>
    /// <param name="indexActualValueToCompare">unit second number</param>
    /// <returns></returns>
    public static bool CompareTwoValuesWithUnitsIfFirstIsBiggerThanSecond(float coins, float value, int indexLevelActual, int indexActualValueToCompare) {
        int difLevel = indexLevelActual - indexActualValueToCompare;
        //Debug.Log($" {difLevel} {indexLevelActual} {indexActualValueToCompare}");
        if (difLevel < 0) return false;
        if (difLevel == 0) return (coins - value) >= 0;
        return true;
    }
}
