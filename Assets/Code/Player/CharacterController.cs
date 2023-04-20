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
    private bool touchReleased = true;
    private float screenSize;
    [SerializeField] float speed = 1;
    [SerializeField] RectTransform stickPosition, stickChild;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        inputReader = GetComponent<InputReader>();
        screenSize = Screen.width / 5;
    }

    Vector2 GetPressedPosition()
    {
        return inputReader.GetMousePosition();
    }

    Vector2 GetStick()
    {
        return inputReader.GetStick();
    }

    bool GetBeingPressed()
    {
        return inputReader.GetLeftMouse();
    }

    void FixedUpdate()
    {
        if (GameManager.playMode == 0)
        {
            UnderWaterMovement();
        }
        else if (GameManager.playMode == 1)
        {
            //SurfaceMovement();
        }
    }
    
    void UnderWaterMovement()
    {
        if (!GameManager.playerInControl) return;

        if (GetBeingPressed() && touchReleased)
        {
            stickPosition.position = GetPressedPosition();
            touchReleased = false;
        }
        else if (GetBeingPressed() && !touchReleased)
        {
            movementVector = GetPressedPosition() - ((Vector2)stickPosition.position);
            stickChild.position = GetPressedPosition();
            movementVector = Vector2.ClampMagnitude(stickChild.anchoredPosition, 100f) / 100f;
            stickChild.anchoredPosition = Vector2.ClampMagnitude(stickChild.anchoredPosition, 100f);
        }
        else if (!GetBeingPressed() && !touchReleased)
        {
            touchReleased = true;
            movementVector = Vector2.zero;
            stickPosition.anchoredPosition = new Vector2(10000, 10000);
        }

        //movementVector = GetStick();
        rb.AddForce(movementVector * speed);
        if (movementVector != Vector2.zero)
        {
            rb.rotation = Vector2.SignedAngle(Vector2.right, movementVector) - 90;
            if (movementVector.x < 0f)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
        }
        /*if (GetBeingPressed())
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
        }*/
    }

    void SurfaceMovement()
    {
        /*if (GetBeingPressed())
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
                movementVector.y = 0f;
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
        }
        else
        {
            changedPosition = false;
            touchReleased = true;
        }*/
    }

    bool CheckIfPauseButtonIsBeingPressed()
    {
        // En tiedä miten tämä pitäisi tehdä...
        return false;
    }
}
