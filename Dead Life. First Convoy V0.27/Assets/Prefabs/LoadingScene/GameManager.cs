using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject loadingScreen;
    public Image FillLoading;
    public Animator Loading_Animator;

    private int IndexScene_INT;


    private void Awake()
    {
        instance = this;

        SceneManager.LoadSceneAsync((int)SceneIndexes.TITLE_SCREEN, LoadSceneMode.Additive);
        if(PlayerPrefs.GetInt("ResolutionWidth")!=0)
        Screen.SetResolution(PlayerPrefs.GetInt("ResolutionWidth"), PlayerPrefs.GetInt("ResolutionHeight"), true);
        else
        {
            Screen.SetResolution(1280, 720, true);
        }

    }

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    //рестарт уровня
    public void RestartGameScene(int LustIndexScene)
    {
        loadingScreen.gameObject.SetActive(true);

        scenesLoading.Add(SceneManager.UnloadSceneAsync(LustIndexScene));

        scenesLoading.Add(SceneManager.LoadSceneAsync(LustIndexScene, LoadSceneMode.Additive));


        IndexScene_INT = LustIndexScene;

        StartCoroutine(GetSceneLoadProgress());
    }


    public void LoadGame(int IndexScene, int LustIndexScene)//IndexScene - номер сцену которую надо загрузить LustIndexScene - текущая сцена, нужно удалить перед загрузкой
    {
        loadingScreen.gameObject.SetActive(true);

        scenesLoading.Add(SceneManager.UnloadSceneAsync(LustIndexScene));

        scenesLoading.Add(SceneManager.LoadSceneAsync(IndexScene, LoadSceneMode.Additive));


        IndexScene_INT = IndexScene;

        StartCoroutine(GetSceneLoadProgress());       

    }
    float totalSceneProgress;
    public IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                totalSceneProgress = 0;

                foreach(AsyncOperation operation in scenesLoading)
                {
                    totalSceneProgress += operation.progress;
                }

                totalSceneProgress = totalSceneProgress / scenesLoading.Count;

                FillLoading.fillAmount = Mathf.RoundToInt(totalSceneProgress);

                yield return null;
            }
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(IndexScene_INT));

        Loading_Animator.SetBool("Loading_End", true);
    }
}
