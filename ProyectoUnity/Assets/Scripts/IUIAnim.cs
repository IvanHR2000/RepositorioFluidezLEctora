using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnim : MonoBehaviour {
    // Start is called before the first frame update
 
	 public virtual void AnimIn ()
	{}
	public virtual void AnimOut()
	{}

	protected bool finishAnim = true;

	public bool completeAnim
	{
		get{ 
			return finishAnim;
		}
	}

	public int layer = 0;
	[HideInInspector]
	public  bool isShowing = false;

}
