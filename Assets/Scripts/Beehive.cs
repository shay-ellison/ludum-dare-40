using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Beehive : MonoBehaviour {
    // public GameObject winTextObject;
    // public GameObject pollenNeededDisplay;

    public int pollenNeeded = 10;
    public string nextLevelName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // UnityEngine.UI.Text goalText = pollenNeededDisplay.GetComponent<UnityEngine.UI.Text>();
        // goalText.text = "Need: " + pollenNeeded.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherGameObject = other.gameObject;

        if (otherGameObject.tag == "Player")
        {
            Bee bee = otherGameObject.GetComponent<Bee>();
            if (bee.pollenCollected >= pollenNeeded)
            {
				SceneManager.LoadScene(nextLevelName);
            }
        }
    }
}
