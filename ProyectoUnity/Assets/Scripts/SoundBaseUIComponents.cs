using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBaseUIComponents : MonoBehaviour
{
    // Start is called before the first frame update

	public AudioClip clickSound;
	public AudioClip transicionSound;
	public AudioClip[]  mixSounds ;
	//protected MusicManager soundManager;

    void Start()
    {
		//soundManager = MusicManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void PlayClick()
	{
		PlaySound (clickSound);
	}

	private  void PlaySound(AudioClip clip)
	{
	//	if (soundManager != null && clip != null) {
		//	soundManager.PlayUISound (clip);
	//	}
	}

	public void PlayTransicion()
	{
		PlaySound (transicionSound);
	}

	public void PlaySoundByIndex(int index)
	{
		PlaySound (mixSounds [index]);
	}
}
