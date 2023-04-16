using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceMenuBehaviour : MonoBehaviour
{
    [SerializeField] RectTransform leftButton, rightButton, totalScore, jumpButton, equipmentButton;
    RectTransform hpBar, pauseButton, paperCount;
    Rigidbody2D playerRigidbody;
    SpriteRenderer playerSprite;
    bool sequenceStopped = false;
    float sequence, exponentialSequence = 0f;
    int menuAnimId = 0;
    
    void Start()
    {
        leftButton.anchoredPosition = new Vector2(-1000, 0);
        rightButton.anchoredPosition = new Vector2(1000, 0);
        totalScore.anchoredPosition = new Vector2(0, 1000);

        jumpButton.anchoredPosition = new Vector2(0, -1000);
        equipmentButton.anchoredPosition = new Vector2(0, -1000);

        hpBar = GameObject.Find("HPBar").GetComponent<RectTransform>();
        pauseButton = GameObject.Find("PauseButton").GetComponent<RectTransform>();
        paperCount = GameObject.Find("PaperCount").GetComponent<RectTransform>();

        hpBar.anchoredPosition = new Vector2(1000, -135);
        pauseButton.anchoredPosition = new Vector2(-1000, -60);
        paperCount.anchoredPosition = new Vector2(1000, -60);
        
        playerRigidbody = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        playerSprite = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>();
        sequence = 12.8f;
    }

    
    void FixedUpdate()
    {
        if (!sequenceStopped) MenuAnimation();
    }

    public void StartGame()
    {
        DoMenuAnim(1);
    }

    void MenuAnimation()
    {
        if (menuAnimId == 0)
        {
            MenuIn();
        }
        else if (menuAnimId == 1)
        {
            StartGameLoop();
        }
    }

    void DoMenuAnim(int id)
    {
        if (id == 1) sequence = 0f;
        exponentialSequence = 0f;
        menuAnimId = id;
        sequenceStopped = false;
    }

    void MenuIn()
    {
        sequence += 2 * Time.deltaTime;
        exponentialSequence = Mathf.Pow(2, sequence);
        leftButton.anchoredPosition = new Vector2(-1000 + exponentialSequence / 10 + 80, 0);
        rightButton.anchoredPosition = new Vector2(1000 - exponentialSequence / 10 - 80, 0);
        totalScore.anchoredPosition = new Vector2(0, 1000 - exponentialSequence / 10 - 100);
        jumpButton.anchoredPosition = new Vector2(0, -1000 + exponentialSequence / 10 + 200);
        equipmentButton.anchoredPosition = new Vector2(0, -1000 + exponentialSequence / 10 + 50);

        if (exponentialSequence > 10000f)
        {
            leftButton.anchoredPosition = new Vector2(80, 0);
            rightButton.anchoredPosition = new Vector2(-80, 0);
            totalScore.anchoredPosition = new Vector2(0, -100);
            jumpButton.anchoredPosition = new Vector2(0, 200);
            equipmentButton.anchoredPosition = new Vector2(0, 50);
            sequenceStopped = true;
        }
    }

    bool playerJumped = false;
    void StartGameLoop()
    {
        sequence += 10 * Time.deltaTime;
        if (exponentialSequence < 10000f)
        {
            exponentialSequence = Mathf.Pow(2, sequence);
            leftButton.anchoredPosition = new Vector2(-exponentialSequence / 10 + 80, 0);
            rightButton.anchoredPosition = new Vector2(exponentialSequence / 10 - 80, 0);
            totalScore.anchoredPosition = new Vector2(0, exponentialSequence / 10 - 100);
            jumpButton.anchoredPosition = new Vector2(0, -exponentialSequence / 10 + 200);
            equipmentButton.anchoredPosition = new Vector2(0, -exponentialSequence / 10 + 50);
        }

        if (sequence < 22f)
        {
            playerRigidbody.MovePosition(Vector2.MoveTowards(playerRigidbody.position, new Vector2(-2f, -16.6f), 2.5f * Time.deltaTime));
            playerSprite.flipX = false;
        }

        if (sequence > 22f && !playerJumped)
        {
            playerJumped = true;
            playerRigidbody.AddForce(new Vector2(3f, 6f), ForceMode2D.Impulse);
            GameManager.PlayerClipping(false);
            GameManager.cameraMode = 100;
        }
        if (sequence > 22f)
        {
            exponentialSequence = Mathf.Pow(2, sequence - 22f);

            hpBar.anchoredPosition = new Vector2(1000 - exponentialSequence / 10 - 130, -135);
            pauseButton.anchoredPosition = new Vector2(-1000 + exponentialSequence / 10 + 60, -60);
            paperCount.anchoredPosition = new Vector2(1000 - exponentialSequence / 10 - 130, -60);

            if (exponentialSequence > 10000f)
            {
                hpBar.anchoredPosition = new Vector2(-130, -135);
                pauseButton.anchoredPosition = new Vector2(60, -60);
                paperCount.anchoredPosition = new Vector2(-130, -60);
            }
        }

        if (sequence > 23.5f && sequence < 29f) playerRigidbody.rotation = Mathf.MoveTowards(playerRigidbody.rotation, -180f, 330 * Time.deltaTime);
        if (sequence > 42.5f)
        {
            playerRigidbody.gravityScale = 0f;
            GameManager.PlayerClipping(true);
            GameManager.playMode = 0;
            GameManager.PauseWorld(false);
            sequenceStopped = true;
            playerRigidbody.velocity = new Vector2(0.1f, -4f);
            GameManager.CloseSurfaceMenu();
        }
    }
}
