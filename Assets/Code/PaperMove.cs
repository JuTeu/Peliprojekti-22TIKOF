using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperMove : MonoBehaviour
{
    [SerializeField] private Vector2 direction = Vector2.zero;
    [SerializeField] private float distance = 1; 
    [SerializeField] private float speed = 1;

    private float travelledSoFar = 0;

    // Start is called before the first frame update
    void Start()
    {
        direction = direction.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = direction * speed * Time.deltaTime;

        if(travelledSoFar < distance)
        {
            travelledSoFar += movement.magnitude;
            transform.Translate(movement);
        }
    }
}
