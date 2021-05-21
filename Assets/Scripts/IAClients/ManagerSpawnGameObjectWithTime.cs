using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Est.AI
{
    [RequireComponent(typeof(ObjectPooling))]
    public class ManagerSpawnGameObjectWithTime : MonoBehaviour
    {
        [Header("Control")]
        [SerializeField] Vector3 position;
        [SerializeField] Transform[] transformsSpawnGameObject = null;

        [Header("Control GO")]
        [SerializeField] int limitGameObjectSpawn = 10;
        [SerializeField] float timeBetweenSpawnGameObject = 4.5f;

        ObjectPooling objectPooling = null;
        ControlDirectionSpawn getDestinationSpawn = null;
        int actualGameObjectsInTheScene = 0;

        // Start is called before the first frame update
        void Start()
        {
            getDestinationSpawn = GetComponent<ControlDirectionSpawn>();
            objectPooling = GetComponent<ObjectPooling>();

            StartCoroutine( controlSpawnGameObjectWithTransformWithTime());
        }

        IEnumerator controlSpawnGameObjectWithTransformWithTime() {
            yield return new WaitForSeconds(timeBetweenSpawnGameObject);
            actualGameObjectsInTheScene = objectPooling.GetCountOfGOInService();

            if (actualGameObjectsInTheScene < limitGameObjectSpawn)
            {
                //set new direction for gameobject
                SetDestinationData newDirection = getDestinationSpawn.GetNewDestination();

                if (newDirection.directionAICharacter != Vector3.zero) //I have a direction
                {
                    //spawn
                    var newClientGameObject = newGOSpawnOrObjectPooling(Random.Range(0, transformsSpawnGameObject.Length));

                    //set new direction for gameobject
                    SetDestinationCharacter setDestinationCharacter = newClientGameObject.GetComponent<SetDestinationCharacter>();

                    setDestinationCharacter.SetTheControlDestinationObjectIndex(getDestinationSpawn.returnTheActualControlDestinationObjectIndex());
                    //setDestinationCharacter.TargetDestination = newDirection;
                    setDestinationCharacter.SetDestinationData = newDirection;
                }
            }
            //return
            StartCoroutine(controlSpawnGameObjectWithTransformWithTime());
        }

        GameObject newGOSpawnOrObjectPooling(int indexTF) {
            GameObject newGameObject = objectPooling.newGameObject(transformsSpawnGameObject[indexTF]);
            newGameObject.transform.position = transformsSpawnGameObject[indexTF].transform.position;
            return newGameObject;
        }
    }
}
