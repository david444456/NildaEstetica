using System.Collections;
using System.Collections.Generic;
using Est.AI;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class SetDestinationCharacterTest : MonoBehaviour
    {
        GameObject coreGO;
        GameObject objPool;
        GameObject objPool2;
        GameObject camera;

        [SetUp]
        public void SetUp()
        {
            var core = Resources.Load("Prefab/Core/Core") as GameObject;
            coreGO = GameObject.Instantiate(core, new Vector3(0, 0, 0), Quaternion.identity);

            var Gamobj = Resources.Load("Prefab/Test/TestSpawnClient") as GameObject;
             objPool = GameObject.Instantiate(Gamobj, new Vector3(0, 0, 0), Quaternion.identity);

            var Gamobj2 = Resources.Load("Prefab/Core/SpawnCars") as GameObject;
            objPool2 = GameObject.Instantiate(Gamobj2, new Vector3(0, 0, 0), Quaternion.identity);

            //camera by not crash
            var loadCamera = Resources.Load("Prefab/Core/Main Camera") as GameObject;
            camera = GameObject.Instantiate(loadCamera, new Vector3(1, 0, 1), Quaternion.identity);
        }

        [TearDown]
        public void TearDown()
        {
            Destroy(coreGO);
            Destroy(objPool);
            Destroy(objPool2);
            Destroy(camera);
        }

        [UnityTest]
        public IEnumerator GetDestinationCharacterAI_CorrectPositionTargetDirection()
        {
            var pool = objPool.GetComponent<ObjectPooling>();

            yield return new WaitForEndOfFrame();

            //act
            pool.newGameObject(objPool.transform);

            //prepare
            var womanCharacter = GameObject.FindObjectOfType<AICharacter>();
            womanCharacter.CanSetDestinationInCharacterAI = false;

            yield return new WaitForEndOfFrame();

            var vector = womanCharacter.GetComponent<SetDestinationCharacter>().GetDestinationCharacterAI();

            Assert.AreEqual(new Vector3(0, 0, 0), vector.directionAICharacter,
                         "the vector is: " + vector);
        }

        [UnityTest]
        public IEnumerator GetDestinationCharacterAI_CorrectPositionTfSpawnPosition()
        {
            //prepare

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

            womanCharacter.GetComponent<SetDestinationCharacter>().GetDestinationCharacterAI();
            var vector = womanCharacter.GetComponent<SetDestinationCharacter>().GetDestinationCharacterAI();

            Assert.AreEqual(new Vector3(1, 1, 1), vector.directionAICharacter,
                         "the vector is: " + vector);
        }
    }
}
