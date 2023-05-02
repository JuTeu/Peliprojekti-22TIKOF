using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator player, hat, arms;
    SpriteRenderer playerSprite, hatSprite, armsSprite;

    void Start()
    {
        player = GetComponent<Animator>();
        arms = transform.Find("Arms").gameObject.GetComponent<Animator>();
        hat = transform.Find("Hat").gameObject.GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        armsSprite = transform.Find("Arms").gameObject.GetComponent<SpriteRenderer>();
        hatSprite = transform.Find("Hat").gameObject.GetComponent<SpriteRenderer>();
    }

    public void Play(string input)
    {
        string hatString = "";
        if (input == "DownSwim" || input == "DownSwimIdle")
        {
            hatString = "Down";
        }
        else if (input == "MiddleDownSwim" || input == "MiddleDownSwimIdle")
        {
            hatString = "MiddleDown";
        }
        else if (input == "MiddleSwim" || input == "MiddleSwimIdle")
        {
            hatString = "Middle";
        }
        else if (input == "MiddleUpSwim" || input == "MiddleUpSwimIdle")
        {
            hatString = "MiddleUp";
        }
        else if (input == "UpSwim" || input == "UpSwimIdle")
        {
            hatString = "Up";
        }
        else
        {
            hatString = input;
        }
        player.Play(input);
        arms.Play(input);
        hat.Play(hatString);
    }

    public void Flip(bool toggle)
    {
        playerSprite.flipX = toggle;
        armsSprite.flipX = toggle;
        hatSprite.flipX = toggle;
    }

    public void SetColor(Color color)
    {
        playerSprite.color = color;
        armsSprite.color = color;
        hatSprite.color = color;
    }
}
