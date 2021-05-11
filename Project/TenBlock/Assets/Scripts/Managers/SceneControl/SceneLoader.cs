// System
using System.Collections;

// UnityEnging
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Text progressText;
    public Image progressBarFill;

    private static string nextSceneName = "";
    private static readonly string LoadingSceneName = "";

    private float loadingPercent = 0.0f;

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void LoadSceneAsync(string sceneName)
    {
        nextSceneName = sceneName;
        SceneManager.LoadScene(LoadingSceneName);
    }

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1.0f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(nextSceneName);
        operation.allowSceneActivation = false;

        float timer = 0.0f;
        while (!operation.isDone)
        {
            timer += Time.deltaTime;

            if (operation.progress < 0.9f)
            {
                loadingPercent = Mathf.Lerp(loadingPercent, operation.progress, timer);
                progressBarFill.fillAmount = loadingPercent;
                progressText.text = $"{(int)(loadingPercent * 100)}%";
                if (loadingPercent >= operation.progress)
                {
                    timer = 0.0f;
                }
            }
            else
            {
                loadingPercent = Mathf.Lerp(loadingPercent, 1.0f, timer);
                progressBarFill.fillAmount = loadingPercent;
                progressText.text = $"{(int)(loadingPercent * 100)}%";
                if (loadingPercent == 1.0f)
                {
                    yield return new WaitForSeconds(1.0f);
                    operation.allowSceneActivation = true;

                    yield break;
                }
            }

            yield return new WaitForSeconds(0.05f);
        }
    }
}