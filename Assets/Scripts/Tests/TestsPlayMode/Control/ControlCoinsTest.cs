using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Control
{
    public class ControlCoinsTest : MonoBehaviour
    {
        ControlCoins controlCoins;
        ControlPrincipalUI controlPrincipalUI;
        GameObject camera;

        GameObject coreGO;

        [SetUp]
        public void SetUp() {
            //camera by not crash
            var loadCamera = Resources.Load("Prefab/Core/Main Camera") as GameObject;
            camera = GameObject.Instantiate(loadCamera, new Vector3(1, 0, 1), Quaternion.identity);


            //prepare
            var core = Resources.Load("Prefab/Core/Core") as GameObject;
            coreGO = GameObject.Instantiate(core, new Vector3(0, 0, 0), Quaternion.identity);


            controlCoins = FindObjectOfType<ControlCoins>();
            controlPrincipalUI = FindObjectOfType<ControlPrincipalUI>();
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.Destroy(coreGO);
            Destroy(camera);
        }

        [UnityTest]
        public IEnumerator ChangeCoinsSessionInMinutes_TheSameCoins()
        {
            //prepare
            controlCoins.SetCoinGenerationPerSecond(1);

            yield return new WaitForEndOfFrame(); //start

            //prepare
            controlCoins.CoinsSinceLastSessionInMinutes(1000); //1

            yield return new WaitForEndOfFrame();

            //asert/act
            Assert.AreEqual(controlCoins.Coins, 1
                , "Coins actual is: " + controlCoins.Coins + ", and compare: " + 1);
        }

        [UnityTest]
        public IEnumerator ChangeCoinsClick_TheSameCoins()
        {
            //prepare
            //controlCoins.SetCoinGenerationPerSecond(1);

            yield return new WaitForEndOfFrame(); //start

            //prepare
            controlCoins.ClickCoinPerSecond(); //1

            yield return new WaitForEndOfFrame();

            //asert/act
            Assert.AreEqual(controlCoins.Coins, 1
                , "Coins actual is: " + controlCoins.Coins + ", and compare: " + 1);
        }

        [UnityTest]
        public IEnumerator AugmentCoinRewardCoin_AugmentCoin()
        {
            //prepare
            controlCoins.SetCoinGenerationPerSecond(1);

            yield return new WaitForEndOfFrame(); //start

            //prepare
            controlCoins.AugmentCoinRewardCoin(5); //1

            yield return new WaitForEndOfFrame();

            //asert/act
            Assert.AreEqual(controlCoins.Coins, 5
                , "Coins actual is: " + controlCoins.Coins + ", and compare: " + 5);
        }

        [UnityTest]
        public IEnumerator ChangeGenerationCoinsSet_TheSame()
        {
            //prepare
            //controlCoins.SetCoinGenerationPerSecond(1);

            yield return new WaitForEndOfFrame(); //start

            yield return new WaitForEndOfFrame();

            //asert/act
            Assert.AreEqual(controlCoins.CoinGenerationSecond, 1
                , "Coins actual is: " + controlCoins.CoinGenerationSecond + ", and compare: " + 1);
        }

        [UnityTest]
        public IEnumerator ChangeGenerationCoinsMultiply_TheSameValue()
        {
            //prepare
            controlCoins.SetCoinGenerationPerSecond(2);

            yield return new WaitForEndOfFrame(); //start

            controlCoins.MultiplyCoinGenerationPerSecond(2.2f);

            yield return new WaitForEndOfFrame();

            //asert/act
            Assert.AreEqual(controlCoins.CoinGenerationSecond, 6.6f
                , "Coins actual is: " + controlCoins.CoinGenerationSecond + ", and compare: " + 4.4);
        }

        [UnityTest]
        public IEnumerator CompareWithUnits_IfCoinsIsMostBiggerThanPassParameter()
        {
            //prepare
            int coinToCompare = 200;
            int indexCoins =0 ;

            yield return new WaitForEndOfFrame(); //start

            //prepare
            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes( 30000); //301

            yield return new WaitForEndOfFrame();

            //asert/act
            Assert.IsTrue(controlCoins.CompareWithUnits_IfCoinsIsMostBiggerThanPassParameter(coinToCompare, indexCoins)
                , "Coins actual is: " + controlCoins.Coins + ", and compare: " + coinToCompare);
        }

        [UnityTest]
        public IEnumerator CompareWithUnits_IfCoinsIsSmallerThanPassParameter()
        {
            //prepare
            int coinToCompare = 200;
            int indexCoins = 1;

            yield return new WaitForEndOfFrame(); //start

            //prepare
            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(5); //301

            yield return new WaitForEndOfFrame();

            //asert/act
            Assert.IsFalse(controlCoins.CompareWithUnits_IfCoinsIsMostBiggerThanPassParameter(coinToCompare, indexCoins)
                , "Coins actual is: " + controlCoins.Coins + ", and compare: " + coinToCompare);
        }

        [UnityTest]
        public IEnumerator WithChangeUnits_ReturnNumberConvertedToTheMainUnit_OneUnitOfDifferent()
        {
            //prepare
            int coinToCompare = 200;
            int indexCoins = 0;

            yield return new WaitForEndOfFrame(); //start

            //prepare
            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes( 120000); //1201

            yield return new WaitForEndOfFrame();

            //asert/act
            Assert.AreEqual(controlCoins.WithDifUnits_ReturnNumberConvertedToTheMainUnit(coinToCompare, indexCoins), 0.2f
                , "Actual coins with change unit is: " + controlCoins.WithDifUnits_ReturnNumberConvertedToTheMainUnit(coinToCompare, indexCoins) +
                ", and compare: " + 0.2f);
        }

        [UnityTest]
        public IEnumerator WithChangeUnits_ReturnNumberConvertedToTheMainUnit_TwoUnitOfDifferent()
        {
            //prepare
            int coinToCompare = 200;
            int indexCoins = 0;

            yield return new WaitForEndOfFrame(); //start

            //prepare
            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(60000000); //10k
            controlCoins.CoinsSinceLastSessionInMinutes(60000000); //10M

            yield return new WaitForEndOfFrame();

            //asert/act
            Assert.AreEqual(0.0002f, controlCoins.WithDifUnits_ReturnNumberConvertedToTheMainUnit(coinToCompare, indexCoins)
                , "Actual coins with change unit is: " + controlCoins.WithDifUnits_ReturnNumberConvertedToTheMainUnit(coinToCompare, indexCoins) +
                ", and compare: " + 0.0002f);
        }

        [UnityTest]
        public IEnumerator GetStringValueUnitWithIndex_ReturnTheDefaultValueInScene()
        {
            //prepare
            int indexCoins = 0;

            yield return new WaitForEndOfFrame(); //start

            //act
            string stringCoinsIndex = controlCoins.GetStringValueUnitWithIndex(indexCoins);

            yield return new WaitForEndOfFrame();

            //asert/act
            Assert.AreEqual("", stringCoinsIndex
                , "Actual string is: " + stringCoinsIndex + ", and expected: " + "");
        }

        [UnityTest]
        public IEnumerator BuyAThing_CompateWithUnitsTest_ReturnTrue()
        {
            //prepare
            int coinToCompare = 200;
            int indexCoins = 1;

            yield return new WaitForEndOfFrame(); //start

            //prepare
            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(18000000); //300k

            yield return new WaitForEndOfFrame();

            //asert/act
            Assert.IsTrue(controlCoins.BuyAThing_CompateWithUnits(coinToCompare, indexCoins)
                , "Coins actual is: " + controlCoins.Coins + ", and compare: " + coinToCompare);
        }

        [UnityTest]
        public IEnumerator BuyAThing_CompateWithUnitsTest_ReturnFalse()
        {
            //prepare
            int coinToCompare = 200;
            int indexCoins = 1;

            yield return new WaitForEndOfFrame(); //start

            //prepare
            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(180000); //3k

            yield return new WaitForEndOfFrame();

            //asert/act
            Assert.IsFalse(controlCoins.BuyAThing_CompateWithUnits(coinToCompare, indexCoins)
                , "Coins actual is: " + controlCoins.Coins + ", and compare: " + coinToCompare);
        }

        [UnityTest]
        public IEnumerator BuyAThing_CompateWithUnitsTest_SubtractValue()
        {
            //prepare
            int coinToCompare = 200;
            int indexCoins = 0;

            yield return new WaitForEndOfFrame(); //start

            //prepare
            controlCoins.SetCoinGenerationPerSecond(1);
            controlCoins.CoinsSinceLastSessionInMinutes(3000); //3k
            controlCoins.BuyAThing_CompateWithUnits(coinToCompare, indexCoins); //coins = 2800

            yield return new WaitForEndOfFrame();

            //asert/act
            Assert.AreEqual(2.8f, controlCoins.Coins
                , "Coins actual is: " + controlCoins.Coins + ", and compare: " + 2.8f);
        }

        [TestCase("K", 1)]
        [TestCase("M", 2)]
        [TestCase("Bi", 3)]
        [TestCase("Tri", 4)]
        public void ZGetStringValueUnitWithIndex_ReturnValue(string expectedString, int indexCoins)
        {
            //act
            string stringCoinsIndex = controlCoins.GetStringValueUnitWithIndex(indexCoins);

            //asert/act
            Assert.AreEqual(expectedString, stringCoinsIndex
                , "Actual string is: " + stringCoinsIndex + ", and expected: " + "");
        }

    }
}
