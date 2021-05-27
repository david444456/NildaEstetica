using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Est.CycleGoal
{
    public class CheckSlotCompleteGoal : CompleteCycleGoals
    {
        [SerializeField] ControlSlotInformation slotInformation = null;

        public override bool VerifyCompleteGoal<SlotDataGoal>(SlotDataGoal dateGoal)
        {
            if (dateGoal.GetLevelTypeSlotToCheck < 0) return false;
            return dateGoal.GetLevelTypeSlotToCheck >= slotInformation.GetLevelOfSlotByIndex(dateGoal.TypeSlotMain);
        }
    }
}
