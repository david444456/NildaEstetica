using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Est.Interact;

namespace Est.AI
{
    public class ControlTargetDestinationObject : MonoBehaviour
    {
        public SetDestinationData destinationData;
        [SerializeField] Transform transformCorrectAnimation;
        [SerializeField] UnityEvent ServiceIsFalse = new UnityEvent();
        [SerializeField] UnityEvent ServiceIsTrue = new UnityEvent();

        //control bool
        private bool InServiceGameObject = false;
        private bool locked = false;

        private SlotLocked slotLocked;

        private void Start()
        {
            destinationData.directionAICharacter = transform.position;

            if (transformCorrectAnimation != null)
            {
                destinationData.positionSlotAnimationWorkCorrectly = transformCorrectAnimation.position;
                destinationData.rotationSlotAnimationWorkCorrectly = transformCorrectAnimation.localRotation;
            }

            slotLocked = GetComponent<SlotLocked>();
            if(slotLocked != null)
                slotLocked.OnUnlocked += UnlockedSlot;
        }

        public bool ServiceBool 
        {
            get { return InServiceGameObject; }
            set
            {
                //events
                if (!value) ServiceIsFalse.Invoke(); 
                else ServiceIsTrue.Invoke();

                //ai
                InServiceGameObject = value;
            }
        }

        public bool Locked {
            get { return locked; }
            set {
                locked = value;
            }
        }

        void UnlockedSlot() {
            Locked = false;
        }
    }
}