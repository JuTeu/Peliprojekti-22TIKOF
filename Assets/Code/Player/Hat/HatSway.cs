using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatSway : MonoBehaviour
{
    SpriteRenderer hatSprite;
    void Start()
    {
        hatSprite = GetComponent<SpriteRenderer>();
    }
    public void Sway(int frame)
    {
        if (frame == 1)
        {
            transform.localPosition = new Vector2(0.0937f, -0.0313f);
        }
        else if (frame == 2)
        {
            transform.localPosition = new Vector2(0.0625f, 0f);
        }
        else if (frame == 3)
        {
            transform.localPosition = new Vector2(0f, 0f);
        }
        else if (frame == 4)
        {
            transform.localPosition = new Vector2(-0.0625f, 0f);
        }
        else if (frame == 5)
        {
            transform.localPosition = new Vector2(-0.0937f, -0.0313f);
        }
        if (hatSprite.flipX) transform.localPosition = new Vector2(transform.localPosition.x * -1, transform.localPosition.y);
    }
    public void Jump(int frame)
    {
        // 0 = 0, 4 = 1, 5 = 2, 6 = 3, 7 = 4, 8 = 5, 12 = 6, 13 = 7, 14 = 5, 15 = 8, 16 = 9, 17 = 4
        // 0 = idle, 6 = middle up, 17 = middle, 18 = middle down, 19 = down
        if (frame == 0)
        {
            transform.localPosition = new Vector2(-0.0313f, 0f);
        }
        else if (frame == 1)
        {
            transform.localPosition = new Vector2(-0.0313f, 0.0313f);
        }
        else if (frame == 2)
        {
            transform.localPosition = new Vector2(-0.0313f, 0.0937f);
        }
        else if (frame == 3)
        {
            transform.localPosition = new Vector2(0f, 0.125f);
        }
        else if (frame == 4)
        {
            transform.localPosition = new Vector2(0f, 0f);
        }
        else if (frame == 5)
        {
            transform.localPosition = new Vector2(0f, 0.0313f);
        }
        else if (frame == 6)
        {
            transform.localPosition = new Vector2(0f, -0.0625f);
        }
        else if (frame == 7)
        {
            transform.localPosition = new Vector2(0f, -0.157f);
        }
        else if (frame == 8)
        {
            transform.localPosition = new Vector2(-0.0313f, 0.3125f);
        }
        else if (frame == 9)
        {
            transform.localPosition = new Vector2(0.0313f, 0.156f);
        }
    }
}
