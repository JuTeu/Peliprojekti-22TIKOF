using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceGeneratorBehaviour : MonoBehaviour
{
    private float sequence = 0f;
    private float xPos;
    private float yPos;
    [SerializeField] float shakeAmount = 0.01f;
    [SerializeField] float shakeSpeed = 64f;
    // Start is called before the first frame update
    void Start()
    {
        xPos = transform.position.x;
        yPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        sequence += Time.deltaTime;
        if (sequence > 90f) sequence = 0f;
        transform.position = new Vector2(xPos + Mathf.Sin(shakeSpeed * sequence / 2) * shakeAmount, yPos + Mathf.Sin(shakeSpeed * sequence) * shakeAmount);
    }
}
