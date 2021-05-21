using System.Collections.Generic;
using UnityEngine;

namespace Est.AI
{
    public class SetDestinationCharacter : MonoBehaviour
    {
        //target
        [SerializeField] TypeObjectPooling typeObjectPool;
        [SerializeField] Vector3 _targetDestination; //only for test

        bool startService = false;
        int controlDestinationObjectIndex = 0;
        ControlDirectionSpawn directionSpawn;
        SetDestinationData destinationData = new SetDestinationData();

        private void Start()
        {
            directionSpawn = ControlPoolingTypeInGame.controlPoolingType.GetTypeControlDirectionSpawn(typeObjectPool);
        }

        public void SetTheControlDestinationObjectIndex(int destinationObjectIndex) => controlDestinationObjectIndex = destinationObjectIndex;

        public Vector3 TargetDestination { get => _targetDestination; set => _targetDestination = value; }

        public SetDestinationData SetDestinationData
        {
            get => destinationData; set
            {
                destinationData = value;
                _targetDestination = value.directionAICharacter;
            }
        }

        public SetDestinationData GetDestinationCharacterAI()
        {
            //update new data
            SetDestinationData setDestinationData = new SetDestinationData();
            setDestinationData.directionAICharacter = destinationData.directionAICharacter;
            setDestinationData.positionSlotAnimationWorkCorrectly = destinationData.positionSlotAnimationWorkCorrectly;
            setDestinationData.rotationSlotAnimationWorkCorrectly = destinationData.rotationSlotAnimationWorkCorrectly;
            setDestinationData.animationTextInService = destinationData.animationTextInService;

            //first call
            if (!startService)
            {
                startService = true;
                //setDestinationData.GetDirectionCharacter() = _targetDestination;
                return setDestinationData;
            }
            //second call
            startService = false;

            // spawner, change value after service
            directionSpawn.changeValueServiceInControlDestinationObject(controlDestinationObjectIndex);
            setDestinationData.directionAICharacter = directionSpawn.GetNewSpawnDirectionAfterService();
            return setDestinationData;
        }
    }

}
