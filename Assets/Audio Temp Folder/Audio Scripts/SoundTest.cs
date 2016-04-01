using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class SoundTest : MonoBehaviour 
{
	public AudioClip glassBreak;
	public AudioClip backgroundMusic;
	public AudioMixerGroup sfx;

	void Awake ()
	{
		AudioManager.audManInst.PlayMusic (backgroundMusic);
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.anyKeyDown)
		{
			AudioManager.audManInst.PlayRandomSfx (sfx, glassBreak, transform.position);
		}
	}
}
