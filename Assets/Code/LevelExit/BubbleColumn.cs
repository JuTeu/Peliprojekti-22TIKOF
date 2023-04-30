using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleColumn : MonoBehaviour
{
    bool isPlayerInBubbleColumn = false;
    Rigidbody2D playerRigidbody;
    void Start()
    {
        playerRigidbody = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isPlayerInBubbleColumn = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isPlayerInBubbleColumn = false;
        }
    }
    void FixedUpdate()
    {
        if (isPlayerInBubbleColumn) playerRigidbody.AddForce(new Vector2(0f, 10f));
    }
}
