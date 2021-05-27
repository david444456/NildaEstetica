using System;
using System.Collections.Generic;
using UnityEngine;

namespace Est.CycleGoal
{
    public class ControlCycleGoals 
    {
        public Dictionary<string, bool> _dataGoalIsPurchased = new Dictionary<string, bool>();
        List<DataGoal> _dataGoalsQueue = new List<DataGoal>();
        List<DataGoal> _actualGoalsService = new List<DataGoal>();

        private int _maxSlotInViewGoals = 3;
        private int countGoalsInView = 0;

        public ControlCycleGoals(int maxSlotInViewGoals, List<DataGoal> dataGoalsQueue) {
            _maxSlotInViewGoals = maxSlotInViewGoals;
            _dataGoalsQueue = dataGoalsQueue;
        }

        public void SetDataGoalIsPurchased(int index) => _dataGoalIsPurchased[_actualGoalsService[index].name] = true;

        public int GetCountGoalsInService() => countGoalsInView;

        public Dictionary<string, bool> NewDataGoalIsPurchased() {
            for (int i = 0; i < _dataGoalsQueue.Count; i++)
            {
                _dataGoalIsPurchased.Add(_dataGoalsQueue[i].name, false);
            }
            return _dataGoalIsPurchased;
        }

        public Dictionary<string, bool> GetDataGoalIsPurchased() => _dataGoalIsPurchased;

        public List<DataGoal> LoadDataActualGoalService() {
            for (int i = 0; i < _dataGoalsQueue.Count && countGoalsInView < _maxSlotInViewGoals; i++)
            {
                if (!_dataGoalIsPurchased[_dataGoalsQueue[i].name])
                {
                    _actualGoalsService.Add(_dataGoalsQueue[i]);
                    countGoalsInView++;
                }
            }
            return _actualGoalsService;
        }

        public List<DataGoal> SetActualGoalsService(int index, DataGoal newDataGoal) {
            _actualGoalsService[index] = newDataGoal;
            return _actualGoalsService;
        }

        public DataGoal NewDataGoalInActualGoal(int index)
        {
            int count = 0;
            for (int i = 0; i < _dataGoalsQueue.Count; i++)
            {
                if (!_dataGoalIsPurchased[_dataGoalsQueue[i].name])
                {
                    count = 0;
                    for (int j = 0; j < _maxSlotInViewGoals; j++)
                    {
                        if (_actualGoalsService[j] != _dataGoalsQueue[i])
                        {
                            count++;
                        }
                        if (count == _maxSlotInViewGoals)
                        {
                            return _dataGoalsQueue[i];
                        }
                    }
                }
            }
            return null;
        }
    }
}
