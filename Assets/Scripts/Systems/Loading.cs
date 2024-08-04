using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [Header("Child Link Objects")]
    public GameObject loadingScene;
    public Slider loadingBar;

    private void Update()
    {
        // Brightness
        /*if (loadingScene.activeInHierarchy)
        {
            int alpha = 200 + (PlayerPrefs.GetInt("last_brightness") * -20);
            brightnessScreen.GetComponent<Image>().color = new Color32(0, 0, 0, (byte)alpha);
        }*/
    }

    public void Show()
    {
        loadingScene.SetActive(true);
    }

    public void LoadScene(int sceneId) { StartCoroutine(LoadSceneAsync(sceneId)); }
    public IEnumerator LoadSceneAsync(int sceneId)
    {
        if (sceneId > SceneManager.sceneCountInBuildSettings - 1)
        {
            Debug.Log("Error LoadingScenes : Out of Range SceneID = " + sceneId + " / " + (SceneManager.sceneCountInBuildSettings - 1));
            sceneId = 0;
        }

        loadingBar.value = 0;
        loadingScene.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        operation.allowSceneActivation = false;
        float progress = 0;

        while (!operation.isDone)
        {
            progress = Mathf.MoveTowards(progress, operation.progress, Time.deltaTime);
            loadingBar.value = progress;
            if (progress >= 0.9f)
            {
                loadingBar.value = 1;
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
    public int GetSceneIndex() { return SceneManager.GetActiveScene().buildIndex; }
}
