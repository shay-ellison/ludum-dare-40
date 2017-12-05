using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    private AudioSource audioSource;
    public AudioClip backgroundMusic;
    public AudioClip deathExplosion;

    public void PlayDeathExplosion()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(deathExplosion, 1f);
    }

	// Use this for initialization
	void Start () {
        GameManager.instance.OnScreenName(SceneManager.GetActiveScene().name);
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.volume = 0.4f;
        audioSource.Play();
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R))  // Reset the Scene
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        } else if (Input.GetKey(KeyCode.Escape)) 
        {
            // switch to windowed
            Screen.fullScreen = false;
        } else if (Input.GetKey(KeyCode.F))
        {
            Screen.fullScreen = true;
        }
	}
}
