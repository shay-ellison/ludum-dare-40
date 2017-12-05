using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    public GameObject dialogueBox;
    public GameObject dialogueText;

    private bool showingText = false;

    public void ShowText(string text)
    {
        showingText = true;
        dialogueBox.SetActive(true);
        dialogueText.SetActive(true);
        dialogueText.GetComponent<Text>().text = text;
    }

    public void ShowLevelCompletionText(string text)
    {
        ShowText(text);
    }

    public void EndText()
    {
        showingText = false;
        dialogueText.SetActive(false);
        dialogueBox.SetActive(false);        
    }

	// Use this for initialization
	void Start () {
        dialogueText.SetActive(false);
        dialogueBox.SetActive(false);        
	}
	
	// Update is called once per frame
	void Update () {
        // Center the DialogueBox
        float centerX = Screen.width / 2f;
        float centerY = Screen.height / 2f;
        Vector3 screenCenter = new Vector3(centerX, centerY, 0f);
        Vector3 worldCenter = Camera.main.ScreenToWorldPoint(screenCenter);

        dialogueBox.transform.position = new Vector3(worldCenter.x, worldCenter.y, dialogueBox.transform.position.z);
        // Center the text in the dialogue box
        dialogueText.transform.position = new Vector3(worldCenter.x, worldCenter.y, dialogueBox.transform.position.z);

        if (showingText && Input.GetKeyDown(KeyCode.Return))
        {
            EndText();
        }
    }
}
