using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KelpFruitAnimationDamage : MonoBehaviour
{
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Vector2 fruitPosition = new Vector2(transform.position.x, transform.position.y - 0.5f);
        if (Vector2.Distance(fruitPosition, player.transform.position) < 2f)
        player.GetComponent<PlayerHealth>().TakeDamageFromEnemy(gameObject, 30f, 9f, 0.5f);
    }
}
