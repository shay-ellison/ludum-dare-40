using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeSoundPlayer : MonoBehaviour {

    public AudioSource loopAudioSource;
    public AudioSource oneTimeAudioSource;

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
        loopAudioSource.Stop();
    }

    // Use this for initialization
    void Start () {
		if (loopAudioSource == null)
        {
            AudioSource[] audioSources = GetComponents<AudioSource>();
            loopAudioSource = audioSources[0];
            oneTimeAudioSource = audioSources[1];
        }
	}

    private void PlayLoop(AudioClip audioClip)
    {
        Stop();
        loopAudioSource.clip = audioClip;
        loopAudioSource.loop = true;
        loopAudioSource.volume = 0.4f;
        loopAudioSource.Play();
    }

    private void PlayOnce(AudioClip audioClip)
    {
        // Keep any looping track going, hopefully
        oneTimeAudioSource.PlayOneShot(audioClip, 1f);                
    }
}
