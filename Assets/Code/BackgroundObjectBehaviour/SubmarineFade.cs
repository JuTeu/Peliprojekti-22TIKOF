using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineFade : MonoBehaviour
{
    SpriteRenderer submarineSprite;
    float opacity = 1f;
    bool inside = false;
    void Start()
    {
        submarineSprite = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") inside = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (-50f < collision.transform.position.y) inside = false;
        }
    }
    void Update()
    {
        if (inside && opacity > 0f)
        {
            opacity -= Time.deltaTime;
            submarineSprite.color = new Color(1f, 1f, 1f, opacity);
        }
        else if (!inside && opacity < 1f)
        {
            opacity += Time.deltaTime;
            submarineSprite.color = new Color(1f, 1f, 1f, opacity);
        }
    }
}
