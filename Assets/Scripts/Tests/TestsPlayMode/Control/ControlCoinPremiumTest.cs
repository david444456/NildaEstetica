using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ControlCoinPremiumTest : MonoBehaviour
    {
        ControlCoinPremium controlCoinsPremium;
        ControlPrincipalUI controlPrincipalUI;
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


            controlCoinsPremium = FindObjectOfType<ControlCoinPremium>();
            controlPrincipalUI = FindObjectOfType<ControlPrincipalUI>();
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.Destroy(coreGO);
            Destroy(camera);
        }

        [UnityTest]
        public IEnumerator ControlCoinPremium_ChangeValueOFthePremiumCoin()
        {
            int augmentValue = 5;

            controlCoinsPremium.SetAugmentPremiumCoin(augmentValue);

            yield return new WaitForEndOfFrame();

            Assert.AreEqual(augmentValue, controlCoinsPremium.CoinsPremium, "The value is: " + controlCoinsPremium.CoinsPremium );
        }
    }
}
