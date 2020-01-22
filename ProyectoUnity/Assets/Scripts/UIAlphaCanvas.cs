using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIAlphaCanvas : UIAnim
{
    // Start is called before the first frame update

	private  Image[] spriteArray;
	private float[] initAlphaArray, lastAlphaArray, destinationAlphaArray;
	private Color[] colorArray;
	public float vel;
	private bool  onAlpha;
	private float velTransition;
	private float maxDistance = 0.1f;

	void Awake()
	{
		spriteArray = transform.GetComponentsInChildren<Image> ();
		initAlphaArray = new float[spriteArray.Length];
		lastAlphaArray = new float[spriteArray.Length];
		colorArray = new Color[spriteArray.Length];
		destinationAlphaArray =  new float[spriteArray.Length];
		lastAlphaArray = new  float[spriteArray.Length];
		velTransition = vel / 100.00f;
		for (int i = 0; i < spriteArray.Length; i++) {
			initAlphaArray [i] = spriteArray [i].color.a;
			colorArray [i] = new Color ();
			colorArray [i] = spriteArray [i].color;
			colorArray [i].a = 0;
		    spriteArray [i].color= colorArray [i]  ;
			lastAlphaArray [i] = 0;
		}

		this.gameObject.SetActive (false);
		destinationAlphaArray = initAlphaArray;	
		
	}
    void Start()
    {
        
    }

	public void SetActive()
	{
		this.gameObject.SetActive (true);
		for (int i = 0; i < spriteArray.Length; i++) {
			colorArray [i].a = initAlphaArray [i] ;
			spriteArray [i].color = colorArray [i] ;
		}
		isShowing = true;
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

	public void AnimBack()
	{
		onAlpha = true;
		finishAnim = false;
		destinationAlphaArray =  lastAlphaArray ;	
		velTransition = vel / 100.00f * -1;

	}

	public void AnimGo()
	{
		onAlpha = true;
		finishAnim = false;
		destinationAlphaArray =  initAlphaArray ;	
		velTransition = vel / 100.00f ;
	}

    // Update is called once per frame
    void Update()
    {
		if (onAlpha) {
			float inc = Time.deltaTime * velTransition;
			for (int i = 0; i < spriteArray.Length; i++) {
				colorArray [i].a += inc;
				if (  colorArray [i].a > initAlphaArray[i] ||  colorArray [i].a < 0 ) {
					colorArray [i].a = destinationAlphaArray [i];
				}
				spriteArray [i].color = colorArray [i];
			}
			if (spriteArray [0].color.a == destinationAlphaArray [0]) {
				finishAnim = true;
				onAlpha = false;
				this.gameObject.SetActive (isShowing);
			}

		}
        
    }
}
