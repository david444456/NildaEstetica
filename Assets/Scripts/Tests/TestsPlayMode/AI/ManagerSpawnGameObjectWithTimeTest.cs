using System.Collections;
using System.Collections.Generic;
using Est.AI;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ManagerSpawnGameObjectWithTimeTest
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
        public IEnumerator AfterSecond_CreateAGameObject()
        {
            //prepare
            var obj = Resources.Load("Prefab/Test/TestSpawnClient") as GameObject;
            var objPool = GameObject.Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity);

            var manSpawn = objPool.GetComponent<ManagerSpawnGameObjectWithTime>();

            yield return new WaitForEndOfFrame();

            //act start

            yield return new WaitForSeconds(3.5f);

            //prepare
            var womanCharacter = GameObject.FindObjectOfType<AICharacter>();
            womanCharacter.CanSetDestinationInCharacterAI = false;

            yield return new WaitForEndOfFrame();

            Assert.IsTrue("Woman(Clone)" == womanCharacter.name || "Man(Clone)" == womanCharacter.name,
                         "the name is: " + womanCharacter.name);
        }

        [UnityTest]
        public IEnumerator AfterSecond_CreateAGameObject_OnlyOneGameObject()
        {
            //prepare
            var obj = Resources.Load("Prefab/Test/TestSpawnClient") as GameObject;
            var objPool = GameObject.Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity);

            var manSpawn = objPool.GetComponent<ManagerSpawnGameObjectWithTime>();

            yield return new WaitForEndOfFrame();

            //act start

            yield return new WaitForSeconds(3.5f);

            //prepare
            var womanCharacter = GameObject.FindObjectOfType<AICharacter>();
            womanCharacter.CanSetDestinationInCharacterAI = false;

            yield return new WaitForSeconds(3.5f);

            bool menBool = false;
            var objects = GameObject.FindObjectsOfType<AICharacter>();
            for (int i = 0; i < objects.Length; i++) {
                if (objects[i] != womanCharacter) {
                    AICharacter men = objects[i];
                    men.CanSetDestinationInCharacterAI = false;
                    menBool = true;
                }
            }

            Assert.IsFalse(menBool,
                         "the bool is: " + menBool);
        }

        [UnityTest]
        public IEnumerator AfterSecond_CreateAGameObject_SetCorrectDirection()
        {
            //prepare
            var obj = Resources.Load("Prefab/Test/TestSpawnClient") as GameObject;
            var objPool = GameObject.Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity);

            var manSpawn = objPool.GetComponent<ManagerSpawnGameObjectWithTime>();

            yield return new WaitForEndOfFrame();

            //act start
            var trans = objPool.GetComponentsInChildren<Transform>();
            for (int i = 0; i < trans.Length; i++)
            {
                if (i == 0) continue;
                trans[i].transform.position = new Vector3(1, 1, 1);
            }

            yield return new WaitForSeconds(3.5f);

            //prepare
            var womanCharacter = GameObject.FindObjectOfType<AICharacter>();
            womanCharacter.CanSetDestinationInCharacterAI = false;

            yield return new WaitForEndOfFrame();

            Vector3 newVector = womanCharacter.GetComponent<SetDestinationCharacter>().TargetDestination;

            Assert.AreEqual(new Vector3(1,1,1) ,newVector , 
                         "the vector is: " + newVector);
        }
    }
}
