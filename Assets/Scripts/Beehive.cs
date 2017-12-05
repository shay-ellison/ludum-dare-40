using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Beehive : MonoBehaviour {
    // public GameObject winTextObject;
    // public GameObject pollenNeededDisplay;
    public GameObject dialogueManager;

    public int pollenNeeded = 10;

    private int hiveAttempts = 0;

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
            hiveAttempts++;
            Bee bee = otherGameObject.GetComponent<Bee>();
            if (bee.pollenCollected >= pollenNeeded)
            {
                dialogueManager.GetComponent<DialogueManager>().ShowText("Nice job! Onto the next level...");
                GameManager.instance.GoToNextScene();
            } else
            {
                DialogueManager dm = dialogueManager.GetComponent<DialogueManager>();
                if (hiveAttempts < 3)
                {
                    dm.ShowText(":) Hey!...." + pollenNeeded.ToString() + " please");
                } else if (hiveAttempts < 5)
                {
                    dm.ShowText(":/ What are you trying to pull? " + pollenNeeded.ToString());
                } else if (hiveAttempts < 8)
                {
                    dm.ShowText("-_- Listen, " + pollenNeeded.ToString() + "..." + pollenNeeded.ToString() + "!!!");
                } else if (hiveAttempts < 10)
                {
                    dm.ShowText(":$ Might not let you in...");
                } else
                {
                    bee.Die();
                }
            }
        }
    }
}
