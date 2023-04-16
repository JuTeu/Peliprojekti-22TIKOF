using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceGeneratorBehaviour : MonoBehaviour
{
    private float sequence = 0f;
    private float yPos;
    [SerializeField] float shakeAmount = 0.01f;
    [SerializeField] float shakeSpeed = 64f;
    // Start is called before the first frame update
    void Start()
    {
        yPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        sequence += Time.deltaTime;
        if (sequence > 90f) sequence = 0f;
        transform.position = new Vector2(transform.position.x, yPos + Mathf.Sin(shakeSpeed * sequence) * shakeAmount);
    }
}
