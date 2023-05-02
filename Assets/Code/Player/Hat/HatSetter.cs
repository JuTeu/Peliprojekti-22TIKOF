using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatSetter : MonoBehaviour
{
    Sprite down, middleDown, middle, middleUp, up, idle;
    Sprite[] hatSprites;
    SpriteRenderer hat;
    // Start is called before the first frame update
    void Start()
    {
        hat = GetComponent<SpriteRenderer>();
        SetHatType();
    }

    public void SetHatType()
    {
        string hatType = "";
        if (GameManager.equippedHat == 0)
        {
            hatType = "no_hat";
        }
        hatSprites = Resources.LoadAll<Sprite>(hatType);
    }
    public void SetHat(int direction)
    {
        switch (direction)
        {
            case 0 :
                hat.sprite = hatSprites[0];
                break;
            case 1 :
                hat.sprite = hatSprites[3];
                break;
            case 2 :
                hat.sprite = hatSprites[2];
                break;
            case 3 :
                hat.sprite = hatSprites[4];
                break;
            case 4 :
                hat.sprite = hatSprites[5];
                break;
            case 5 :
                hat.sprite = hatSprites[1];
                break;
        }
    }
}
