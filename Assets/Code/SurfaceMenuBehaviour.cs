using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurfaceMenuBehaviour : MonoBehaviour
{
    [SerializeField] GameObject leftButton, rightButton, totalScore, jumpButton, equipmentButton, openShopButton, talkButton;
    RectTransform leftButtonT, rightButtonT, totalScoreT, jumpButtonT, equipmentButtonT, openShopButtonT, talkButtonT;
    Button leftButtonB, rightButtonB, totalScoreB, jumpButtonB, equipmentButtonB, openShopButtonB, talkButtonB;
    RectTransform hpBar, pauseButton, paperCount;
    Rigidbody2D playerRigidbody;
    SpriteRenderer playerSprite;
    bool sequenceStopped = false;
    float sequence, exponentialSequence = 0f;
    int menuAnimId = 0;
    int currentMenu = 0;
    
    void Start()
    {
        leftButtonT = leftButton.GetComponent<RectTransform>();
        rightButtonT = rightButton.GetComponent<RectTransform>();
        totalScoreT = totalScore.GetComponent<RectTransform>();
        jumpButtonT = jumpButton.GetComponent<RectTransform>();
        equipmentButtonT = equipmentButton.GetComponent<RectTransform>();
        openShopButtonT = openShopButton.GetComponent<RectTransform>();
        talkButtonT = talkButton.GetComponent<RectTransform>();

        leftButtonB = leftButton.GetComponent<Button>();
        rightButtonB = rightButton.GetComponent<Button>();
        totalScoreB = totalScore.GetComponent<Button>();
        jumpButtonB = jumpButton.GetComponent<Button>();
        equipmentButtonB = equipmentButton.GetComponent<Button>();
        openShopButtonB = openShopButton.GetComponent<Button>();
        talkButtonB = talkButton.GetComponent<Button>();

        leftButtonT.anchoredPosition = new Vector2(-1000, 0);
        rightButtonT.anchoredPosition = new Vector2(1000, 0);
        totalScoreT.anchoredPosition = new Vector2(0, 1000);

        jumpButtonT.anchoredPosition = new Vector2(0, -1000);
        equipmentButtonT.anchoredPosition = new Vector2(0, -1000);

        openShopButtonT.anchoredPosition = new Vector2(0, -1000);
        talkButtonT.anchoredPosition = new Vector2(0, -1000);

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

    public void PressLeft()
    {
        if (currentMenu == 0)
        {
            DoMenuAnim(2);
        }
    }

    public void PressRight()
    {
        if (currentMenu == 1)
        {
            DoMenuAnim(3);
        }
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
        else if (menuAnimId == 2)
        {
            GoToBearMenu();
        }
        else if (menuAnimId == 3)
        {
            GoToHole();
        }
    }

    void DoMenuAnim(int id)
    {
        if (id != 0) sequence = 0f;
        exponentialSequence = 0f;
        menuAnimId = id;
        sequenceStopped = false;
        leftButtonB.interactable = false;
        rightButtonB.interactable = false;
    }

    void MenuIn()
    {
        sequence += 2 * Time.deltaTime;
        exponentialSequence = Mathf.Pow(2, sequence);
        leftButtonT.anchoredPosition = new Vector2(-1000 + exponentialSequence / 10 + 80, 0);
        rightButtonT.anchoredPosition = new Vector2(1000 - exponentialSequence / 10 - 80, 0);
        totalScoreT.anchoredPosition = new Vector2(0, 1000 - exponentialSequence / 10 - 100);
        jumpButtonT.anchoredPosition = new Vector2(0, -1000 + exponentialSequence / 10 + 200);
        equipmentButtonT.anchoredPosition = new Vector2(0, -1000 + exponentialSequence / 10 + 50);

        if (exponentialSequence > 10000f)
        {
            leftButtonT.anchoredPosition = new Vector2(80, 0);
            rightButtonT.anchoredPosition = new Vector2(-80, 0);
            totalScoreT.anchoredPosition = new Vector2(0, -100);
            jumpButtonT.anchoredPosition = new Vector2(0, 200);
            equipmentButtonT.anchoredPosition = new Vector2(0, 50);
            sequenceStopped = true;
        }
    }

    bool playerJumped = false;

    void GoToBearMenu()
    {
        sequence += 10 * Time.deltaTime;
        if (exponentialSequence < 10000f)
        {
            exponentialSequence = Mathf.Pow(2, sequence);
            jumpButtonT.anchoredPosition = new Vector2(0, -exponentialSequence / 10 + 200);

            openShopButtonT.anchoredPosition = new Vector2(-1000 + exponentialSequence / 10, 260);
            talkButtonT.anchoredPosition = new Vector2(1000 - exponentialSequence / 10, 150);

            if (exponentialSequence > 10000f)
            {
                openShopButtonT.anchoredPosition = new Vector2(0, 260);
                talkButtonT.anchoredPosition = new Vector2(0, 150);
            }
        }
        if (sequence < 22f)
        {
            playerRigidbody.MovePosition(Vector2.MoveTowards(playerRigidbody.position, new Vector2(-10.65f, playerRigidbody.position.y), 5f * Time.deltaTime));
            playerSprite.flipX = currentMenu == 0 ? true : false;
        }
        if (sequence > 14f)
        {
            currentMenu = 1;
            leftButtonB.interactable = true;
            rightButtonB.interactable = true;
            sequenceStopped = true;
        }
    }

    void GoToHole()
    {
        sequence += 10 * Time.deltaTime;
        if (exponentialSequence < 10000f)
        {
            exponentialSequence = Mathf.Pow(2, sequence);

            jumpButtonT.anchoredPosition = new Vector2(0, -1000 + exponentialSequence / 10 + 200);

            openShopButtonT.anchoredPosition = new Vector2(-exponentialSequence / 10, 260);
            talkButtonT.anchoredPosition = new Vector2(exponentialSequence / 10, 150);

            if (exponentialSequence > 10000f)
            {
                jumpButtonT.anchoredPosition = new Vector2(0, 200);
            }
        }
        if (sequence < 22f)
        {
            playerRigidbody.MovePosition(Vector2.MoveTowards(playerRigidbody.position, new Vector2(-5f, playerRigidbody.position.y), 5f * Time.deltaTime));
            playerSprite.flipX = false;
        }
        if (sequence > 14f)
        {
            currentMenu = 0;
            leftButtonB.interactable = true;
            rightButtonB.interactable = true;
            sequenceStopped = true;
        }
    }

    void StartGameLoop()
    {
        sequence += 10 * Time.deltaTime;
        if (exponentialSequence < 10000f)
        {
            exponentialSequence = Mathf.Pow(2, sequence);
            leftButtonT.anchoredPosition = new Vector2(-exponentialSequence / 10 + 80, 0);
            rightButtonT.anchoredPosition = new Vector2(exponentialSequence / 10 - 80, 0);
            totalScoreT.anchoredPosition = new Vector2(0, exponentialSequence / 10 - 100);
            jumpButtonT.anchoredPosition = new Vector2(0, -exponentialSequence / 10 + 200);
            equipmentButtonT.anchoredPosition = new Vector2(0, -exponentialSequence / 10 + 50);
        }

        if (sequence < 22f)
        {
            playerRigidbody.MovePosition(Vector2.MoveTowards(playerRigidbody.position, new Vector2(-2f, playerRigidbody.position.y), 2.5f * Time.deltaTime));
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
