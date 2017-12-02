using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;

public class Bee : MonoBehaviour {
    public Sprite normal;
    public Sprite fat;
    public Sprite obese;

    public enum BodyState { Normal, Fat, Obese };
    public GameObject pollenGoalDisplay;
    public GameObject pollenCollectedDisplay;

    public int pollenGoal = 10;  // set at level start
    public int pollenCollected = 0;

    public int pollenToFat = 5;
    public int pollenToObese = 8;

    private SpriteRenderer beeRenderer;
    private BodyState currentBodyState;

    public BodyState getCurrentBodyState()
    {
        return currentBodyState;
    }

	// Use this for initialization
	void Start () {
        beeRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        UnityEngine.UI.Text goalText = pollenGoalDisplay.GetComponent<UnityEngine.UI.Text>();
        goalText.text = "Need: " + pollenGoal.ToString();

        UnityEngine.UI.Text collectedText = pollenCollectedDisplay.GetComponent<UnityEngine.UI.Text>();
        collectedText.text = "Collected: " + pollenCollected.ToString();

        if (pollenCollected >= pollenToObese)
        {
            if (currentBodyState != BodyState.Obese)
            {
                UpdateBodyState(BodyState.Obese);
            }   
        } else if (pollenCollected >= pollenToFat)
        {
            if (currentBodyState != BodyState.Fat)
            {
                UpdateBodyState(BodyState.Fat);
            }
        } else
        {
            if (currentBodyState != BodyState.Normal)
            {
                UpdateBodyState(BodyState.Normal);
            }
        }
    }

    private void UpdateBodyState(BodyState newBodyState) {
        switch(newBodyState)
        {
            case BodyState.Normal:
                currentBodyState = BodyState.Normal;
                beeRenderer.sprite = normal;
                break;
            case BodyState.Fat:
                currentBodyState = BodyState.Fat;
                beeRenderer.sprite = fat;
                break;
            case BodyState.Obese:
                currentBodyState = BodyState.Obese;
                beeRenderer.sprite = obese;
                break;
            default:
                break;
        }
    }
}
