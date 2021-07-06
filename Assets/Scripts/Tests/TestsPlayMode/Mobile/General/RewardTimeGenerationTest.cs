using System.Collections;
using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Est.Control;
using NSubstitute;
using Est.Mobile;

namespace Tests.Mobile
{
    public class RewardTimeGenerationTest : MonoBehaviour
    {
        GameObject camera;
        GameObject coreGO;

        [SetUp]
        public void SetUp()
        {
            //camera by not crash
            var loadCamera = Resources.Load("Prefab/Core/Main Camera") as GameObject;
            camera = GameObject.Instantiate(loadCamera, new Vector3(1, 0, 1), Quaternion.identity);


            //prepare
            var core = Resources.Load("Prefab/Core/Core") as GameObject;
            coreGO = GameObject.Instantiate(core, new Vector3(0, 0, 0), Quaternion.identity);
        }

        [TearDown]
        public void TearDown()
        {
            foreach (GameObject o in FindObjectsOfType<GameObject>())
            {
                Destroy(o);
            }
        }

        [UnityTest]
        public IEnumerator RewardTimeGeneration_ClaimRewardVideoGeneration_OneCall()
        {
            var ICoin = Substitute.For<ICoinsSinceLastTimeConnect>();
            var IReward = Substitute.For<IRewardUITime>();

            RewardTimeGeneration rewardTime = FindObjectOfType<RewardTimeGeneration>();
            rewardTime.ChangeRewardUIView(IReward);
            rewardTime.ChangeControlCoin(ICoin);

            yield return new WaitForEndOfFrame();

            rewardTime.ClaimRewardVideoGeneration();

            yield return new WaitForEndOfFrame();

            IReward.Received(1).ChangeActiveStateRewardPoster(false);
            ICoin.Received(1).CoinsSinceLastSessionInMinutes( Arg.Any<float>());
        }

        [UnityTest]
        public IEnumerator RewardTimeGeneration_ClaimRewardVideoGeneration_RealValue()
        {
            var ICoin = Substitute.For<ICoinsSinceLastTimeConnect>();

            RewardTimeGeneration rewardTime = FindObjectOfType<RewardTimeGeneration>();
            rewardTime.ChangeControlCoin(ICoin);

            yield return new WaitForEndOfFrame();

            rewardTime.rewardCoinsGeneration(25);
            rewardTime.ClaimRewardVideoGeneration();
            float value = FindObjectOfType<ControlCoins>().CoinGenerationSecond * 24 * 2;

            yield return new WaitForEndOfFrame();

            ICoin.Received(1).CoinsSinceLastSessionInMinutes(value);
        }

        [UnityTest]
        public IEnumerator RewardTimeGeneration_ClaimRewardNormalGeneration_OneCall()
        {
            var ICoin = Substitute.For<ICoinsSinceLastTimeConnect>();
            var IReward = Substitute.For<IRewardUITime>();

            RewardTimeGeneration rewardTime = FindObjectOfType<RewardTimeGeneration>();
            rewardTime.ChangeRewardUIView(IReward);
            rewardTime.ChangeControlCoin(ICoin);

            yield return new WaitForEndOfFrame();

            rewardTime.ClaimRewardNormalGeneration();

            yield return new WaitForEndOfFrame();

            IReward.Received(1).ChangeActiveStateRewardPoster(false);
            ICoin.Received(1).CoinsSinceLastSessionInMinutes(Arg.Any<float>());
        }

        [UnityTest]
        public IEnumerator RewardTimeGeneration_ClaimRewardNormalGeneration_RealValue()
        {
            var ICoin = Substitute.For<ICoinsSinceLastTimeConnect>();


            RewardTimeGeneration rewardTime = FindObjectOfType<RewardTimeGeneration>();

            rewardTime.ChangeControlCoin(ICoin);

            yield return new WaitForEndOfFrame();

            rewardTime.rewardCoinsGeneration(25);
            rewardTime.ClaimRewardNormalGeneration();
            float value = FindObjectOfType<ControlCoins>().CoinGenerationSecond * 24;

            yield return new WaitForEndOfFrame();


            ICoin.Received(1).CoinsSinceLastSessionInMinutes(value);
        }

        [UnityTest]
        public IEnumerator RewardTimeGeneration_RewardCoinsGeneration_CallUI()
        {
            var IReward = Substitute.For<IRewardUITime>();

            RewardTimeGeneration rewardTime = FindObjectOfType<RewardTimeGeneration>();
            rewardTime.ChangeRewardUIView(IReward);

            yield return new WaitForEndOfFrame();

            rewardTime.rewardCoinsGeneration(25);
            //rewardTime.ClaimRewardNormalGeneration();

            yield return new WaitForEndOfFrame();

            IReward.Received(1).ChangeActiveStateRewardPoster(true);
            IReward.Received(1).ChangeTextCoinsRewardPosterTime(Arg.Any<string>());
        }
    }
}