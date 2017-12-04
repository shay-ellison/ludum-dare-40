using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
    public AudioClip deathExplosion;
    public GameObject textBox;

    private AudioSource audioSource;

    public void PlayDeathExplosion()
    {
        audioSource.PlayOneShot(deathExplosion, 1f);
    }

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.volume = 0.1f;
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
