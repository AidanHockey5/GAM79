using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class MonsterSounds : MonoBehaviour 
{
    public AudioClip roar = null;
    public AudioClip swipe = null;
    public AudioClip[] footsteps = null;

    public AudioMixerGroup monsterVox = null;
    public AudioMixerGroup monsterAction = null;

    public void Roar()
    {
        AudioManager.audManInst.PlayRandomSfx(monsterVox, roar, transform.position);
    }

    public void Swipe()
    {
        AudioManager.audManInst.PlayRandomSfx(monsterAction, swipe, transform.position);
    }

    public void Footstep()
    {
       // AudioManager.audManInst.PlayRandomSfx(monsterAction, footsteps[Random.Range(0, footsteps.Length)], transform.position);
    }

}
