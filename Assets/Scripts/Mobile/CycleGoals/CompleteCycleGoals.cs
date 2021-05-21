using System;
using System.Collections.Generic;
using UnityEngine;

namespace Est.CycleGoal
{
    public abstract class CompleteCycleGoals : MonoBehaviour
    {
        public TypeGoal typeGoal;

        public abstract bool VerifyCompleteGoal<T>(T dateGoal) where T : DataGoal;
    }
}