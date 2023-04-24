using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KelpFruitSpawner : MonoBehaviour
{
    [SerializeField] GameObject kelpFruit;
    public LayerMask levelLayerMask;
    private GameObject[] obstructions;
    bool invalidSpawn = false;
    
    void Start()
    {
        Invoke("DoTheThing", 1.5f);
    }
    void DoTheThing()
    {
        obstructions = GameObject.FindGameObjectsWithTag("SpawnedObject");
        foreach (GameObject obstruction in obstructions)
        {
            if (Vector2.Distance(obstruction.transform.position, transform.position) < 2f) invalidSpawn = true;
        }
        if (Vector2.Distance(transform.position, new Vector2(24, -25)) < 5f) invalidSpawn = true;
        if (Physics2D.OverlapCapsule(transform.position, new Vector2(1.4f, 2f), CapsuleDirection2D.Vertical, 0, levelLayerMask) == null && !invalidSpawn)
        {
            Vector2 kelpFruitPosition = new Vector2(transform.position.x, transform.position.y + 0.4f);
            Instantiate(kelpFruit, kelpFruitPosition, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
