using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb;
    private InputReader inputReader;
    private SpriteRenderer sprite, armSprite, hatSprite, speedEffectSprite;
    private Animator anim, armAnim, hatAnim;
    private PlayerAnimator animator;
    public int currentAnim = 0;
    Vector2 movementVector = Vector2.zero;
    Vector2 oldPosition = Vector2.zero;
    private bool movingForwards, speedEffectIsActive, touchReleased = true;
    public bool boostMode;
    private float stickUpperBound, tapTime = 0f;
    public float speed = 1f;
    [SerializeField] RectTransform stickPosition, stickChild;
    [SerializeField] Image joyStick, joyStickCap;
    [SerializeField] GameObject speedEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        armSprite = transform.Find("Arms").gameObject.GetComponent<SpriteRenderer>();
        hatSprite = transform.Find("Hat").gameObject.GetComponent<SpriteRenderer>();
        speedEffectSprite = speedEffect.GetComponent<SpriteRenderer>();
        animator = GetComponent<PlayerAnimator>();
        anim = GetComponent<Animator>();
        armAnim = transform.Find("Arms").gameObject.GetComponent<Animator>();
        hatAnim = transform.Find("Hat").gameObject.GetComponent<Animator>();
        inputReader = GetComponent<InputReader>();
        stickUpperBound = Screen.height * 0.9f;
    }
    void OnBecameInvisible()
    {
        GameManager.playerOnScreen = false;
    }
    void OnBecameVisible()
    {
        GameManager.playerOnScreen = true;
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

   public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
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
        if (!GameManager.playerInControl) 
        {
            if(speedEffectIsActive)
            {
                speedEffect.SetActive(false);
                speedEffectIsActive = false;
            }
            return;
        }
        if (tapTime > -1f) tapTime -= Time.deltaTime;

        if (GetBeingPressed() && touchReleased && GetPressedPosition().y < stickUpperBound)
        {
            if(!speedEffectIsActive)
            {
                speedEffect.SetActive(true);
                speedEffectIsActive = true;
            }
            boostMode = tapTime > 0f;
            if ((GameManager.unlocks & 0b_1_0000) == 0b_1_0000) tapTime = 0.5f;
            if (boostMode)
            {
                joyStick.color = new Color(0.7f, 0.13f, 0.14f, 0.8f);
                joyStickCap.color = new Color(1f, 0.6f, 0f);
            }
            else
            {
                joyStick.color = new Color(0.6f, 0.6f, 0.6f, 0.8f);
                joyStickCap.color = Color.white;
            }
            stickPosition.position = GetPressedPosition();
            touchReleased = false;
            if (currentAnim == 0)
            {
                animator.Play("UpSwim");
            }
            else if (currentAnim == 1)
            {
                animator.Play("MiddleUpSwim");
            }
            else if (currentAnim == 2)
            {
                animator.Play("MiddleSwim");
            }
            else if (currentAnim == 3)
            {
                animator.Play("MiddleDownSwim");
            }
            else if (currentAnim == 4)
            {
                animator.Play("DownSwim");
            }
        }
        else if (GetBeingPressed() && !touchReleased)
        {
            movementVector = GetPressedPosition() - ((Vector2)stickPosition.position);
            stickChild.position = GetPressedPosition();
            movementVector = Vector2.ClampMagnitude(stickChild.anchoredPosition, 100f) / 100f;
            if (boostMode) movementVector *= 2f;
            stickChild.anchoredPosition = Vector2.ClampMagnitude(stickChild.anchoredPosition, 100f);
        }
        else if (!GetBeingPressed() && !touchReleased)
        {
            touchReleased = true;
            movementVector = Vector2.zero;
            stickPosition.anchoredPosition = new Vector2(10000, 10000);
            if (currentAnim == 0)
            {
                animator.Play("UpSwimIdle");
            }
            else if (currentAnim == 1)
            {
                animator.Play("MiddleUpSwimIdle");
            }
            else if (currentAnim == 2)
            {
                animator.Play("MiddleSwimIdle");
            }
            else if (currentAnim == 3)
            {
                animator.Play("MiddleDownSwimIdle");
            }
            else if (currentAnim == 4)
            {
                animator.Play("DownSwimIdle");
            }
        }

        rb.AddForce(movementVector * speed);
        movingForwards = Vector2.SignedAngle(rb.velocity.normalized, movementVector.normalized) < 30f;
        if (rb.velocity.sqrMagnitude > 120f && movingForwards)
        {
            speedEffectSprite.color = new Color(0.3f, 0.4f, 0.9f, (rb.velocity.sqrMagnitude - 120f) / 150f);
        }
        else
        {
            speedEffectSprite.color = new Color(0, 0, 0, 0);
        }
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
                currentAnim = nextAnim;
                if (currentAnim == 0)
                {
                    animator.Play("UpSwim");
                }
                else if (currentAnim == 1)
                {
                    animator.Play("MiddleUpSwim");
                }
                else if (currentAnim == 2)
                {
                    animator.Play("MiddleSwim");
                }
                else if (currentAnim == 3)
                {
                    animator.Play("MiddleDownSwim");
                }
                else if (currentAnim == 4)
                {
                    animator.Play("DownSwim");
                }
            }
            animator.Flip(movementVector.x < 0f);
        }
    }
}
