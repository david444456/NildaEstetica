using System.Collections;
using System.Collections.Generic;
using Est.CycleGoal;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class ViewGoalsTest : MonoBehaviour
    {
        ViewControlCycleGoals view;
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

            view = FindObjectOfType<ViewControlCycleGoals>();
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.Destroy(coreGO);
            Destroy(camera);
        }

        [UnityTest]
        public IEnumerator UpdateDataGoals_ChangeText_CallIndirect()
        {
            yield return new WaitForEndOfFrame(); //start
            Dictionary<string, bool> doc = view.GetDataBoolPurchased();
            List<DataGoal> dat = view.GetActualGoalsInService();

            FindObjectOfType<ControlCoins>().SetCoinGenerationPerSecond(1);

            yield return new WaitForEndOfFrame(); //update
            view.ReclaimedRewardCycleGoal(0);

            yield return new WaitForEndOfFrame(); //update

            Button button = GameObject.Find("ButtonGoals").GetComponent<Button>();

            button.onClick.Invoke();

            yield return new WaitForEndOfFrame(); //update

            Text text = GameObject.Find("TextGoal1").GetComponent<Text>();

            yield return new WaitForEndOfFrame();

            Assert.AreSame(dat[0].GetTextInfo(), text.text, "The text in the textSlot is: " + text.text);
        }

        [UnityTest]
        public IEnumerator UpdateDataGoals_ChangeText_CallDirect()
        {
            yield return new WaitForEndOfFrame(); //start
            Dictionary<string, bool> doc = view.GetDataBoolPurchased();
            List<DataGoal> dat = view.GetListCycleGoals().GetListTotalCycleDataGoals();

            FindObjectOfType<ControlCoins>().SetCoinGenerationPerSecond(1);

            yield return new WaitForEndOfFrame(); //update
            view.GetComponent<View>().UpdateDataGoals(dat[3], 0);


            yield return new WaitForEndOfFrame(); //update

            Button button = GameObject.Find("ButtonGoals").GetComponent<Button>();

            button.onClick.Invoke();

            yield return new WaitForEndOfFrame(); //update

            Text text = GameObject.Find("TextGoal1").GetComponent<Text>();

            yield return new WaitForEndOfFrame();

            Assert.AreSame(dat[3].GetTextInfo(), text.text, "The text in the textSlot is: " + text.text);
        }

        [UnityTest]
        public IEnumerator UpdateDataGoals_NotChangeText()
        {
            yield return new WaitForEndOfFrame(); //start
            Dictionary<string, bool> doc = view.GetDataBoolPurchased();
            List<DataGoal> dat = view.GetActualGoalsInService();

            FindObjectOfType<ControlCoins>().SetCoinGenerationPerSecond(1);

            yield return new WaitForEndOfFrame(); //update

            Button button = GameObject.Find("ButtonGoals").GetComponent<Button>();

            button.onClick.Invoke();

            yield return new WaitForEndOfFrame(); //update

            Text text = GameObject.Find("TextGoal1").GetComponent<Text>();

            yield return new WaitForEndOfFrame();

            Assert.AreSame(dat[0].GetTextInfo(), text.text, "The text in the textSlot is: " + text.text);
        }

        [UnityTest]
        public IEnumerator UpdateDataGoals_ChangeSprite()
        {
            yield return new WaitForEndOfFrame(); //start
            Dictionary<string, bool> doc = view.GetDataBoolPurchased();
            List<DataGoal> dat = view.GetActualGoalsInService();

            FindObjectOfType<ControlCoins>().SetCoinGenerationPerSecond(1);

            yield return new WaitForEndOfFrame(); //update
            view.ReclaimedRewardCycleGoal(0);

            yield return new WaitForEndOfFrame(); //update

            Button button = GameObject.Find("ButtonGoals").GetComponent<Button>();

            button.onClick.Invoke();

            yield return new WaitForEndOfFrame(); //update

            Image text = GameObject.Find("ImageGoal1").GetComponent<Image>();

            yield return new WaitForEndOfFrame();

            Assert.AreSame(dat[0].GetSpriteInfo(), text.sprite, "The text in the textSlot is: " + text.sprite.name);
        }

        [UnityTest]
        public IEnumerator CompleteGoalChangeUI_ActiveButton()
        {
            yield return new WaitForEndOfFrame(); //start
            Dictionary<string, bool> doc = view.GetDataBoolPurchased();
            List<DataGoal> dat = view.GetActualGoalsInService();

            FindObjectOfType<ControlCoins>().SetCoinGenerationPerSecond(1);

            yield return new WaitForEndOfFrame(); //update
            view.GetComponent<View>().CompleteGoalChangeUI(0);

            yield return new WaitForEndOfFrame(); //update

            Button button = GameObject.Find("ButtonGoals").GetComponent<Button>();

            button.onClick.Invoke();

            yield return new WaitForEndOfFrame(); //update

            Button buttonReward = GameObject.Find("ButtonGetRewardGoal").GetComponent<Button>();

            yield return new WaitForEndOfFrame();

            Assert.IsTrue(buttonReward.gameObject.activeSelf, "The bool active object button is: " + buttonReward.gameObject.activeSelf);
        }

        [UnityTest]
        public IEnumerator DesactiveButtonCompleteGoal_DesactiveInHierarchy()
        {
            yield return new WaitForEndOfFrame(); //start
            Dictionary<string, bool> doc = view.GetDataBoolPurchased();
            List<DataGoal> dat = view.GetActualGoalsInService();

            FindObjectOfType<ControlCoins>().SetCoinGenerationPerSecond(1);

            yield return new WaitForEndOfFrame(); //update
            view.GetComponent<View>().CompleteGoalChangeUI(0);


            yield return new WaitForEndOfFrame(); //update

            Button button = GameObject.Find("ButtonGoals").GetComponent<Button>();

            button.onClick.Invoke();

            yield return new WaitForEndOfFrame(); //update

            GameObject buttonReward = GameObject.Find("ButtonGetRewardGoal");

            view.GetComponent<View>().DesactiveButtonCompleteGoal(0);

            yield return new WaitForEndOfFrame();

            Assert.IsFalse(buttonReward.gameObject.activeSelf, "The bool active object button is: " + buttonReward.gameObject.activeSelf);
        }

        [UnityTest]
        public IEnumerator DesactiveAllGoalSlot_DesactiveInHierarchy()
        {
            yield return new WaitForEndOfFrame(); //start
            Dictionary<string, bool> doc = view.GetDataBoolPurchased();
            List<DataGoal> dat = view.GetActualGoalsInService();

            FindObjectOfType<ControlCoins>().SetCoinGenerationPerSecond(1);

            yield return new WaitForEndOfFrame(); //update
            view.GetComponent<View>().CompleteGoalChangeUI(0);


            yield return new WaitForEndOfFrame(); //update

            Button button = GameObject.Find("ButtonGoals").GetComponent<Button>();

            button.onClick.Invoke();

            yield return new WaitForEndOfFrame(); //update

            GameObject buttonAllSlot = GameObject.Find("BackGroundGoal1");

            view.GetComponent<View>().DesactiveAllGoalSlot(0);

            yield return new WaitForEndOfFrame();

            Assert.IsFalse(buttonAllSlot.gameObject.activeSelf, "The bool active object button is: " + buttonAllSlot.gameObject.activeSelf);
        }

    }
}
