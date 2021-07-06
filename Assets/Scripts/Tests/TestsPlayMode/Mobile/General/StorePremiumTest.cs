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
    public class StorePremiumTest : MonoBehaviour
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
        public IEnumerator StorePremiumTest_TryToBuy_NotEnoughMoney()
        {
            var augmentCoin = Substitute.For<IAugmentCoin>();

            yield return new WaitForEndOfFrame();

            StorePremiumCoin store = FindObjectOfType<StorePremiumCoin>();

            yield return new WaitForEndOfFrame();

            store.ChangeControlCoin(augmentCoin);
            store.TryToBuyPremiumCoin(5);

            yield return new WaitForEndOfFrame();

            augmentCoin.Received(0).AugmentCoinRewardCoin(Arg.Any<int>());
        }

        [UnityTest]
        public IEnumerator StorePremiumTest_TryToBuy_EnoughMoney()
        {
            var augmentCoin = Substitute.For<IAugmentCoin>();

            yield return new WaitForEndOfFrame();

            FindObjectOfType<ControlCoinPremium>().SetAugmentPremiumCoin(10);

            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            StorePremiumCoin store = FindObjectOfType<StorePremiumCoin>();
            store.ChangeControlCoin(augmentCoin);
            store.TryToBuyPremiumCoin(5);

            augmentCoin.Received(1).AugmentCoinRewardCoin(Arg.Any<float>());
        }
    }
}
