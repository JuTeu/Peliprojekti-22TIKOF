using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private PlayerHealth playerhealth;
    public int damage = 2;     // taman avulla muutkin voi tehda pelaajalle damagea
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("AAAA");
        if(collision.gameObject.tag == "Player")
        {
            playerhealth = collision.gameObject.GetComponent<PlayerHealth>();
            //playerhealth.TakeDamage(damage);
            playerhealth.TakeDamageFromEnemy(gameObject, damage, 10f, 3f);
        }
    }
}

