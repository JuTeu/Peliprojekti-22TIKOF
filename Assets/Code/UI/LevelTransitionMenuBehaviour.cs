using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTransitionMenuBehaviour : MonoBehaviour
{
    float sequence = 0f, exponentialSequence = 0f, tallyDelay = 0f, tallyDelayOld = 0f;
    bool tallyingFinished = false;
    int sequenceOrder = 0;

    [SerializeField] GameObject noDamageObj, allCorrectObj;
    [SerializeField] TextMeshProUGUI timeText, timeTextDropShadow, healthText, healthTextDropShadow, scoreText, scoreTextDropShadow, noDamageText, noDamageTextDropShadow, allCorrectText, allCorrectTextDropShadow;
    [SerializeField] Sprite bearNeutral, bearHappy;
    bool allCorrect, unhurt;
    int time, health, points, score, noDamageBonus = 3000, allCorrectBonus = 5000, floor = GameManager.currentFloor;
    float spawnRoomLocation;
    RectTransform mainTransform, hpBar, pauseButton, paperCount;
    GameObject player, trophy, bear;
    PlayerAnimator playerAnimator;
    CharacterController playerController;
    Rigidbody2D playerRigidbody;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        mainTransform = GetComponent<RectTransform>();
        hpBar = GameObject.Find("HPBar").GetComponent<RectTransform>();
        pauseButton = GameObject.Find("PauseButton").GetComponent<RectTransform>();
        paperCount = GameObject.Find("PaperCount").GetComponent<RectTransform>();
        playerAnimator = player.GetComponent<PlayerAnimator>();
        playerController = player.GetComponent<CharacterController>();

        if (GameManager.currentFloor == 4) trophy = GameObject.Find("CutsceneTrophy");

        allCorrect = GameManager.correctAnswers >= GameManager.chestsInLevel;
        unhurt = GameManager.unhurt;
        time = 1000;
        health = (int) (player.GetComponent<PlayerHealth>().healthAmount * 10);
        score = GameManager.score;
        if (GameManager.currentFloor + 1 == 1)
        {
            spawnRoomLocation = 24f;
        }
        else
        {
            spawnRoomLocation = 0f;
        }

        if (!unhurt) noDamageObj.SetActive(false);
        if (!allCorrect) allCorrectObj.SetActive(false);
        RefreshMenu();
        GameManager.HideStick();
        mainTransform.anchoredPosition = new Vector2(0, 1000);
        GameManager.PauseWorld(true);
        if (GameManager.currentFloor != 4)
        {
            playerRigidbody.rotation = -180f;
            playerAnimator.Play("DownSwim");
            playerRigidbody.velocity = new Vector2(0f, -5f);
        }
        else
        {
            bear = GameObject.Find("VKHead");
        }
        GameManager.ChangeLightSize(50, 0, 10);
    }

    void FixedUpdate()
    {
        sequence += Time.deltaTime;

        if (GameManager.currentFloor == 0) ToKelpTransition();
        else if (GameManager.currentFloor == 4) UpwardsTunnelTransition();
        else if (GameManager.currentFloor > 0) TunnelTransition();

    }
    void Tallying()
    {
        tallyDelay += Time.deltaTime;
        if (tallyDelay > tallyDelayOld + 0.07f)
        {
            tallyDelayOld = tallyDelay;
            if (time > 0)
            {
                points = time < 20 ? time : 20;
                time -= points;
                score += points;
            }
            else if (health > 0)
            {
                points = health < 20 ? health : 20;
                health -= points;
                score += points;
            }
            else if (allCorrect && allCorrectBonus > 0)
            {
                points = allCorrectBonus < 40 ? allCorrectBonus : 40;
                allCorrectBonus -= points;
                score += points;
            }
            else if (unhurt && noDamageBonus > 0)
            {
                points = noDamageBonus < 40 ? noDamageBonus : 40;
                noDamageBonus -= points;
                score += points;
            }
            else
            {
                GameManager.score = score;
                player.GetComponent<PlayerHealth>().Heal(100f);
                tallyingFinished = true;
                GameObject.Find("PaperCount").GetComponent<UIPaperCount>().AddScore(0);
                sequence = 18f;
            }
            RefreshMenu();
            if (!tallyingFinished && sequence > 10) sequence = 11f; 
        }
    }
    void PointOperation(int input)
    {
        points = input < 20 ? points = input : 20;
        input -= points;
        score += points;
    }
    void RefreshMenu()
    {
        timeText.text = time + "";
        timeTextDropShadow.text = time + "";
        healthText.text = health + "";
        healthTextDropShadow.text = health + "";
        scoreText.text = score + "";
        scoreTextDropShadow.text = score + "";
        if (unhurt)
        {
            noDamageText.text = noDamageBonus + "";
            noDamageTextDropShadow.text = noDamageBonus + "";
        }
        if (allCorrect)
        {
            allCorrectText.text = allCorrectBonus + "";
            allCorrectTextDropShadow.text = allCorrectBonus + "";
        }
    }
    void TunnelTransition()
    {
        if (exponentialSequence < 10000f && sequenceOrder == 1)
        {
            exponentialSequence = Mathf.Pow(2, sequence * 3);
            mainTransform.anchoredPosition = new Vector2(0, 1000 - exponentialSequence / 10 - 200);
            hpBar.anchoredPosition = new Vector2(exponentialSequence / 10 - 130, -135);
            pauseButton.anchoredPosition = new Vector2(-exponentialSequence / 10 + 60, -60);
            paperCount.anchoredPosition = new Vector2(exponentialSequence / 10 - 130, -60);

            if (exponentialSequence > 10000f)
            {
                mainTransform.anchoredPosition = new Vector2(0, -200);
            }
        }
        if (exponentialSequence < 10000f && sequenceOrder > 1)
        {
            exponentialSequence = Mathf.Pow(2, (sequence - 20) * 4);
            mainTransform.anchoredPosition = new Vector2(0, exponentialSequence / 10 - 200);
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


        if (exponentialSequence >= 10000f && !tallyingFinished) Tallying();

        playerRigidbody.velocity = new Vector2(0f, -5f);

        if (sequence > 2 && sequenceOrder == 0)
        {
            sequenceOrder = 1;
            player.transform.position = new Vector2(59f + (floor * 50), 330f);
            playerRigidbody.rotation = -180f;
            player.GetComponent<Animator>().Play("DownSwim");
            GameManager.cameraMode = 2;
            Camera.main.gameObject.GetComponent<Camera>().orthographicSize = 10f;
            GameManager.ChangeLightSize(0, 50, 10);
        }
        if (sequence > 5 && sequenceOrder == 1) GameManager.cameraMode = 4;
        if (sequence > 20 && sequenceOrder == 1)
        {
            
            exponentialSequence = 0f;
            GameManager.cameraMode = 3;
            sequenceOrder = 2;
            GameManager.ChangeLightSize(50, 0, 10);
            if (GameManager.currentFloor + 1 == 4)
            {
                //Tää on syvältä
                GameManager.cameraMode = 0;
                Camera.main.gameObject.GetComponent<Camera>().orthographicSize = GameManager.cameraPlaySize;
                GameManager.PlayerClipping(true);
                playerRigidbody.position = new Vector2(spawnRoomLocation, -25f);
                playerRigidbody.velocity = new Vector2(0f, -5f);
                hpBar.anchoredPosition = new Vector2(-130, -135);
                pauseButton.anchoredPosition = new Vector2(60, -60);
                paperCount.anchoredPosition = new Vector2(-130, -60);
                GameManager.PauseWorld(false);
                GameManager.ChangeLightSize(0, 50, 10);
            }
            GameManager.GenerateMap(GameManager.currentFloor + 1);
        }
        if (sequence > 23 && GameManager.levelIsGenerated && sequenceOrder == 2)
        {
            sequenceOrder = 3;
            GameManager.cameraMode = 0;
            Camera.main.gameObject.GetComponent<Camera>().orthographicSize = GameManager.cameraPlaySize;
            GameManager.PlayerClipping(false);
            playerRigidbody.position = new Vector2(spawnRoomLocation, -14f);
            playerRigidbody.velocity = new Vector2(0f, -5f);
            GameManager.PauseWorld(false);
            GameManager.ChangeLightSize(0, 50, 10);
        }
        if (sequence > 24 && sequenceOrder == 3)
        {
            sequenceOrder = 4;
            GameManager.PlayerClipping(true);
            player.GetComponent<Animator>().Play("DownSwim");
        }
        if (sequence > 24.3f && sequenceOrder == 4)
        {
            player.GetComponent<Animator>().Play("DownSwimIdle");
            GameManager.CloseLevelTransitionMenu();
        }
    }
    bool playerLanded = false;
    void UpwardsTunnelTransition()
    {
        if (exponentialSequence < 10000f && sequenceOrder == 1)
        {
            exponentialSequence = Mathf.Pow(2, sequence * 3);
            mainTransform.anchoredPosition = new Vector2(0, 1000 - exponentialSequence / 10 - 200);
            hpBar.anchoredPosition = new Vector2(exponentialSequence / 10 - 130, -135);
            pauseButton.anchoredPosition = new Vector2(-exponentialSequence / 10 + 60, -60);
            paperCount.anchoredPosition = new Vector2(exponentialSequence / 10 - 130, -60);

            if (exponentialSequence > 10000f)
            {
                mainTransform.anchoredPosition = new Vector2(0, -200);
            }
        }
        if (exponentialSequence < 10000f && sequenceOrder > 1)
        {
            exponentialSequence = Mathf.Pow(2, (sequence - 20) * 4);
            mainTransform.anchoredPosition = new Vector2(0, exponentialSequence / 10 - 200);
        }


        if (exponentialSequence >= 10000f && !tallyingFinished) Tallying();

        if (sequenceOrder > 0 && !(sequenceOrder > 2 && trophy.transform.position.y > 90f))
        {
            player.transform.localPosition = new Vector2(0f, 1f);
            trophy.transform.position = new Vector2(trophy.transform.position.x, trophy.transform.position.y + 0.05f);
            playerRigidbody.velocity = Vector2.zero;
        } else if (sequenceOrder == 3)
        {
            sequenceOrder = 4;
        }
        if (sequence > 2 && sequenceOrder == 0)
        {
            sequenceOrder = 1;
            player.transform.parent = null;
            trophy.transform.position = new Vector2(59f + (floor * 50), 250f);
            player.transform.parent = trophy.transform;
            player.transform.localPosition = new Vector2(0f, 1f);
            playerController.currentAnim = 1;
            playerAnimator.Play("MiddleUpSwimIdle");
            GameManager.cameraMode = 2;
            Camera.main.gameObject.GetComponent<Camera>().orthographicSize = 10f;
            GameManager.ChangeLightSize(0, 50, 10);
        }
        if (sequence > 5 && sequenceOrder == 1) GameManager.cameraMode = 4;
        if (sequence > 20 && sequenceOrder == 1)
        {
            
            exponentialSequence = 0f;
            GameManager.cameraMode = 3;
            sequenceOrder = 2;
            GameManager.ChangeLightSize(50, 0, 10);
        }
        if (sequence > 23 && sequenceOrder == 2)
        {
            playerAnimator.Flip(false);
            bear.transform.localPosition = new Vector2(55f, -0.38f);
            bear.GetComponent<SpriteRenderer>().sprite = bearNeutral;
            Camera.main.transform.position = new Vector3(-154f, 92f, -10f);
            Camera.main.gameObject.GetComponent<Camera>().orthographicSize = 14f;
            GameManager.ChangeLightSize(0, 100, 10);
            trophy.transform.position = new Vector2(-154.45f, 80f);
            sequenceOrder = 3;
        }

        if (sequence > 25 && sequenceOrder == 4)
        {
            GameManager.playMode = 1;
            player.transform.parent = null;
            playerRigidbody.gravityScale = 1f;
            playerRigidbody.AddForce(new Vector2(2f, 6.5f), ForceMode2D.Impulse);
            GameManager.PlayerClipping(true);
            sequenceOrder = 5;
        }
        if (sequence > 26.7f)
        {
            bear.transform.localPosition = new Vector2(bear.transform.localPosition.x, -0.38f + (Mathf.Sin(sequence * 20) / 100));
        }
        if (sequence > 26 && sequenceOrder == 5)
        {
            sequenceOrder = 6;
            bear.GetComponent<SpriteRenderer>().sprite = bearHappy;
        }
        if (!playerLanded && sequenceOrder > 4 && player.transform.position.y < 89.5f)
        {
            playerLanded = true;
            playerAnimator.Play("JumpFromTrophy");
        }
        if (sequence > 32 && sequenceOrder == 6)
        {
            GameManager.ChangeLightSize(50, 0, 10);
            sequenceOrder = 7;
        }
        if (sequence > 34 && sequenceOrder == 7)
        {
            GameManager.BeginGame();
            GameManager.CloseLevelTransitionMenu();
        }
    }
    void ToKelpTransition()
    {
        if (exponentialSequence < 10000f && sequenceOrder == 1)
        {
            exponentialSequence = Mathf.Pow(2, sequence * 3);
            mainTransform.anchoredPosition = new Vector2(0, 1000 - exponentialSequence / 10 - 200);
            hpBar.anchoredPosition = new Vector2(exponentialSequence / 10 - 130, -135);
            pauseButton.anchoredPosition = new Vector2(-exponentialSequence / 10 + 60, -60);
            paperCount.anchoredPosition = new Vector2(exponentialSequence / 10 - 130, -60);

            if (exponentialSequence > 10000f)
            {
                mainTransform.anchoredPosition = new Vector2(0, -200);
            }
        }
        if (exponentialSequence < 10000f && sequenceOrder > 1)
        {
            exponentialSequence = Mathf.Pow(2, (sequence - 20) * 4);
            mainTransform.anchoredPosition = new Vector2(0, exponentialSequence / 10 - 200);
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


        if (exponentialSequence >= 10000f && !tallyingFinished) Tallying();

        if (sequence < 10)
        {
            playerRigidbody.velocity = new Vector2(0f, -5f);
        }
        else if (sequence > 15)
        {
            playerRigidbody.velocity = new Vector2(0f, -5f);
        }
        else
        {
            playerRigidbody.velocity = new Vector2(0f, 0f);
        }
        if (sequence > 2 && sequenceOrder == 0)
        {
            sequenceOrder = 1;
            player.transform.position = new Vector2(59f + (floor * 50), 330f);
            playerRigidbody.rotation = -180f;
            player.GetComponent<Animator>().Play("DownSwim");
            GameManager.cameraMode = 2;
            Camera.main.gameObject.GetComponent<Camera>().orthographicSize = 10f;
            GameManager.ChangeLightSize(0, 50, 10);
        }
        if (sequence > 20 && sequenceOrder == 1)
        {
            playerRigidbody.position = new Vector2(59f + (floor * 50), 255f);
            exponentialSequence = 0f;
            sequenceOrder = 2;
            GameManager.ChangeLightSize(50, 0, 10);
            GameManager.GenerateMap(GameManager.currentFloor + 1);
        }
        if (sequence > 23 && GameManager.levelIsGenerated && sequenceOrder == 2)
        {
            sequenceOrder = 3;
            GameManager.cameraMode = 0;
            Camera.main.gameObject.GetComponent<Camera>().orthographicSize = GameManager.cameraPlaySize;
            GameManager.PlayerClipping(false);
            playerRigidbody.position = new Vector2(spawnRoomLocation, -14f);
            playerRigidbody.velocity = new Vector2(0f, -5f);
            GameManager.PauseWorld(false);
            GameManager.ChangeLightSize(0, 50, 10);
        }
        if (sequence > 24 && sequenceOrder == 3)
        {
            sequenceOrder = 4;
            GameManager.PlayerClipping(true);
            player.GetComponent<Animator>().Play("DownSwim");
        }
        if (sequence > 24.3f && sequenceOrder == 4)
        {
            player.GetComponent<Animator>().Play("DownSwimIdle");
            GameManager.CloseLevelTransitionMenu();
        }
    }
}
