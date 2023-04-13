using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float knockbackForce = 10f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "SpawnedObject")
        {
            Vector2 knockbackDirection = transform.position - collision.transform.position;
            knockbackDirection.Normalize();
            Vector2 knockbackForceVector = knockbackDirection * knockbackForce;

            rb.AddForce(knockbackForceVector, ForceMode2D.Impulse);
        }
    }
}
