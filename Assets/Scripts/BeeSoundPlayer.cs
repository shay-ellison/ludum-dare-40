using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeSoundPlayer : MonoBehaviour {

    public AudioSource audioSource;

    // Looped
    public AudioClip wingFlutter;
    public AudioClip walk;    

    // One-time
    public AudioClip grabPollen;
    public AudioClip grabDeathPollen;    

    public void PlayWingFlutter()
    {
        PlayLoop(wingFlutter);
    }

    public void PlayWalk()
    {
        PlayLoop(walk);
    }

    public void PlayGrabPollen()
    {
        PlayOnce(grabPollen);
    }

    public void PlayGrabDeathPollen()
    {
        PlayOnce(grabDeathPollen);
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    // Use this for initialization
    void Start () {
		if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
	}

    private void PlayLoop(AudioClip audioClip)
    {
        Stop();
        audioSource.clip = audioClip;
        audioSource.loop = true;
        audioSource.volume = 0.4f;
        audioSource.Play();
    }

    private void PlayOnce(AudioClip audioClip)
    {
        // Keep any looping track going, hopefully
        audioSource.PlayOneShot(audioClip, 1f);                
    }
}
