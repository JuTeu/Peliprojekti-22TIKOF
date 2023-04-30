using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBounce : MonoBehaviour
{
    float startPosition;
    float sequence = 0;
    void Start()
    {
        startPosition = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        sequence += 10 * Time.deltaTime;
        transform.position = new Vector2(transform.position.x, startPosition + Mathf.Sin(sequence) / 5);
    }
}
