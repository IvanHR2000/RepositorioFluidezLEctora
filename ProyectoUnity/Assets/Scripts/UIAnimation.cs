using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimation : UIAnim
{
    // Start is called before the first frame update

	private Vector2 initPos;
	private Vector2 lastPosition, destinationPos;
	public Vector2 translatePos;
	public float velTranslate;
	private bool onMove;
	private float maxDistance = 0.02f;
	private RectTransform rectTransform;
	private float newVel;

	void Awake()
	{
		rectTransform = GetComponent<RectTransform> ();
		initPos = rectTransform.anchoredPosition;
		rectTransform.anchoredPosition += translatePos;
		lastPosition = rectTransform.anchoredPosition;
		destinationPos = initPos;
		this.gameObject.SetActive (false);
		newVel = velTranslate * 100;
	}
    void Start()
    {
        
    }



	public  void AnimGo()
	{
		destinationPos = initPos;
		onMove = true;
		finishAnim = false;
	}

	public  override void AnimIn()
	{
		isShowing = true;
		this.gameObject.SetActive (true);
		AnimGo ();
	}

	public override void AnimOut()
	{
		isShowing = false;
		AnimBack ();
	}


	public void AnimBack
	()
	{
		destinationPos = lastPosition;
		onMove = true;
		finishAnim = false;
	}



    // Update is called once per frame
    void Update()
    {
		if (onMove) {
			rectTransform.anchoredPosition = Vector3.MoveTowards (rectTransform.anchoredPosition, destinationPos, newVel * Time.deltaTime);
			float distanceSquard = (rectTransform.anchoredPosition - destinationPos).sqrMagnitude;
			if (distanceSquard < maxDistance) {
				rectTransform.anchoredPosition = destinationPos;
				onMove = false;
				finishAnim = true;
				this.gameObject.SetActive (isShowing);
			}
		}


			

    }
}
