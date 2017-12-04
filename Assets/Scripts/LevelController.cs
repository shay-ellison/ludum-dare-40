using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        audio.PlayDelayed(44100);
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
