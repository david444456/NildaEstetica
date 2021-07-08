using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Est.Mobile
{
    public interface IControlRewardCoin 
    {
        void NewRewardCar();
        void NewRewardCycleGoal(float coinToReward, int levelCoinToReward);
        float GetNewRewardCar();
    }
}
