using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchBehaviour : MonoBehaviour
{
    SpriteRenderer hatchSprite;
    Collider2D hatchCollision;
    // Start is called before the first frame update
    void Start()
    {
        hatchSprite = GetComponent<SpriteRenderer>();
        hatchCollision = GetComponent<Collider2D>();
        hatchSprite.enabled = false;
    }

    public void Refresh()
    {
        hatchSprite.enabled = false;
        hatchCollision.isTrigger = false;
    }
    public void Open()
    {
        hatchSprite.enabled = true;
        hatchCollision.isTrigger = true;
    }
}
