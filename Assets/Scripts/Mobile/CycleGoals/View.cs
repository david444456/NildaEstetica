using UnityEngine;
using UnityEngine.UI;

namespace Est.CycleGoal
{
    public class View : MonoBehaviour
    {
        [SerializeField] Image[] imageDataActualGoals;
        [SerializeField] Text[] textDataActualGoals;
        [SerializeField] GameObject[] buttonsActualGoals = null;
        [SerializeField] GameObject[] backGroundAllGoals = null;

        ViewControlCycleGoals cycleGoals;
        int m_lastIndex = 0;

        // Start is called before the first frame update
        void Start()
        {
            cycleGoals = GetComponent<ViewControlCycleGoals>();
        }

        public void UpdateDataGoals(DataGoal dataGoal, int index)
        {
            //data in slots goals
            textDataActualGoals[index].text = dataGoal.GetTextInfo();
            imageDataActualGoals[index].sprite = dataGoal.GetSpriteInfo();
        }

        public void CompleteGoalChangeUI(int index)
        {
            //complete in ui, complete button
            m_lastIndex = index;
            buttonsActualGoals[index].SetActive(true);
        }

        public void DesactiveButtonCompleteGoal(int lastIndex) => buttonsActualGoals[lastIndex].SetActive(false);

        public void DesactiveAllGoalSlot(int index) => backGroundAllGoals[index].SetActive(false);

        public void NewGoalInActualGoals(DataGoal dataGoal, int lastIndex)
        {
            print("Desactive button" + lastIndex);

            //change data in new data goal
            UpdateDataGoals(dataGoal, lastIndex);
        }
    }
}
