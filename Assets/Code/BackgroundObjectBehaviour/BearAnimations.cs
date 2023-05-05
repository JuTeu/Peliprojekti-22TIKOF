using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAnimations : MonoBehaviour
{
    Animator anim;
    bool playerIsNear;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") playerIsNear = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerIsNear = false;
            anim.speed = 1f;
        }
    }

    public void PauseForPlayer()
    {
        if (playerIsNear) anim.speed = 0f;
    }
}
