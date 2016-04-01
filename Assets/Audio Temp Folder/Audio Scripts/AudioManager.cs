using UnityEngine;
using UnityEngine.Audio; 																	//This to enables access to AudioMixerGroup and AudioSnapshot data types.
using System.Collections;

public class AudioManager : MonoBehaviour 
{

	public static AudioManager audManInst = null;											//Public static variable of type AudioManager. Allows other scripts to call functions from this one.
	public GameObject soundObjectPrefab = null;												//The prefab used to create sound objects.
	public AudioMixerGroup musicMixer = null;

	private GameObject soundObject = null;													//A game object to generate sound.
	private AudioSource sound = null;														//Used to reference the Audio Source attatched to the soundObject.

	void Awake()
	{
		//Singleton
		if (audManInst == null)																//Checks if there is already an instance of AudioManager (there should not be).
			audManInst = this;																//If not, assigns this MonoBehavior to the variable (this is what we want).
		else if (audManInst != this)														//Checks if there is an instance that is not this.
			Destroy (gameObject);															//Removes the unwanted instance.
		DontDestroyOnLoad(gameObject);														//Instructs Unity not to destroy the game object when a scene is loaded.
	}
	
	//The following fuctions are used to play sounds:

	public void PlaySfx (AudioMixerGroup mixer, AudioClip soundEffect, Vector3 pos)
	{
		soundObject = (GameObject)Instantiate (soundObjectPrefab, pos, transform.rotation);	//Creates an instance of the soundObjectPrefab at the location passed into the function, and assigns it to the "soundObject" variable.
		sound = soundObject.GetComponent<AudioSource>();									//Assigns the AudioSource "sound" with the Audio Source component on the soundObject.
		sound.outputAudioMixerGroup = mixer;												//Outputs the sound through the audio mixer group passed into the function.
		sound.clip = soundEffect;															//Loads the audio clip passed into the function.
		sound.Play();																		//Plays the sound
		Destroy(soundObject, soundEffect.length);											//Destroys the soundObject after the sound has finished playing.
	}

    public void PlayRandomSfx(AudioMixerGroup mixer, AudioClip soundEffect, Vector3 pos)
    {
        soundObject = (GameObject)Instantiate(soundObjectPrefab, pos, transform.rotation);	//Creates an instance of the soundObjectPrefab at the location passed into the function, and assigns it to the "soundObject" variable.
        sound = soundObject.GetComponent<AudioSource>();									//Assigns the AudioSource "sound" with the Audio Source component on the soundObject.
        sound.outputAudioMixerGroup = mixer;												//Outputs the sound through the audio mixer group passed into the function.
        sound.clip = soundEffect;															//Loads the audio clip passed into the function.
        sound.pitch = Random.Range(0.95f, 1.05f);											//Used to change the pitch of "sfx".
        sound.Play();																		//Plays the sound
        Destroy(soundObject, soundEffect.length);											//Destroys the soundObject after the sound has finished playing.
    }

	public void PlayMusic (AudioClip musicTrack)
	{
		soundObject = (GameObject)Instantiate (soundObjectPrefab);							//Creates an instance of the soundObjectPrefab and assigns it to the "soundObject" variable.
		sound = soundObject.GetComponent<AudioSource>();									//Assigns the AudioSource "sound" with the Audio Source component on the soundObject.
		sound.outputAudioMixerGroup = musicMixer;											//Outputs the sound through the specified mixer group for music.
		sound.clip = musicTrack;															//Loads the music track passed into the function.
		sound.loop = true;																	//Sets the music track to loop.
		sound.spatialBlend = 0.0f;															//Prevents music from being affected by the player's location in 3D space.
		sound.Play();																		//Plays the music

		if (sound.loop != true)
			Destroy(soundObject, musicTrack.length);										//If not looping, destroys the soundObject after the music track has finished playing.
	}
}	
