using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KelpFruitSpawner : MonoBehaviour
{
    [SerializeField] GameObject kelpFruit;
    public LayerMask levelLayerMask;
    
    void Start()
    {
        Invoke("DoTheThing", 1f);
    }
    void DoTheThing()
    {
        if (Physics2D.OverlapCapsule(transform.position, new Vector2(1.4f, 2f), CapsuleDirection2D.Vertical, 0, levelLayerMask) == null)
        {
            Vector2 kelpFruitPosition = new Vector2(transform.position.x, transform.position.y + 0.4f);
            Instantiate(kelpFruit, kelpFruitPosition, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
