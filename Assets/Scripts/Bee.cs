﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;

public class Bee : MonoBehaviour {
	public Animator anim;

    public enum BodyState { Normal, Fat, Obese };
    public int pollenCollected = 0;
    public int deathPollenCollected = 0;

    private BeeDigitDisplay beeDigitDisplay;

    private int pollenToFat = 10;
    private int pollenToObese = 16;
    private int deathPollenToDie = 10;

    private BodyState currentBodyState;

    public BodyState getCurrentBodyState()
    {
        return currentBodyState;
    }

    public void Die()
    {
        anim.SetBool("die", true);
        //SceneManager.LoadScene("LoseScreen");
    }

    // Use this for initialization
    void Start () {
		anim = GetComponent<Animator>();
        beeDigitDisplay = GetComponent<BeeDigitDisplay>();
    }

    // Update is called once per frame
    void Update()
    {               
        beeDigitDisplay.Show(pollenCollected, deathPollenCollected);

        if (deathPollenCollected >= deathPollenToDie)
        {
            Die();
        }

        if (pollenCollected >= pollenToObese)
        {
            if (currentBodyState != BodyState.Obese)
            {
                UpdateBodyState(BodyState.Obese);
            }
        }
        else if (pollenCollected >= pollenToFat)
        {
            if (currentBodyState != BodyState.Fat)
            {
                UpdateBodyState(BodyState.Fat);
            }
        }
        else
        {
            if (currentBodyState != BodyState.Normal)
            {
                UpdateBodyState(BodyState.Normal);
            }
        }
    }

    private void UpdateBodyState(BodyState newBodyState) {
        BoxCollider2D beeBox = gameObject.GetComponent<BoxCollider2D>();

        switch (newBodyState)
        {
            case BodyState.Normal:
                currentBodyState = BodyState.Normal;                               
                break;
            case BodyState.Fat:
                beeBox.size = beeBox.size * 2.0f;
                // gameObject.GetComponent<BoxCollider2D>().offset = new Vector2((spriteSize.x / 2), 0);
                currentBodyState = BodyState.Fat;
				break;
            case BodyState.Obese:
                currentBodyState = BodyState.Obese;
				break;
            default:
                break;
        }
		//the unity gui has a check for the pollencollected to determine sprite
		anim.SetFloat("pollenCollected", pollenCollected);
	}
}
