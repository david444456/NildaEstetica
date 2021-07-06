using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Est.Control;
using NSubstitute;
using Est.Mobile;

namespace Tests.Mobile
{
    public class ControlRewardCoinTest : MonoBehaviour
    {
        ControlCoins controlCoins;
        ControlPrincipalUI controlPrincipalUI;

        GameObject coreGO;

        [SetUp]
        public void SetUp()
        {
            //camera by not crash
            var loadCamera = Resources.Load("Prefab/Core/Main Camera") as GameObject;
            GameObject.Instantiate(loadCamera, new Vector3(1, 0, 1), Quaternion.identity);


            //prepare
            var core = Resources.Load("Prefab/Core/Core") as GameObject;
            coreGO = GameObject.Instantiate(core, new Vector3(0, 0, 0), Quaternion.identity);


            controlCoins = FindObjectOfType<ControlCoins>();
            controlPrincipalUI = FindObjectOfType<ControlPrincipalUI>();
        }

        [TearDown]
        public void TearDown()
        {
            foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
            {
                Destroy(o);
            }
        }

        [UnityTest]
        public IEnumerator ControlRewardCoinTest_NewRewardCar_CallAugmentCoin()
        {
            var augmentCoin = Substitute.For<IAugmentCoin>();
            var convertUnits = Substitute.For<IReturnConvertNumber>();

            yield return new WaitForEndOfFrame();

            FindObjectOfType<ControlRewardCoin>().ChangeAugmentCoin(augmentCoin);
            FindObjectOfType<ControlRewardCoin>().ChangeConvertNumber(convertUnits);
            FindObjectOfType<ControlRewardCoin>().NewRewardCar();
            yield return new WaitForEndOfFrame();

            augmentCoin.Received(1).AugmentCoinRewardCoin(Arg.Any<float>());
            convertUnits.Received(1).WithDifUnits_ReturnNumberConvertedToTheMainUnit(Arg.Any<float>(), Arg.Any<int>());
        }

        [UnityTest]
        public IEnumerator ControlRewardCoinTest_NewRewardCycleGoal_CallAugmentCoin()
        {
            var augmentCoin = Substitute.For<IAugmentCoin>();
            var convertUnits = Substitute.For<IReturnConvertNumber>();

            yield return new WaitForEndOfFrame();

            FindObjectOfType<ControlRewardCoin>().ChangeAugmentCoin(augmentCoin);
            FindObjectOfType<ControlRewardCoin>().ChangeConvertNumber(convertUnits);
            FindObjectOfType<ControlRewardCoin>().NewRewardCycleGoal(1,1);
            yield return new WaitForEndOfFrame();

            augmentCoin.Received(1).AugmentCoinRewardCoin(Arg.Any<float>());
            convertUnits.Received(1).WithDifUnits_ReturnNumberConvertedToTheMainUnit(Arg.Any<float>(), Arg.Any<int>());
        }

        [UnityTest]
        public IEnumerator ControlRewardCoinTest_GetNewRewardCar_Zero()
        {


            yield return new WaitForEndOfFrame();

            var ctrolReward = FindObjectOfType<ControlRewardCoin>();

            yield return new WaitForEndOfFrame();

            Assert.AreEqual(ctrolReward.GetNewRewardCar(), 0, "The actual value is: " + ctrolReward.GetNewRewardCar());

        }

        [UnityTest]
        public IEnumerator ControlRewardCoinTest_GetNewRewardCar_10Coins()
        {


            yield return new WaitForEndOfFrame();

            var ctrolReward = FindObjectOfType<ControlRewardCoin>();
            ControlCoins.Instance.AugmentCoinRewardCoin(10);

            yield return new WaitForEndOfFrame();

            Assert.AreEqual(ctrolReward.GetNewRewardCar(), 20f, "The actual value is: " + ctrolReward.GetNewRewardCar());

        }
    }
}
