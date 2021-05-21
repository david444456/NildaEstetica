using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Est.AI
{
    //
    // Resumen:
    //     reuse Object and generate other
    public class ObjectPooling : MonoBehaviour
    {
        [Header("Other")]
        public TypeObjectPooling typeObjectPooling;

        [Header("Control Object")]
        [SerializeField] GameObject[] gameObjectsStartInTheGame = null;
        [SerializeField] GameObject[] gameObjectsPrefabSpawnGameObject = null;

        [Header("Destroy gameobjects")]
        [SerializeField] bool workWithDestroyGameObject = false;
        [SerializeField] int maxCountToDestroyGameObject = 10;

        List<GameObject> gameObjectsPool = new List<GameObject>();
        List<GameObject> gameObjectsInService = new List<GameObject>();

        int countNewDestroyGameObject = 0;

        void Start()
        {
            //objectPooling = this;

            for (int i = 0; i< gameObjectsStartInTheGame.Length; i++) {
                gameObjectsPool.Add(gameObjectsStartInTheGame[i]);
            }
        }

        public void TheGoalOfTheGameObjectStarted(GameObject GOStartMission) {
            ChangeValueGameObjectInTheLists(true, GOStartMission);
        }

        public void TheGoalOfTheGameObjectEnd(GameObject GOStartMission) {
            ChangeValueGameObjectInTheLists(false, GOStartMission);
        }

        public GameObject newGameObject(Transform gameObjectSpawnPosition) {
            if (workWithDestroyGameObject) destroyGameObjectEveryTick();

            GameObject newGoActual = null;
            if (gameObjectsPool.Count > 0)
            {
                newGoActual = gameObjectsPool[0];
                TheGoalOfTheGameObjectStarted(newGoActual);
            }
            else
            {
                newGoActual = InstantiateNewRandomObject(gameObjectSpawnPosition);
                gameObjectsInService.Add(newGoActual);
            }

            return newGoActual;
        }

        public int GetCountOfGOInService() {
            return gameObjectsInService.Count;
        }

        public int GetCountOfGOPool() => gameObjectsPool.Count;

        private void ChangeValueGameObjectInTheLists(bool InServiceGO, GameObject GOStartMission) {
            if (InServiceGO)
            {
                //list
                gameObjectsPool?.Remove(GOStartMission);
                gameObjectsInService.Add(GOStartMission);
            }
            else {
                gameObjectsPool.Add(GOStartMission);
                gameObjectsInService?.Remove(GOStartMission);
            }

            //scene
            GOStartMission.SetActive(InServiceGO);
        }

        private void destroyGameObjectEveryTick() {
            countNewDestroyGameObject++;

            if (countNewDestroyGameObject >= maxCountToDestroyGameObject) {
                if (gameObjectsPool.Count > 0) {

                    Destroy(gameObjectsPool[0]);
                    gameObjectsPool.RemoveAt(0);

                }
                countNewDestroyGameObject = 0;
            }
        }

        private GameObject InstantiateNewRandomObject(Transform gameObjectSpawnPosition)
             => Instantiate(gameObjectsPrefabSpawnGameObject[Random.Range(0, gameObjectsPrefabSpawnGameObject.Length)],
                            gameObjectSpawnPosition.position, Quaternion.identity, gameObject.transform);
    }

    public enum TypeObjectPooling {
        People,
        Car
    }
}
