using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Est.AI;

namespace Tests.AI
{
    public class ControlDirectionSpawnTest
    {
        GameObject camera;

        GameObject coreGO;

        [SetUp]
        public void SetUp()
        {
            //camera by not crash
            var loadCamera = Resources.Load("Prefab/Core/Main Camera") as GameObject;
            camera = GameObject.Instantiate(loadCamera, new Vector3(1, 0, 1), Quaternion.identity);


            //prepare
            var core = Resources.Load("Prefab/Core/Core") as GameObject;
            coreGO = GameObject.Instantiate(core, new Vector3(0, 0, 0), Quaternion.identity);

        }

        [TearDown]
        public void TearDown()
        {
            foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
            {
                GameObject.Destroy(o);
            }
        }

        [UnityTest]
        public IEnumerator ControlDirectionSpawn_GetNewSpawnDirectionAfterService()
        {
            //prepare
            var obj = Resources.Load("Prefab/Core/SpawnClients") as GameObject;
            var objPool = GameObject.Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity);

            var pool = objPool.GetComponent<ObjectPooling>();

            yield return new WaitForEndOfFrame();

            //act
            pool.newGameObject(objPool.transform);

            //prepare
            var womanCharacter = GameObject.FindObjectOfType<AICharacter>();
            womanCharacter.CanSetDestinationInCharacterAI = false;

            var trans = objPool.GetComponentsInChildren<Transform>();

            for (int i = 0; i < trans.Length; i++)
            {
                if (i == 0) continue;
                trans[i].transform.position = new Vector3(1, 1, 1);
            }

            yield return new WaitForEndOfFrame();

           // womanCharacter.GetComponent<SetDestinationCharacter>().GetDestinationCharacterAI();
            var vector = GameObject.FindObjectOfType<ControlDirectionSpawn>().GetNewSpawnDirectionAfterService();

            Assert.AreEqual(new Vector3(1, 1, 1), vector,
                         "the vector is: " + vector);
        }
        
        [UnityTest]
        public IEnumerator ControlDirectionSpawn_NotGetNewDestination()
        {
            //prepare
            var obj = Resources.Load("Prefab/Core/SpawnClients") as GameObject;
            var objPool = GameObject.Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity);

            var pool = objPool.GetComponent<ObjectPooling>();

            yield return new WaitForEndOfFrame();

            //act
            pool.newGameObject(objPool.transform);

            //prepare
            var womanCharacter = GameObject.FindObjectOfType<AICharacter>();
            womanCharacter.CanSetDestinationInCharacterAI = false;

            var trans = objPool.GetComponentsInChildren<Transform>();

            for (int i = 0; i < trans.Length; i++)
            {
                if (i == 0) continue;
                trans[i].transform.position = new Vector3(1, 1, 1);
            }

            yield return new WaitForEndOfFrame();

            // womanCharacter.GetComponent<SetDestinationCharacter>().GetDestinationCharacterAI();
            var vector = GameObject.FindObjectOfType<ControlDirectionSpawn>().GetNewDestination();

            Assert.AreEqual(new Vector3(0, 0, 0), vector.directionAICharacter,
                         "the vector is: " + vector);
        }

        [UnityTest]
        public IEnumerator ControlDirectionSpawn_GetNewDestination()
        {
            //prepare
            var obj = Resources.Load("Prefab/Test/TestSpawnClient") as GameObject;
            var objPool = GameObject.Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity);

            var pool = objPool.GetComponent<ObjectPooling>();

            yield return new WaitForEndOfFrame();

            //act
            pool.newGameObject(objPool.transform);

            //prepare
            var womanCharacter = GameObject.FindObjectOfType<AICharacter>();
            womanCharacter.CanSetDestinationInCharacterAI = false;

            var trans = objPool.GetComponentsInChildren<Transform>();

            for (int i = 0; i < trans.Length; i++)
            {
                if (i == 0) continue;
                trans[i].transform.position = new Vector3(1, 1, 1);
            }

            yield return new WaitForEndOfFrame();

            // womanCharacter.GetComponent<SetDestinationCharacter>().GetDestinationCharacterAI();
            var vector = GameObject.FindObjectOfType<ControlDirectionSpawn>().GetNewDestination();

            Assert.AreEqual(new Vector3(1, 1, 1), vector.directionAICharacter,
                         "the vector is: " + vector);
        }

        [UnityTest]
        public IEnumerator ControlDirectionSpawn_GetNewDestination_GetIndexDirection()
        {
            //prepare
            var obj = Resources.Load("Prefab/Test/TestSpawnClient") as GameObject;
            var objPool = GameObject.Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity);

            var pool = objPool.GetComponent<ObjectPooling>();

            yield return new WaitForEndOfFrame();

            //act
            pool.newGameObject(objPool.transform);

            //prepare
            var womanCharacter = GameObject.FindObjectOfType<AICharacter>();
            womanCharacter.CanSetDestinationInCharacterAI = false;

            var trans = objPool.GetComponentsInChildren<Transform>();

            for (int i = 0; i < trans.Length; i++)
            {
                if (i == 0) continue;
                trans[i].transform.position = new Vector3(1, 1, 1);
            }

            yield return new WaitForEndOfFrame();

            // womanCharacter.GetComponent<SetDestinationCharacter>().GetDestinationCharacterAI();
            GameObject.FindObjectOfType<ControlDirectionSpawn>().GetNewDestination();
            var ind = GameObject.FindObjectOfType<ControlDirectionSpawn>().returnTheActualControlDestinationObjectIndex();

            Assert.AreEqual(0, ind,
                         "the index is: " + 0);
        }
    }
}
