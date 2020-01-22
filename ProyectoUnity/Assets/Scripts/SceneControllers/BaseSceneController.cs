using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    protected GameManager objGameManager;
    protected AudioSource audio1;


    private void Awake()
    {
        objGameManager = GameManager.Instance;
        audio1 = GetComponent<AudioSource>();
        LocalAwake();
    }

    protected virtual void LocalAwake()
    {

    }

    public void PlayClick()
    {
        if (audio1!=null)
        audio1.Play();
    }

    public void GoToScene(string sceneName)
    {
        PlayClick();
        objGameManager.GoToScene(sceneName);
    }
}

 
