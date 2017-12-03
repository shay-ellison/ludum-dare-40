using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

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
        }
	}
}
