using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeeDigitDisplay : MonoBehaviour {
    public List<Sprite> digits;  // 0 - 9

    public GameObject pollenIcon;
    public GameObject pollenDigit100;
    public GameObject pollenDigit10;
    public GameObject pollenDigit1;

    public GameObject deathPollenIcon;
    public GameObject deathPollenDigit100;
    public GameObject deathPollenDigit10;
    public GameObject deathPollenDigit1;

    void Update()
    {
        // Need to bind the objects to the left upper corner of the screen    
        Vector3 topLeft = new Vector3(0f, Screen.height, 0f);
        Vector3 worldTopLeft = Camera.main.ScreenToWorldPoint(topLeft);

        float startX = worldTopLeft.x + 1;
        float startY = worldTopLeft.y - 1f;

        pollenIcon.transform.position = new Vector3(startX, startY, pollenIcon.transform.position.z);

        float digitWidth = pollenDigit100.GetComponent<RectTransform>().sizeDelta.x;

        pollenDigit100.transform.position = new Vector3(startX + digitWidth, startY, pollenIcon.transform.position.z);
        pollenDigit10.transform.position = new Vector3(startX + digitWidth * 2, startY, pollenIcon.transform.position.z);
        pollenDigit1.transform.position = new Vector3(startX + digitWidth * 3, startY, pollenIcon.transform.position.z);
    }

    public void Show(int pollenNumber)
    {
        List<Sprite> numberSprites = NumberToDigitSprites(pollenNumber);        
        pollenDigit100.GetComponent<Image>().sprite = numberSprites[0];
        pollenDigit10.GetComponent<Image>().sprite = numberSprites[1];
        pollenDigit1.GetComponent<Image>().sprite = numberSprites[2];
    }

    public void Show(int pollenNumber, int deathPollenNumber)
    {
        List<Sprite> pollenNumberSprites = NumberToDigitSprites(pollenNumber);
        pollenDigit100.GetComponent<Image>().sprite = pollenNumberSprites[0];
        pollenDigit10.GetComponent<Image>().sprite = pollenNumberSprites[1];
        pollenDigit1.GetComponent<Image>().sprite = pollenNumberSprites[2];

        List<Sprite> deathPollennumberSprites = NumberToDigitSprites(deathPollenNumber);
        pollenDigit100.GetComponent<Image>().sprite = deathPollennumberSprites[0];
        pollenDigit10.GetComponent<Image>().sprite = deathPollennumberSprites[1];
        pollenDigit1.GetComponent<Image>().sprite = deathPollennumberSprites[2];
    }

    private List<Sprite> NumberToDigitSprites(int number)
    {
        int ones = number % 10;
        number = number / 10;
        int tens = number % 10;
        number = number / 10;
        int hundreds = number % 10;

        List<Sprite> digitSprites = new List<Sprite>
        {
            digits[hundreds],
            digits[tens],
            digits[ones]
        };        

        return digitSprites;
    }
}
