
using Est.Control;

namespace Est.CycleGoal
{
    public class CheckCoinCompleteGoal : CompleteCycleGoals
    {
    
        public override bool VerifyCompleteGoal<CoinDataGoal>(CoinDataGoal dataGoal)
        {
            print(dataGoal.GetValueToGoalCoin + " " + ControlCoins.Instance.ActualLevelUnits);
            return dataGoal.GetValueToGoalCoin <= ControlCoins.Instance.ActualLevelUnits;
        }
    }
}
