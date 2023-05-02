using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchBehaviour : MonoBehaviour
{
    SpriteRenderer hatchSprite;
    Collider2D hatchCollision;
    [SerializeField] GameObject arrow, flippers;
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
        flippers.SetActive((GameManager.unlocks & 0b_1_0000) == 0b_0_0000);
        arrow.SetActive(false);
    }
    public void Open()
    {
        hatchSprite.enabled = true;
        hatchCollision.isTrigger = true;
        arrow.SetActive(true);
    }
}
