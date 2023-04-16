using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb;
    private InputReader inputReader;
    private SpriteRenderer sprite;
    Vector2 movementVector = Vector2.zero;
    Vector2 oldPosition = Vector2.zero;    
    private bool changedPosition = false;
    private bool touchReleased = false;
    private bool stoppedBecauseButton = false;
    [SerializeField] float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        inputReader = GetComponent<InputReader>();
    }

    Vector2 GetPressedPosition()
    {
        return inputReader.GetMousePosition();
    }

    bool GetBeingPressed()
    {
        return inputReader.GetLeftMouse();
    }

    void FixedUpdate()
    {
        if (!GameManager.playerInControl) return;


        if (GetBeingPressed())
        {
            if (touchReleased && CheckIfPauseButtonIsBeingPressed()) stoppedBecauseButton = true;
            else if (touchReleased) stoppedBecauseButton = false;
            touchReleased = false;
            if (stoppedBecauseButton) return;

            if (oldPosition != GetPressedPosition())
            {
                changedPosition = false;
                oldPosition = GetPressedPosition();
            }
            if (!changedPosition)
            {
                movementVector = GetPressedPosition() - rb.position;
                movementVector = movementVector.normalized;
                changedPosition = true;
            }
            rb.AddForce(movementVector * speed);
            if (movementVector.x < 0f)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
            Vector2 angleComparer = movementVector.y > 0 ? Vector2.right : Vector2.left;
            rb.rotation = Vector2.SignedAngle(Vector2.right, movementVector) - 90;
        }
        else
        {
            changedPosition = false;
            touchReleased = true;
        }

        //DevCamToggle();
    }
    bool CheckIfPauseButtonIsBeingPressed()
    {
        // En tiedä miten tämä pitäisi tehdä...
        return false;
    }

    /*bool preventDoubleInputK = false;
    void DevCamToggle()
    {
        if (preventDoubleInputK && inputReader.GetK())
        {
            CameraMovement cameraObject = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
            if (cameraObject.devCam)
            {
                cameraObject.devCam = false;
            }
            else
            {
                cameraObject.devCam = true;
            }
            Debug.Log("Kehittäjä kamera: " + cameraObject.devCam);
            preventDoubleInputK = false;
        }
        else if (!inputReader.GetK())
        {
            preventDoubleInputK = true;
        }
    }*/
}
