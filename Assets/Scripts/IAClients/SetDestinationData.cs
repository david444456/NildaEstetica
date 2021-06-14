using System;
using UnityEngine;

namespace Est.AI
{
    [Serializable]
    public class SetDestinationData  {
        [SerializeField] public Vector3 directionAICharacter = Vector3.zero;
        [SerializeField] public Vector3 positionSlotAnimationWorkCorrectly = Vector3.zero;
        [SerializeField] public Quaternion rotationSlotAnimationWorkCorrectly;
        [SerializeField] public string animationTextInService = "";

        public Vector3 GetDirectionCharacter() => directionAICharacter;
    }

}
