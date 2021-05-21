using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] GameObject gameObjectProgress = null;
    [SerializeField] Slider sliderProgress = null;

    private void Start()
    {
        loadSceneByNumber(1);
    }

    public void loadSceneByNumber(int numberScene) {
        StartCoroutine(LoadAsyncScene(numberScene));
    }

    public void loadNextScene() {
        StartCoroutine(LoadAsyncScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadAsyncScene(int sceneIndex) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            gameObjectProgress.SetActive(true);
            sliderProgress.value = progress;

            yield return null;
        }


    }
}
