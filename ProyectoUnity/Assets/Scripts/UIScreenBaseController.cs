using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIScreenBaseController : MonoBehaviour
{
    // Start is called before the first frame update

	protected UIAnim[] objAnimArray;
	protected GameObject loadingComponent;
	protected EventHandler callBack;
	protected Image progressBar;
	protected bool onLoading;
	protected Canvas canvas;
	protected static UIScreenBaseController instance;
	protected bool onAnimation = false;
	private int showNextLayer = -1;
	protected SoundBaseUIComponents soundComponents;
	protected  Text totalGold;



	void Awake()
	{

		objAnimArray = GameObject.FindObjectsOfType<UIAnim> ();



		onLoading = false;


		canvas = GetComponent<Canvas> ();
		soundComponents = GetComponent<SoundBaseUIComponents> ();
		LocalAwake ();
	}


	void Start()
	{
		LocalStart ();
		ShowUIComponents (0);
	}

	protected  virtual void  LocalAwake()
	{
		
	}

	protected  virtual void  LocalStart()
	{

	}


	public static UIScreenBaseController Instance {
		get{ 
			if (instance == null)
				instance = FindObjectOfType<UIScreenBaseController> ();
			return instance;
	
		}
	}



	public Canvas ScreenCanvas{
		get {return canvas; }
	}



    /*
	protected void LoadNewScene(GameScene pScene)
	{
		loadingSceneProgress.complete = false;
		loadingSceneProgress.progress = 0;
		objGameManager.LoadAsyncScene (pScene,loadingSceneProgress);
		HideUIComponents ();
		StartCoroutine (ShowLoadingComponent (0.3f));
	}


	protected void LoadPrelevel( int world, int level)
	{
		objGameManager.SetLevel (world, level);
		LoadNewScene (GameScene.lOAD_LEVEL);
	}
    

	protected void LoadLevel( int world, int level)
	{
		loadingSceneProgress.complete = false;
		loadingSceneProgress.progress = 0;
		objGameManager.LoadAsyncLevel (world, level,loadingSceneProgress);
		HideUIComponents ();
		StartCoroutine (ShowLoadingComponent (0.3f));
	}

    */
	protected IEnumerator  ShowLoadingComponent(float timeToShow)
	{  
		yield return new WaitForSeconds (timeToShow);
		if (loadingComponent) {
			onLoading = true;
			loadingComponent.SetActive (true);
		}
	}

	protected void HideUIComponents()
	{
		onAnimation = true;
		for (int i = 0; i < objAnimArray.Length; i++) {
			if (objAnimArray[i].isShowing)
			objAnimArray [i].AnimOut ();
		}
        if (soundComponents)
		soundComponents.PlayTransicion ();
	}


	protected void HideUIComponents(int layer)
	{
		onAnimation = true;
		for (int i = 0; i < objAnimArray.Length; i++) {
			if (objAnimArray[i].layer == layer &&  objAnimArray[i].isShowing)
				objAnimArray [i].AnimOut ();
		}
        if (soundComponents)
		soundComponents.PlayTransicion ();
	}


	protected void HideUIComponents(int layer,EventHandler eventCallback)
	{
		HideUIComponents (layer);
		callBack = eventCallback;

	}

	protected void HideAndShowUIComponet(int layer)
	{
		showNextLayer = layer;
		HideUIComponents ();
	}

	protected void HideAndShowUIComponet(int layer, EventHandler eventCallback)
	{
		HideAndShowUIComponet(layer);
		callBack = eventCallback;
	}


	protected void HideAndShowUIComponet(int actualLayer, int nextLayer)
	{
		showNextLayer = nextLayer;
		HideUIComponents(actualLayer);
	
	}

	protected void HideAndShowUIComponet(int actualLayer, int nextLayer, EventHandler eventCallback)
	{
		HideAndShowUIComponet (actualLayer, nextLayer);
    	callBack = eventCallback;
	}


	protected void ShowUIComponents(int layer)
	{
		onAnimation = true;
		for (int i = 0; i < objAnimArray.Length; i++) {
			if (objAnimArray[i].layer == layer)
				objAnimArray [i].AnimIn ();
		}
        if (soundComponents)
            soundComponents.PlayTransicion ();
	}



	protected virtual void OnBackButtonDown()
	{
	}

	protected virtual void OnLocalUpdate()
	{
		
	}
    // Update is called once per frame


	private bool IsCompleteAnimation()
	{
		bool temp = true;
		for (int i = 0;  i< objAnimArray.Length; i++)
		{
			if (!objAnimArray [i].completeAnim) {
				temp = false;
				break;
			}
		}
		return temp;
	}

	private void OnAnimation()
	{
		if (onAnimation) {
			if (IsCompleteAnimation()) {
				if (showNextLayer >= 0) {
					ShowUIComponents (showNextLayer);
					showNextLayer = -1;
				} else {
					onAnimation = false;
					if (callBack != null) {
						callBack (this, null);
						callBack = null;
					}
				}
			}
		}
	}

	private void OnLoading()
	{
		if (onLoading) {
			//progressBar.fillAmount = loadingSceneProgress.progress;
			//loadingSceneProgress.complete = IsCompleteAnimation();
		}
	}

	private void OnReadInput()
	{
		if (onAnimation)
			return;
		if (Input.GetKeyDown (KeyCode.Escape)) {
			OnBackButtonDown ();
		}
	}

    void Update()
    {
		OnLoading ();
		OnAnimation ();
		OnReadInput ();
		OnLocalUpdate ();
    }





}
