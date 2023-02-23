using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb;
    private InputReader inputReader;
    [SerializeField] float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inputReader = GetComponent<InputReader>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (inputReader.GetLeftMouse())
        {
            //transform.position = Vector2.MoveTowards(transform.position, inputReader.GetMousePosition(), speed * Time.fixedDeltaTime);
            Vector2 movementVector = inputReader.GetMousePosition() - rb.position;
            movementVector = movementVector.normalized;
            rb.AddForce(movementVector * speed);
            Vector2 angleComparer = movementVector.y > 0 ? Vector2.right : Vector2.left;
            rb.rotation = Vector2.SignedAngle(Vector2.right, movementVector) - 90;
        }
    }
}
