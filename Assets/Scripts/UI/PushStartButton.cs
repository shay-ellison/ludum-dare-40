﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PushStartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //change screen if start button is pressed

        string currentSceneName = SceneManager.GetActiveScene().name;

		if (Input.GetMouseButtonDown(0))
		{
            GameManager.instance.GoToNextScene();            
		}
	}
}
