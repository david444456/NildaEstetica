
using UnityEngine;
using Est.Mobile;

namespace Est.CycleGoal
{
    public class CheckStore3DCompleteGoal : CompleteCycleGoals
    {
        [SerializeField] ControlStore3D controlStore;

        public override bool VerifyCompleteGoal<StoreDataGoal>(StoreDataGoal dateGoal)
        {
            if (dateGoal.IndexItemStore3D < 0) return false;
            return controlStore.GetIfItemStoreIsPurchased(dateGoal.IndexItemStore3D);
        }
    }
}
