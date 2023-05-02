using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SurfaceMenuBehaviour : MonoBehaviour
{
    [SerializeField] GameObject leftButton, rightButton, totalScore, jumpButton, equipmentButton, openShopButton, talkButton;
    [SerializeField] TextMeshProUGUI scoreText, highScoreText;
    RectTransform leftButtonT, rightButtonT, totalScoreT, jumpButtonT, equipmentButtonT, openShopButtonT, talkButtonT;
    Button leftButtonB, rightButtonB, totalScoreB, jumpButtonB, equipmentButtonB, openShopButtonB, talkButtonB;
    RectTransform hpBar, pauseButton, paperCount;
    Rigidbody2D playerRigidbody;
    SpriteRenderer playerSprite, armSprite, hatSprite;
    Animator playerAnimator, armAnimator, hatAnimator;
    bool sequenceStopped = false;
    float sequence, exponentialSequence = 0f;
    int menuAnimId = 0;
    int currentMenu = 0;
    
    void Start()
    {
        GameManager.totalScore += GameManager.score;
        if (GameManager.score > GameManager.highScore) GameManager.highScore = GameManager.score;
        scoreText.text = GameManager.totalScore + "";
        highScoreText.text = GameManager.highScore + "";
        GameManager.score = 0;
        leftButtonT = leftButton.GetComponent<RectTransform>();
        rightButtonT = rightButton.GetComponent<RectTransform>();
        totalScoreT = totalScore.GetComponent<RectTransform>();
        jumpButtonT = jumpButton.GetComponent<RectTransform>();
        equipmentButtonT = equipmentButton.GetComponent<RectTransform>();
        openShopButtonT = openShopButton.GetComponent<RectTransform>();
        talkButtonT = talkButton.GetComponent<RectTransform>();

        leftButtonB = leftButton.GetComponent<Button>();
        rightButtonB = rightButton.GetComponent<Button>();
        jumpButtonB = jumpButton.GetComponent<Button>();
        equipmentButtonB = equipmentButton.GetComponent<Button>();
        openShopButtonB = openShopButton.GetComponent<Button>();
        talkButtonB = talkButton.GetComponent<Button>();

        leftButtonB.interactable = false;
        rightButtonB.interactable = false;
        jumpButtonB.interactable = false;
        equipmentButtonB.interactable = false;
        openShopButtonB.interactable = false;
        talkButtonB.interactable = false;
        

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
        
        GameObject player = GameObject.FindWithTag("Player");
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        playerSprite = player.GetComponent<SpriteRenderer>();
        armSprite = player.transform.Find("Arms").gameObject.GetComponent<SpriteRenderer>();
        hatSprite = player.transform.Find("Hat").gameObject.GetComponent<SpriteRenderer>();
        playerAnimator = player.GetComponent<Animator>();
        armAnimator = player.transform.Find("Arms").gameObject.GetComponent<Animator>();
        hatAnimator = player.transform.Find("Hat").gameObject.GetComponent<Animator>();

        playerAnimator.Play("Idle");
        armAnimator.Play("Idle");
        hatAnimator.Play("Idle");
        //sequence = 12.8f;
        sequence = 11f;
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
        if (id == 1 || id == 2 || id == 3)
        {
            playerAnimator.Play("Walk");
            armAnimator.Play("Walk");
            hatAnimator.Play("Walk");
        }
        if (id == 1 || id == 2)
        {
            equipmentButtonB.interactable = false;
            jumpButtonB.interactable = false;
        }
        if (id == 3)
        {
            equipmentButtonB.interactable = false;
            openShopButtonB.interactable = false;
            talkButtonB.interactable = false;
        }
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
            leftButtonB.interactable = true;
            //rightButtonB.interactable = true;
            jumpButtonB.interactable = true;
            equipmentButtonB.interactable = true;
            sequenceStopped = true;
        }
    }

    bool playerJumped = false;
    bool playerJumped2 = false;

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
            armSprite.flipX = currentMenu == 0 ? true : false;
            hatSprite.flipX = currentMenu == 0 ? true : false;
        }
        if ((playerRigidbody.position.x < -10.64f && playerSprite.flipX) || 
            (playerRigidbody.position.x > -10.66f && !playerSprite.flipX))
        {
            playerAnimator.Play("Idle");
            armAnimator.Play("Idle");
            hatAnimator.Play("Idle");
        }    
        if (sequence > 13.5f)
        {
            currentMenu = 1;
            playerAnimator.Play("Idle");
            armAnimator.Play("Idle");
            hatAnimator.Play("Idle");
            //leftButtonB.interactable = true;
            rightButtonB.interactable = true;
            equipmentButtonB.interactable = true;
            //openShopButtonB.interactable = true;
            talkButtonB.interactable = true;
            sequenceStopped = true;
        }
    }
    public void Talk() 
    {
        GameManager.OpenDialogue();
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
            playerRigidbody.MovePosition(Vector2.MoveTowards(playerRigidbody.position, new Vector2(-4.5f, playerRigidbody.position.y), 5f * Time.deltaTime));
            playerSprite.flipX = false;
            armSprite.flipX = false;
            hatSprite.flipX = false;
        }
        if (playerRigidbody.position.x > -4.51f)
        {
            playerAnimator.Play("Idle");
            armAnimator.Play("Idle");
            hatAnimator.Play("Idle");
        }
        if (sequence > 13.5f)
        {
            currentMenu = 0;
            playerAnimator.Play("Idle");
            armAnimator.Play("Idle");
            hatAnimator.Play("Idle");
            jumpButtonT.anchoredPosition = new Vector2(0, 200);
            leftButtonB.interactable = true;
            //rightButtonB.interactable = true;
            equipmentButtonB.interactable = true;
            jumpButtonB.interactable = true;
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

        if (sequence < 10f && playerRigidbody.position.x < -1.99f)
        {
            playerRigidbody.MovePosition(Vector2.MoveTowards(playerRigidbody.position, new Vector2(-2f, playerRigidbody.position.y), 2.5f * Time.deltaTime));
            playerSprite.flipX = false;
            armSprite.flipX = false;
            hatSprite.flipX = false;
        }
        if (playerRigidbody.position.x > -2.01f && !playerJumped)
        {
            playerAnimator.Play("Idle");
            armAnimator.Play("Idle");
            hatAnimator.Play("Idle");
        }
        if (sequence > 10f && !playerJumped)
        {
            playerJumped = true;
            playerAnimator.Play("Jump");
            armAnimator.Play("Jump");
            hatAnimator.Play("Jump");
        }
        if (sequence > 42f && !playerJumped2)
        {
            playerRigidbody.AddForce(new Vector2(3f, 6.5f), ForceMode2D.Impulse);
            GameManager.PlayerClipping(false);
            GameManager.cameraMode = 100;
            playerJumped2 = true;
        }
        if (sequence > 42f)
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

        if (sequence > 43.5f && sequence < 49f) playerRigidbody.rotation = Mathf.MoveTowards(playerRigidbody.rotation, -180f, 330 * Time.deltaTime);
        if (sequence > 62.5f)
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
