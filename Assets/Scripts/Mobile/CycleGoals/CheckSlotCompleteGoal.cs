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
            print("The level is: "+dateGoal.GetLevelTypeSlotToCheck + " " +slotInformation.GetLevelOfSlotByIndex(dateGoal.TypeSlotMain));
            return dateGoal.GetLevelTypeSlotToCheck <= slotInformation.GetLevelOfSlotByIndex(dateGoal.TypeSlotMain);
        }
    }
}
