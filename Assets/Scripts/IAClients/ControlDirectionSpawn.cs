using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Est.AI
{
    public class ControlDirectionSpawn : MonoBehaviour
    {
        [Header("Other")]
        public TypeObjectPooling typeObjectPooling;

        [SerializeField] ControlTargetDestinationObject[] controlPossibleDirections = null;
        [SerializeField] Transform[] spawnsTFActual = null;

        int indexDirection = 0;

        public void changeValueServiceInControlDestinationObject(int index) => changeValueServiceBoolDirection(index, false);

        public SetDestinationData GetNewDestination()
        {
            SetIndexDirectionWithControlPossibleDirections();
            return NewDirectionSubtractIndexDirection();
        }

        public Vector3 GetNewSpawnDirectionAfterService() {
            SetIndexDirectionWithSpawnsTFActual();
            return spawnsTFActual[indexDirection].transform.position;
        }

        public int returnTheActualControlDestinationObjectIndex() => indexDirection;

        private SetDestinationData NewDirectionSubtractIndexDirection()
        {
            while (indexDirection >= 0 && controlPossibleDirections.Length > 0)
            {
                if (!controlPossibleDirections[indexDirection].ServiceBool && !controlPossibleDirections[indexDirection].Locked)
                {
                    changeValueServiceBoolDirection(indexDirection, true);
                    return controlPossibleDirections[indexDirection].destinationData;
                }

                indexDirection--;
            }

            return new SetDestinationData();
        }

        private void changeValueServiceBoolDirection(int index, bool value)
        {
            if (controlPossibleDirections.Length > index) controlPossibleDirections[index].ServiceBool = value;
        }

        private void SetIndexDirectionWithControlPossibleDirections() => indexDirection = UnityEngine.Random.Range(0, controlPossibleDirections.Length);

        private void SetIndexDirectionWithSpawnsTFActual() => indexDirection = UnityEngine.Random.Range(0, spawnsTFActual.Length);

    }
}
