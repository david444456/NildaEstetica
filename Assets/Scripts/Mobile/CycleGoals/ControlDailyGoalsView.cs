using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Est.CycleGoal
{
    public class ControlDailyGoalsView : MonoBehaviour
    {
        [SerializeField] Image[] imageDataActualGoals;
        [SerializeField] Text[] textDataActualGoals;
        [SerializeField] GameObject[] buttonsActualGoals;
        [SerializeField] GameObject[] backGroundAllGoals;

        ControlCycleGoals cycleGoals;
        int m_lastIndex = 0;

        // Start is called before the first frame update
        void Start()
        {
            cycleGoals = GetComponent<ControlCycleGoals>();
        }

        public void UpdateDataGoals(DataGoal dataGoal, int index)
        {
            //data in slots goals
            print(dataGoal.GetTextInfo() + " " + dataGoal.GetSpriteInfo() + " " + index);
            textDataActualGoals[index].text = dataGoal.GetTextInfo();
            imageDataActualGoals[index].sprite = dataGoal.GetSpriteInfo();
        }

        public void CompleteGoalChangeUI(int index) {
            //complete in ui, complete button
            m_lastIndex = index;
            buttonsActualGoals[index].SetActive(true);
        }

        public void DesactiveButtonCompleteGoal(int lastIndex) => buttonsActualGoals[lastIndex].SetActive(false);

        public void DesactiveAllGoalSlot(int index) => backGroundAllGoals[index].SetActive(false);

        public void NewGoalInActualGoals(DataGoal dataGoal, int lastIndex) {
            print("Desactive button" + lastIndex);

            //change data in new data goal
            UpdateDataGoals(dataGoal, lastIndex);
        }
    }
}
