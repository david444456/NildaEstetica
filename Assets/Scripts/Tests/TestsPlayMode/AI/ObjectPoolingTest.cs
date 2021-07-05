using System.Collections;
using System.Collections.Generic;
using Est.AI;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.AI
{
    public class ObjectPoolingTest : MonoBehaviour
    {
        GameObject coreGO;

        [SetUp]
        public void SetUp()
        {
            var core = Resources.Load("Prefab/Core/Core") as GameObject;
            coreGO = GameObject.Instantiate(core, new Vector3(0, 0, 0), Quaternion.identity);

            //camera by not crash
            var loadCamera = Resources.Load("Prefab/Core/Main Camera") as GameObject;
            GameObject.Instantiate(loadCamera, new Vector3(1, 0, 1), Quaternion.identity);
        }

        [TearDown]
        public void TearDown()
        {
            var objects = FindObjectsOfType<Transform>();
            foreach (Transform b in objects)
            {
                Destroy(b.gameObject);
            }
        }

        [UnityTest]
        public IEnumerator ObjectPooling_TheGoalOfTheGameObjectStarted()
        {
            //prepare
            var obj = Resources.Load("Prefab/Core/SpawnClients") as GameObject;
            var objPool = GameObject.Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity);

            var manCharacter = Resources.Load("Prefab/Character/Man") as GameObject;
            var manCharacterObj = GameObject.Instantiate(manCharacter, new Vector3(0, 0, 0), Quaternion.identity);

            var pool = objPool.GetComponent<ObjectPooling>();
            manCharacterObj.GetComponent<AICharacter>().CanSetDestinationInCharacterAI = false;

            yield return new WaitForEndOfFrame();

            //act
            pool.TheGoalOfTheGameObjectStarted(manCharacterObj);//0, man

            yield return new WaitForEndOfFrame();

            Assert.AreEqual(1, pool.GetCountOfGOInService(),
                          "the count GOPool is: " + pool.GetCountOfGOPool() + ", and the count GOService is: " + pool.GetCountOfGOInService());
        }

        [UnityTest]
        public IEnumerator ObjectPooling_TheGoalOfTheGameObjectEnd()
        {
            //prepare
            var obj = Resources.Load("Prefab/Core/SpawnClients") as GameObject;
            var objPool = GameObject.Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity);

            var manCharacter = Resources.Load("Prefab/Character/Man") as GameObject;
            var manCharacterObj = GameObject.Instantiate(manCharacter, new Vector3(0, 0, 0), Quaternion.identity);

            var pool = objPool.GetComponent<ObjectPooling>();
            manCharacterObj.GetComponent<AICharacter>().CanSetDestinationInCharacterAI = false;

            yield return new WaitForEndOfFrame();

            //act
            pool.TheGoalOfTheGameObjectEnd(manCharacterObj);//0, man

            yield return new WaitForEndOfFrame();

            Assert.AreEqual(1, pool.GetCountOfGOPool(),
                          "the count GOPool is: " + pool.GetCountOfGOPool() + ", and the count GOService is: " + pool.GetCountOfGOInService());
        }

        [UnityTest]
        public IEnumerator ObjectPooling_NewGameObject_InstantiateObject()
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


            yield return new WaitForEndOfFrame();

            Assert.IsTrue("Woman(Clone)" == womanCharacter.name || "Man(Clone)" == womanCharacter.name, "the name is: " + womanCharacter.name);
        }

        [UnityTest]
        public IEnumerator ObjectPooling_NewGameObject_DestroyWithTick()
        {
            //prepare
            var obj = Resources.Load("Prefab/Core/SpawnClients") as GameObject;
            var objPool = GameObject.Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity);

            var manCharacter = Resources.Load("Prefab/Character/Man") as GameObject;
            var manCharacterObj = GameObject.Instantiate(manCharacter, new Vector3(0, 0, 0), Quaternion.identity);

            var pool = objPool.GetComponent<ObjectPooling>();
            manCharacterObj.GetComponent<AICharacter>().CanSetDestinationInCharacterAI = false;
            yield return new WaitForEndOfFrame();

            //act
            pool.TheGoalOfTheGameObjectEnd(manCharacterObj);//0, man
            yield return new WaitForEndOfFrame();
            //act
            pool.newGameObject(objPool.transform);//1
            //act
            pool.newGameObject(objPool.transform);//2

            pool.newGameObject(objPool.transform);//3

            pool.newGameObject(objPool.transform);//4

            pool.newGameObject(objPool.transform);//5

            pool.newGameObject(objPool.transform);//6

            pool.newGameObject(objPool.transform);//7

            pool.newGameObject(objPool.transform);//8

            pool.newGameObject(objPool.transform);//9

            pool.newGameObject(objPool.transform);//10

            var goWoman = GameObject.FindObjectsOfType<AICharacter>();
            foreach (AICharacter wom in goWoman) {
                wom.CanSetDestinationInCharacterAI = false;
            }

            yield return new WaitForEndOfFrame();

            Assert.IsTrue(pool.GetCountOfGOPool() == 0 || pool.GetCountOfGOInService() == 9,
                          "the count GOPool is: " + pool.GetCountOfGOPool() + ", and the count GOService is: " + pool.GetCountOfGOInService());
        }

        [UnityTest]
        public IEnumerator ObjectPooling_NewGameObject_NotDestroyWithTick()
        {
            //prepare
            var obj = Resources.Load("Prefab/Core/SpawnClients") as GameObject;
            var objPool = GameObject.Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity);

            var manCharacter = Resources.Load("Prefab/Character/Man") as GameObject;
            var manCharacterObj = GameObject.Instantiate(manCharacter, new Vector3(0, 0, 0), Quaternion.identity);

            var pool = objPool.GetComponent<ObjectPooling>();
            manCharacterObj.GetComponent<AICharacter>().CanSetDestinationInCharacterAI = false;
            yield return new WaitForEndOfFrame();

            //act
            pool.TheGoalOfTheGameObjectEnd(manCharacterObj);//0, man
            yield return new WaitForEndOfFrame();
            //act
            pool.newGameObject(objPool.transform);//1
            //act
            pool.newGameObject(objPool.transform);//2

            pool.newGameObject(objPool.transform);//3

            pool.newGameObject(objPool.transform);//4

            pool.newGameObject(objPool.transform);//5

            pool.newGameObject(objPool.transform);//6

            pool.newGameObject(objPool.transform);//7

            pool.newGameObject(objPool.transform);//8

            var goWoman = GameObject.FindObjectsOfType<AICharacter>();
            foreach (AICharacter wom in goWoman)
            {
                wom.CanSetDestinationInCharacterAI = false;
            }

            yield return new WaitForEndOfFrame();

            Assert.IsTrue(pool.GetCountOfGOPool() == 0 || pool.GetCountOfGOInService() == 8,
                          "the count GOPool is: " + pool.GetCountOfGOPool() + ", and the count GOService is: " + pool.GetCountOfGOInService());
        }
    }
}
