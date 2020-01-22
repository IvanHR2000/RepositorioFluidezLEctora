using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartController : BaseSceneController
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Exit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Exit();
    }
}
