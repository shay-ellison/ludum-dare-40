using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beehive : MonoBehaviour {
    public GameObject winTextObject;

    public int pollenNeeded = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherGameObject = other.gameObject;

        if (otherGameObject.tag == "Player")
        {
            Bee bee = otherGameObject.GetComponent<Bee>();
            if (bee.pollenCollected >= pollenNeeded)
            {
                Debug.Log("YOU WIN!");
                UnityEngine.UI.Text winText = winTextObject.GetComponent<UnityEngine.UI.Text>();
                winText.text = "YOU WIN!";
            }
        }
    }
}
