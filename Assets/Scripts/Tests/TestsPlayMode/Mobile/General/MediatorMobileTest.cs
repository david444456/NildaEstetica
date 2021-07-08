using System.Collections;
using System.Collections.Generic;
using Est.Mobile;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Mobile
{
    public class MediatorMobileTest : MonoBehaviour
    {
        ControlCoinPremium controlCoins;
        ControlPrincipalUI controlPrincipalUI;
        GameObject MainCamera;

        GameObject coreGO;

        [SetUp]
        public void SetUp()
        {
            //camera by not crash
            var loadCamera = Resources.Load("Prefab/Core/Main Camera") as GameObject;
            MainCamera = GameObject.Instantiate(loadCamera, new Vector3(1, 0, 1), Quaternion.identity);


            //prepare
            var core = Resources.Load("Prefab/Core/Core") as GameObject;
            coreGO = GameObject.Instantiate(core, new Vector3(0, 0, 0), Quaternion.identity);


            controlCoins = FindObjectOfType<ControlCoinPremium>();
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
        public IEnumerator MediatorMobileTest_VideoRewardCar_OneCall()
        {
            var videoReward = Substitute.For<IVideoReward>();

            yield return new WaitForEndOfFrame();

            FindObjectOfType<MediatorMobile>().ChangeVideoRewardInterface(videoReward);
            FindObjectOfType<MediatorMobile>().VideoRewardCar();

            videoReward.Received(1).InstantiateVideoReward();
        }

        [UnityTest]
        public IEnumerator MediatorMobileTest_ActiveMenuRewardCarPlayerSessionView_CallsMethods()
        {
            var videoReward = Substitute.For<IControlRewardCoin>();
            var menu = Substitute.For<IMenuReward>();

            yield return new WaitForEndOfFrame();

            FindObjectOfType<MediatorMobile>().ChangeControlRewardCoinInterface(videoReward);
            FindObjectOfType<MediatorMobile>().ChangeIMenuRewardInterface(menu);
            FindObjectOfType<MediatorMobile>().ActiveMenuRewardCarPlayerSessionView();

            menu.Received(1).ActiveMenuRewardCar(Arg.Any<float>());
            videoReward.Received(1).GetNewRewardCar();
        }

        [UnityTest]
        public IEnumerator MediatorMobileTest_NewReclaimedRewardCycleGoal_OneCall()
        {
            var videoReward = Substitute.For<IControlRewardCoin>();

            yield return new WaitForEndOfFrame();

            FindObjectOfType<MediatorMobile>().ChangeControlRewardCoinInterface(videoReward);
            FindObjectOfType<MediatorMobile>().NewReclaimedRewardCycleGoal(0,0);

            videoReward.Received(1).NewRewardCycleGoal(0,0);
        }

        [UnityTest]
        public IEnumerator MediatorMobileTest_OnRewardCar_OneCall()
        {
            var videoReward = Substitute.For<IControlRewardCoin>();

            yield return new WaitForEndOfFrame();

            FindObjectOfType<MediatorMobile>().ChangeControlRewardCoinInterface(videoReward);

            FindObjectOfType<MediatorMobile>().VideoRewardCar();

            yield return new WaitForEndOfFrame();

            FindObjectOfType<AdsManager>().VideoIsComplete.Invoke();

            videoReward.Received(1).NewRewardCar();
        }

    }
}
