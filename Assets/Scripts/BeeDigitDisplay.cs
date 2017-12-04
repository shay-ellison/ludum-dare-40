using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class BeeDigitDisplay : MonoBehaviour {
    public Sprite testSprite;
    public List<Sprite> digits;  // 0 - 9

    public GameObject pollenDigit100;
    public GameObject pollenDigit10;
    public GameObject pollenDigit1;

    public void Show(int number)
    {
        List<Sprite> digitSprites = NumberToDigitSprites(number);        
        pollenDigit100.GetComponent<Image>().sprite = digitSprites[0];
        pollenDigit10.GetComponent<Image>().sprite = digitSprites[1];
        pollenDigit1.GetComponent<Image>().sprite = digitSprites[2];
    }

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            string digitPath = "Assets/Sprites/Labels/Digits/Digit" + i.ToString() + ".png";
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(digitPath);
            digits.Add(sprite);
        }
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
