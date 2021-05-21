using System;
using System.Collections.Generic;
using UnityEngine;
using Est.Mobile;
using Est.Mobile.Save;

namespace Est.CycleGoal
{
    public class ControlCycleGoals : MonoBehaviour, ISaveable
    {
        public static ControlCycleGoals Instance { get; private set; }

        [SerializeField] CompleteCycleGoals[] m_completeCycleGoals = null;
        [SerializeField] ListCycleGoals listCycleGoals = null;

        List<DataGoal> _dataGoalsQueue = new List<DataGoal>();
        List<DataGoal> _actualGoalsService = new List<DataGoal>();
        Dictionary<string , bool> _dataGoalIsPurchased = new Dictionary<string, bool>();

        private ControlDailyGoalsView controlDailyGoalsView;
        private int countGoalsInView = 0;
        private int maxSlotInViewGoals = 3;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            controlDailyGoalsView = GetComponent<ControlDailyGoalsView>();
        }

        private void Start()
        {
            _dataGoalsQueue = listCycleGoals.GetListTotalCycleDataGoals();

            //add datagoall purchased
            if (_dataGoalIsPurchased.Count <= 0) {
                for (int i = 0; i < _dataGoalsQueue.Count; i++)
                {
                    _dataGoalIsPurchased.Add(_dataGoalsQueue[i].name, false);
                }
            }

            //load data goals
            for (int i = 0; i < _dataGoalsQueue.Count && countGoalsInView < maxSlotInViewGoals; i++)
            {
                if (!_dataGoalIsPurchased[_dataGoalsQueue[i].name]) {
                    controlDailyGoalsView.UpdateDataGoals(_dataGoalsQueue[i], countGoalsInView);
                    _actualGoalsService.Add(_dataGoalsQueue[i]);
                    countGoalsInView++;
                }
            }

            //desactive others
            if (countGoalsInView < maxSlotInViewGoals) {
                for (int j = countGoalsInView; j < maxSlotInViewGoals; j++)
                {
                    ControlCycleGoalDesactiveAllGoalSlot(j);
                }
            }

            /*
            //service goals
            for (int j = 0; j < 3; j++)
            {
                if (firstValueIndexLastGoal + j < _dataGoalsQueue.Count)
                {
                    controlDailyGoalsView.UpdateDataGoals(_dataGoalsQueue[firstValueIndexLastGoal + j], j);
                    _actualGoalsService.Add(_dataGoalsQueue[firstValueIndexLastGoal + j]);
                    indexLastGoalCompleteInCycle++;
                }
                else
                {
                    ControlCycleGoalDesactiveAllGoalSlot(j);
                    print("You complete all goals");
                }
            }*/

            //importants events
            ControlCoins.PassLevelCoin += NewEventCoinPassLevel;
        }

        private void OnDisable()
        {
            ControlCoins.PassLevelCoin -= NewEventCoinPassLevel;

            //des. other events 
        }

        public void SubscriptionToEvent(ref Action<TypeGoal> eventToSubscribe) {
            eventToSubscribe += NewEventToVerificated;
        }

        public void ReclaimedRewardCycleGoal(int index) {
            PlayerSession.Instance.NewReclaimedRewardCycleGoal(_actualGoalsService[index].GetCoinReward(), _actualGoalsService[index].GetLevelUnitCoinReward());

            //data goal bool
            _dataGoalIsPurchased[_actualGoalsService[index].name] = true;

            //animation complete goal

            //new goal
            DataGoal newDataGoal = NewDataGoalInActualGoal(index);

            controlDailyGoalsView.DesactiveButtonCompleteGoal(index);

            //update view
            if (newDataGoal != null)
            {
                _actualGoalsService[index] = newDataGoal;
                controlDailyGoalsView.NewGoalInActualGoals(newDataGoal, index);
            }
            else
                ControlCycleGoalDesactiveAllGoalSlot(index);
        }

        private void NewEventCoinPassLevel(int index) => NewEventToVerificated(TypeGoal.Coin);

        private void ControlCycleGoalDesactiveAllGoalSlot(int index) => controlDailyGoalsView.DesactiveAllGoalSlot(index);

        private void NewEventToVerificated(TypeGoal typeGoal) {
            for (int i = 0; i < m_completeCycleGoals.Length; i++) {
                if (m_completeCycleGoals[i].typeGoal == typeGoal) {
                    for (int j = 0; j < _actualGoalsService.Count; j++) {
                        if (m_completeCycleGoals[i].VerifyCompleteGoal(_actualGoalsService[j])) {
                            controlDailyGoalsView.CompleteGoalChangeUI(j);
                            print("Completed new goal; " + _actualGoalsService[j].name);
                        }
                    }
                }
            }
        }

        private DataGoal NewDataGoalInActualGoal(int index) {

            int count = 0;
            for (int i = 0; i < _dataGoalsQueue.Count; i++)
            {
                if (!_dataGoalIsPurchased[_dataGoalsQueue[i].name]) {
                    count = 0;
                    for (int j = 0; j < maxSlotInViewGoals; j++)
                    {
                        if (_actualGoalsService[j] != _dataGoalsQueue[i])
                        {
                            print("Index is " + _dataGoalsQueue[i].name + " " + _actualGoalsService[j].name);
                            count++;
                        }
                        if (count == maxSlotInViewGoals)
                        {
                            return _dataGoalsQueue[i];
                        }
                    }
                }
            }

            /*
            if (index < _dataGoalsQueue.Count)
            {
                _actualGoalsService[index] = (_dataGoalsQueue[indexLastGoalCompleteInCycle]);

                indexLastGoalCompleteInCycle++;
                return _actualGoalsService[index];
            }*/

            return null;
        }

        public object CaptureState()
        {
            return _dataGoalIsPurchased;
        }

        public void RestoreState(object state)
        {
            _dataGoalIsPurchased = (Dictionary<string, bool>)state;
        }
    }
}
