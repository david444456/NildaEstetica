using Est.Mobile;
using Est.Mobile.Save;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Est.CycleGoal
{
    public class ViewControlCycleGoals : MonoBehaviour, ISaveable
    {
        public static ViewControlCycleGoals Instance;

        [SerializeField] CompleteCycleGoals[] m_completeCycleGoals = null;
        [SerializeField] ListCycleGoals listCycleGoals = null;

        List<DataGoal> _dataGoalsQueue = new List<DataGoal>();
        List<DataGoal> _actualGoalsService = new List<DataGoal>();
        Dictionary<string, bool> _dataGoalIsPurchased = new Dictionary<string, bool>();

        private ControlCycleGoals controlCycleGoals;
        private View controlDailyGoalsView;
        private int countGoalsInView = 0;
        private int maxSlotInViewGoals = 3;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            controlDailyGoalsView = GetComponent<View>();
        }

        private void Start()
        {
            _dataGoalsQueue = listCycleGoals.GetListTotalCycleDataGoals();

            controlCycleGoals = new ControlCycleGoals(maxSlotInViewGoals, _dataGoalsQueue);

            //add datagoall purchased
            if (_dataGoalIsPurchased.Count <= 0)
            {
                _dataGoalIsPurchased = controlCycleGoals.NewDataGoalIsPurchased();
            }
            else {
                controlCycleGoals._dataGoalIsPurchased = _dataGoalIsPurchased;
            }

            _actualGoalsService = controlCycleGoals.LoadDataActualGoalService();

            //view
            for (int i = 0; i < _actualGoalsService.Count; i++)
            {
                controlDailyGoalsView.UpdateDataGoals(_actualGoalsService[i], i);
            }

            //desactive others
            countGoalsInView = controlCycleGoals.GetCountGoalsInService();
            if (countGoalsInView < maxSlotInViewGoals)
            {
                for (int j = countGoalsInView; j < maxSlotInViewGoals; j++)
                {
                    ControlCycleGoalDesactiveAllGoalSlot(j);
                }
            }

            //importants events
            ControlCoins.PassLevelCoin += NewEventCoinPassLevel;
        }

        private void OnDisable()
        {
            ControlCoins.PassLevelCoin -= NewEventCoinPassLevel;
            //des. other events 
        }

        public List<DataGoal> GetActualGoalsInService() => _actualGoalsService;

        public ListCycleGoals GetListCycleGoals() => listCycleGoals;

        public Dictionary<string, bool> GetDataBoolPurchased() => _dataGoalIsPurchased;

        public void SubscriptionToEvent(ref Action<TypeGoal> eventToSubscribe)
        {
            eventToSubscribe += NewEventToVerificated;
        }

        public void ReclaimedRewardCycleGoal(int index)
        {
            MediatorMobile.Instance.NewReclaimedRewardCycleGoal(
                _actualGoalsService[index].GetCoinReward(),
                _actualGoalsService[index].GetLevelUnitCoinReward());

            //data goal bool
            controlCycleGoals.SetDataGoalIsPurchased(index);
            _dataGoalIsPurchased = controlCycleGoals._dataGoalIsPurchased;
            //animation complete goal

            //new goal
            DataGoal newDataGoal = controlCycleGoals.NewDataGoalInActualGoal(index);

            controlDailyGoalsView.DesactiveButtonCompleteGoal(index);

            //update view
            if (newDataGoal != null)
            {
                _actualGoalsService = controlCycleGoals.SetActualGoalsService(index, newDataGoal);
                controlDailyGoalsView.NewGoalInActualGoals(newDataGoal, index);
            }
            else
                ControlCycleGoalDesactiveAllGoalSlot(index);
        }

        private void NewEventCoinPassLevel(int index) => NewEventToVerificated(TypeGoal.Coin);

        private void ControlCycleGoalDesactiveAllGoalSlot(int index) => controlDailyGoalsView.DesactiveAllGoalSlot(index);

        private void NewEventToVerificated(TypeGoal typeGoal)
        {
            for (int i = 0; i < m_completeCycleGoals.Length; i++)
            {
                if (m_completeCycleGoals[i].typeGoal == typeGoal)
                {
                    for (int j = 0; j < _actualGoalsService.Count; j++)
                    {
                        if (m_completeCycleGoals[i].VerifyCompleteGoal(_actualGoalsService[j]))
                        {
                            controlDailyGoalsView.CompleteGoalChangeUI(j);
                            print("Completed new goal; " + _actualGoalsService[j].name);
                        }
                    }
                }
            }
        }

        public object CaptureState()
        {
            return controlCycleGoals.GetDataGoalIsPurchased();
        }

        public void RestoreState(object state)
        {
            _dataGoalIsPurchased = (Dictionary<string, bool>)state;
        }
    }
}
