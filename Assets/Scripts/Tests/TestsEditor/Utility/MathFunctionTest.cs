using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class MathFunctionTest
    {

        [TestCase(2654139, "Bi")]
        [TestCase(36, "M")]
        [TestCase(10, "K")]
        public void ChangeUnitNumberWithString_GetStringUnionTwoValues(float number, string keyUnit)
        {
            string valueString = MathFunction.ChangeUnitNumberWithString(number, keyUnit);
            string newStringToCompate = number.ToString() + " " + keyUnit;


            Assert.AreEqual(valueString, newStringToCompate, "I expected: " + valueString + "but was: " + newStringToCompate);
        }

        [TestCase(1, 1, 8, 3)]
        [TestCase(22, 1, 5, 2)]
        [TestCase(2, 1, 0, 0)]
        [TestCase(1, 1, 1, 0)]
        [TestCase(1, 1, 0,0)]
        public void CompareTwoValuesWithUnits_IfFirstIsBiggerThanSecondReturnTrue(float coins, float value, int indexLevelActual, int indexActualValueToCompare)
        {
            bool valueBool = MathFunction.CompareTwoValuesWithUnitsIfFirstIsBiggerThanSecond(coins, value, indexLevelActual, indexActualValueToCompare);

            Assert.IsTrue(valueBool, 
                "I expected true, the values is " + coins+ " unit: " + indexLevelActual + " and: " + value + " unit: "+ indexActualValueToCompare);
        }

        [TestCase(2, 1, 0, 1)]
        [TestCase(1, 1, 1, 2)]
        [TestCase(1, 2, 0, 0)]
        public void CompareTwoValuesWithUnits_IfFirstIsBiggerThanSecondReturn_ReturnFalse_SecondValueIsBiggerThanFirst(float coins, float value, int indexLevelActual, int indexActualValueToCompare)
        {
            bool valueBool = MathFunction.CompareTwoValuesWithUnitsIfFirstIsBiggerThanSecond(coins, value, indexLevelActual, indexActualValueToCompare);

            Assert.IsFalse(valueBool,
                "I expected true, the values is " + coins + " unit: " + indexLevelActual + " and: " + value + " unit: " + indexActualValueToCompare);
        }

        //error bool

        [TestCase(10, 1, 2, 1)]
        [TestCase(1, 1, 1, 0)]
        [TestCase(1, 1, 0, 0)]
        public void CompareTwoValuesWithUnits_ReturnCoinAfterSustractSecondValue(float coins, float value, int indexLevelActual, int indexActualValueToCompare)
        {
            float valueFloat = MathFunction.CompareTwoValuesWithUnits_ReturnTheCoinsAfterSubtractValue(coins, value, indexLevelActual, indexActualValueToCompare);
            float valueInTest = (float)(coins * Math.Pow(10, 3 * (indexLevelActual-indexActualValueToCompare))) - value;
            valueInTest = (float)Math.Round(valueInTest / Math.Pow(10, 3 * (indexLevelActual - indexActualValueToCompare)), 3);

            Assert.AreEqual(valueFloat, valueInTest, 0.001f,
                "I expected returns " + valueInTest + " unit: " + indexLevelActual + " and return: " + valueFloat + " unit: " + indexLevelActual);
        }

        [TestCase(33, 22, 2, 1, 32.978000f)]
        [TestCase(1, 1, 2, 1, 0.999000f)]
        [TestCase(1, 1, 1, 0, 0.999f)]
        [TestCase(1, 1, 0, 0, 0)]
        public void CompareTwoValuesWithUnits_ReturnCoinAfterSustractSecondValue_DontUseCalculateMath
            (float coins, float value, int indexLevelActual, int indexActualValueToCompare, float valueExpected) {
            float valueFloat = MathFunction.CompareTwoValuesWithUnits_ReturnTheCoinsAfterSubtractValue(coins, value, indexLevelActual, indexActualValueToCompare);

            Assert.AreEqual(valueFloat, valueExpected, 0.001f,
                "I expected returns " + valueExpected + " unit: " + indexLevelActual + " and return: " + valueFloat + " unit: " + indexLevelActual);
        }


        [TestCase(10, 1, 2, 1)]
        [TestCase(1, 1, 1, 0)]
        [TestCase(1, 1, 0, 0)]
        public void CompareTwoValuesWithUnits_ReturnSecondValueInTheSameUnitTheFirstValue(float coins, float value, int indexLevelActual, int indexActualValueToCompare)
        {
            float valueFloat = MathFunction.ChangeUnits_ReturnNumberToConvertValueToCoinsUnits(coins, value, indexLevelActual, indexActualValueToCompare);
            float valueInTest = (float)Math.Pow(10, (-1) * 3 * (indexLevelActual - indexActualValueToCompare)) * value;

            Assert.AreEqual(valueFloat, valueInTest, 0.001f,
                "I expected returns " + valueInTest + " unit: " + indexLevelActual + " and return: " + valueFloat + " unit: " + indexLevelActual);
        }

        [TestCase(1, 10, 2, 1, 0.01f)]
        [TestCase(10, 1, 2, 1, 0.001f)]
        [TestCase(1, 1, 1, 0, 0.001f)]
        [TestCase(1, 1, 0, 0, 1)]
        public void CompareTwoValuesWithUnits_ReturnSecondValueInTheSameUnitTheFirstValue_DontHaveMathFunction
            (float coins, float value, int indexLevelActual, int indexActualValueToCompare, float valueInTest)
        {
            float valueFloat = MathFunction.ChangeUnits_ReturnNumberToConvertValueToCoinsUnits(coins, value, indexLevelActual, indexActualValueToCompare);

            Assert.AreEqual(valueFloat, valueInTest, 0.0001f,
                "I expected returns " + valueInTest + " unit: " + indexLevelActual + " and return: " + valueFloat + " unit: " + indexLevelActual);
        }
    }
}
