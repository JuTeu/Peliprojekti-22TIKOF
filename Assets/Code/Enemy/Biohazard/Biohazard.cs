using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biohazard : MonoBehaviour
{
    private PlayerHealth playerhealth;
    public int damage = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerhealth = collision.gameObject.GetComponent<PlayerHealth>();
            playerhealth.TakeDamage(damage);
        }
    }
}
