using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb;
    private InputReader inputReader;
    private SpriteRenderer sprite;
    private Animator anim;
    private int currentAnim = 0;
    Vector2 movementVector = Vector2.zero;
    Vector2 oldPosition = Vector2.zero;
    private bool touchReleased = true;
    private float stickUpperBound;
    [SerializeField] float speed = 1;
    [SerializeField] RectTransform stickPosition, stickChild;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        inputReader = GetComponent<InputReader>();
        stickUpperBound = Screen.height * 0.9f;
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

        if (GetBeingPressed() && touchReleased && GetPressedPosition().y < stickUpperBound)
        {
            stickPosition.position = GetPressedPosition();
            touchReleased = false;
            if (currentAnim == 0)
            {
                anim.Play("UpSwim");
            }
            else if (currentAnim == 1)
            {
                anim.Play("MiddleUpSwim");
            }
            else if (currentAnim == 2)
            {
                anim.Play("MiddleSwim");
            }
            else if (currentAnim == 3)
            {
                anim.Play("MiddleDownSwim");
            }
            else if (currentAnim == 4)
            {
                anim.Play("DownSwim");
            }
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
            if (currentAnim == 0)
            {
                anim.Play("UpSwimIdle");
            }
            else if (currentAnim == 1)
            {
                anim.Play("MiddleUpSwimIdle");
            }
            else if (currentAnim == 2)
            {
                anim.Play("MiddleSwimIdle");
            }
            else if (currentAnim == 3)
            {
                anim.Play("MiddleDownSwimIdle");
            }
            else if (currentAnim == 4)
            {
                anim.Play("DownSwimIdle");
            }
        }

        //movementVector = GetStick();
        rb.AddForce(movementVector * speed);
        if (movementVector != Vector2.zero)
        {
            float rotation = Vector2.SignedAngle(Vector2.right, movementVector) - 90;
            rb.rotation = rotation;
            float nextAnimRot = rotation > 0 ? rotation : rotation * -1;
            int nextAnim;
            if (nextAnimRot >= 0 && nextAnimRot < 36)
            {
                nextAnim = 0;
            }
            else if (nextAnimRot >= 36 && nextAnimRot < 72)
            {
                nextAnim = 1;
            }
            else if (nextAnimRot >= 72 && nextAnimRot < 108 || (nextAnimRot >= 252))
            {
                nextAnim = 2;
            }
            else if ((nextAnimRot >= 108 && nextAnimRot < 144) || (nextAnimRot >= 216 && nextAnimRot < 252))
            {
                nextAnim = 3;
            }
            else
            {
                nextAnim = 4;
            }
            if (currentAnim != nextAnim)
            {
                //Debug.Log(nextAnim + " " + rotation);
                currentAnim = nextAnim;
                if (currentAnim == 0)
                {
                    anim.Play("UpSwim");
                }
                else if (currentAnim == 1)
                {
                    anim.Play("MiddleUpSwim");
                }
                else if (currentAnim == 2)
                {
                    anim.Play("MiddleSwim");
                }
                else if (currentAnim == 3)
                {
                    anim.Play("MiddleDownSwim");
                }
                else if (currentAnim == 4)
                {
                    anim.Play("DownSwim");
                }
            }
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
