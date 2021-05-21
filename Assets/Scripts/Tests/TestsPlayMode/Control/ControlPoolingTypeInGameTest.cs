using System.Collections;
using System.Collections.Generic;
using Est.AI;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ControlPoolingTypeInGameTest : MonoBehaviour
    {
        ControlCoins controlCoins;
        ControlPrincipalUI controlPrincipalUI;
        GameObject camera;

        GameObject coreGO;

        [SetUp]
        public void SetUp()
        {
            //camera by not crash
            var loadCamera = Resources.Load("Prefab/Core/Main Camera") as GameObject;
            camera = GameObject.Instantiate(loadCamera, new Vector3(1, 0, 1), Quaternion.identity);

            controlCoins = FindObjectOfType<ControlCoins>();
            controlPrincipalUI = FindObjectOfType<ControlPrincipalUI>();
        }

        [TearDown]
        public void TearDown()
        {
            //GameObject.Destroy(coreGO);
            var objects = FindObjectsOfType<Transform>();
            foreach (Transform b in objects)
            {
                Destroy(b.gameObject);
            }

        }

        [UnityTest]
        public IEnumerator ControlPoolingTypeInGame_GetTypePooling()
        {
            //prepare
            var core = Resources.Load("Prefab/Core/Core") as GameObject;
            coreGO = GameObject.Instantiate(core, new Vector3(0, 0, 0), Quaternion.identity);

            var Gamobj = Resources.Load("Prefab/Test/TestSpawnClient") as GameObject;
            var objPool = GameObject.Instantiate(Gamobj, new Vector3(0, 0, 0), Quaternion.identity);
            
            var Gamobj2 = Resources.Load("Prefab/Core/SpawnCars") as GameObject;
            var objPool2 = GameObject.Instantiate(Gamobj2, new Vector3(0, 0, 0), Quaternion.identity);

            var manSpawn = objPool.GetComponent<ManagerSpawnGameObjectWithTime>();
            var ctrolObj = FindObjectOfType<ControlPoolingTypeInGame>();

            yield return new WaitForEndOfFrame();

            ObjectPooling pool = ctrolObj.GetTypePooling(TypeObjectPooling.People);
            ObjectPooling pool2  = null;
            
            yield return new WaitForEndOfFrame();

            var objLoop = FindObjectsOfType<ObjectPooling>();
            foreach (ObjectPooling obj in objLoop)
            {
                if (obj.typeObjectPooling == TypeObjectPooling.People)
                {
                    pool2 = obj;
                }
            }
            yield return new WaitForEndOfFrame();

            Assert.IsTrue(pool.typeObjectPooling == pool2.typeObjectPooling, "The are no the same pooling; " + pool);
        }

        [UnityTest]
        public IEnumerator ControlPoolingTypeInGame_GetTypeControlDirectionSpawn()
        {
            //prepare
            var core = Resources.Load("Prefab/Core/Core") as GameObject;
            coreGO = GameObject.Instantiate(core, new Vector3(0, 0, 0), Quaternion.identity);

            var Gamobj = Resources.Load("Prefab/Test/TestSpawnClient") as GameObject;
            var objPool = GameObject.Instantiate(Gamobj, new Vector3(0, 0, 0), Quaternion.identity);

            var Gamobj2 = Resources.Load("Prefab/Core/SpawnCars") as GameObject;
            var objPool2 = GameObject.Instantiate(Gamobj2, new Vector3(0, 0, 0), Quaternion.identity);

            var ctrolObj = FindObjectOfType<ControlPoolingTypeInGame>();

            yield return new WaitForEndOfFrame();

            var pool = ctrolObj.GetTypeControlDirectionSpawn(TypeObjectPooling.Car);
            ControlDirectionSpawn pool2 = null;

            yield return new WaitForEndOfFrame();

            var objLoop = FindObjectsOfType<ControlDirectionSpawn>();
            foreach (ControlDirectionSpawn obj in objLoop)
            {
                if (obj.typeObjectPooling == TypeObjectPooling.Car)
                {
                    pool2 = obj;
                }
            }
            yield return new WaitForEndOfFrame();

            Assert.IsTrue(pool.typeObjectPooling == pool2.typeObjectPooling, "The are no the same pooling; " + pool);
        }
    }
}
