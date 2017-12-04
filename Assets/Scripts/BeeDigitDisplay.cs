using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeeDigitDisplay : MonoBehaviour {
    public List<Sprite> digits;  // 0 - 9

    public GameObject pollenIcon;
    public GameObject pollenDigit10;
    public GameObject pollenDigit1;

    public GameObject deathPollenIcon;
    public GameObject deathPollenDigit10;
    public GameObject deathPollenDigit1;

    void Update()
    {
        // Need to bind the objects to the left upper corner of the screen    
        Vector3 topLeft = new Vector3(0f, Screen.height, 0f);
        Vector3 worldTopLeft = Camera.main.ScreenToWorldPoint(topLeft);

        float startX = worldTopLeft.x + 1;
        float startY = worldTopLeft.y - 1f;
        
        float digitWidth = pollenDigit10.GetComponent<RectTransform>().sizeDelta.x;
        float digitHeight = pollenDigit10.GetComponent<RectTransform>().sizeDelta.y;

        pollenIcon.transform.position = new Vector3(startX, startY, pollenIcon.transform.position.z);
        pollenDigit10.transform.position = new Vector3(startX + digitWidth, startY, pollenIcon.transform.position.z);
        pollenDigit1.transform.position = new Vector3(startX + digitWidth * 2, startY, pollenIcon.transform.position.z);

        float deathY = startY - digitHeight;

        deathPollenIcon.transform.position = new Vector3(startX, deathY, pollenIcon.transform.position.z);
        deathPollenDigit10.transform.position = new Vector3(startX + digitWidth, deathY, pollenIcon.transform.position.z);
        deathPollenDigit1.transform.position = new Vector3(startX + digitWidth * 2, deathY, pollenIcon.transform.position.z);
    }

    public void Show(int pollenNumber)
    {
        List<Sprite> numberSprites = NumberToDigitSprites(pollenNumber);        
        pollenDigit10.GetComponent<Image>().sprite = numberSprites[0];
        pollenDigit1.GetComponent<Image>().sprite = numberSprites[1];
    }

    public void Show(int pollenNumber, int deathPollenNumber)
    {
        List<Sprite> pollenNumberSprites = NumberToDigitSprites(pollenNumber);
        pollenDigit10.GetComponent<Image>().sprite = pollenNumberSprites[0];
        pollenDigit1.GetComponent<Image>().sprite = pollenNumberSprites[1];

        List<Sprite> deathPollennumberSprites = NumberToDigitSprites(deathPollenNumber);
        deathPollenDigit10.GetComponent<Image>().sprite = deathPollennumberSprites[0];
        deathPollenDigit1.GetComponent<Image>().sprite = deathPollennumberSprites[1];
    }

    /// [Tens, Ones]
    private List<Sprite> NumberToDigitSprites(int number)
    {
        int ones = number % 10;
        number = number / 10;
        int tens = number % 10;

        List<Sprite> digitSprites = new List<Sprite>
        {
            digits[tens],
            digits[ones]
        };

        return digitSprites;
    }
}
