
using Est.Control;

namespace Est.CycleGoal
{
    public class CheckCoinCompleteGoal : CompleteCycleGoals
    {
    
        public override bool VerifyCompleteGoal<CoinDataGoal>(CoinDataGoal dataGoal)
        {
            return dataGoal.GetValueToGoalCoin <= ControlCoins.Instance.ActualLevelUnits;
        }
    }
}
