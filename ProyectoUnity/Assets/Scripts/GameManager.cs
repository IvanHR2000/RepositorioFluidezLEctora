using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManagerParams
{
    public float progress;
    public bool complete;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private bool checkSceneLoad = false; // chekea  si a escena termino de cargar
    private AsyncOperation asyncLoadedScene;
    private Enums.GameScene sceneName;
    private GameManagerParams param;

    public static GameManager Instance
    {

        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    public void LoadSceneAsync(Enums.GameScene pScene , GameManagerParams param = null)
    {
        if (!checkSceneLoad)
        {
            this.param = param;
            StartCoroutine("LoadAsyncGameScene", pScene);
        }
    }

    public void GoToScene(string sceneName)
    {
        Initiate.Fade(sceneName, Color.black, 1.0f);
    }

    IEnumerator LoadAsyncGameScene(Enums.GameScene pScene)
    {
        sceneName = pScene;
        asyncLoadedScene = SceneManager.LoadSceneAsync(sceneName.ToString(), LoadSceneMode.Single);
        asyncLoadedScene.allowSceneActivation = false;
        checkSceneLoad = true;
        yield return asyncLoadedScene;
    }

    void Update()
    {

        if (checkSceneLoad)
        {
            bool loadNextScene = true;
            if (param != null)
            {
                param.progress = asyncLoadedScene.progress;
                loadNextScene = param.complete;
            }
            if (asyncLoadedScene.progress >= 0.9f && !asyncLoadedScene.allowSceneActivation && loadNextScene)
            {
                asyncLoadedScene.allowSceneActivation = true;

            }
            if (asyncLoadedScene.isDone)
            {
                checkSceneLoad = false;
                Scene scene1 = SceneManager.GetSceneByName(sceneName.ToString());
                bool activar = SceneManager.SetActiveScene(scene1);
            }
        }
    }
}
