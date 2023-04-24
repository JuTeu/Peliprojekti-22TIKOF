using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitionMenuBehaviour : MonoBehaviour
{
    private float sequence = 0f;
    private bool playerIsInTheTransition = false;
    private bool penultimate = false;
    private bool doSequence = true;
    RectTransform mainTransform;
    GameObject player;
    Rigidbody2D playerRigidbody;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        mainTransform = GetComponent<RectTransform>();
        GameManager.HideStick();
        mainTransform.anchoredPosition = new Vector2(0, 1000);
        GameManager.PauseWorld(true);
        playerRigidbody.velocity = new Vector2(0f, -5f);
        GameManager.ChangeLightSize(50, 0, 10);
    }

    // Update is called once per frame
    void Update()
    {
        sequence += Time.deltaTime;
        if (!doSequence)
        {
            if (GameManager.levelIsGenerated)
            {
                GameManager.ChangeLightSize(0, 50, 10);
                playerRigidbody.velocity = new Vector2(0f, -5f);
                GameManager.CloseLevelTransitionMenu();
            }
            return;
        }
        playerRigidbody.AddForce(new Vector2(0f, -3f));
        if (!playerIsInTheTransition && sequence > 2)
        {
            playerIsInTheTransition = true;
            player.transform.position = new Vector2(20f + (GameManager.currentFloor * 50), 330f);
            playerRigidbody.rotation = -180f;
            player.GetComponent<Animator>().Play("DownSwim");
            GameManager.cameraMode = 2;
            Camera.main.gameObject.GetComponent<Camera>().orthographicSize = 10f;
            GameManager.ChangeLightSize(0, 50, 10);
        }
        if (sequence > 25 && !penultimate)
        {
            penultimate = true;
            GameManager.ChangeLightSize(50, 0, 10);
        }
        if (sequence > 30)
        {
            doSequence = false;
            GameManager.cameraMode = 0;
            Camera.main.gameObject.GetComponent<Camera>().orthographicSize = GameManager.cameraPlaySize;
            GameObject.FindWithTag("Player").transform.position = new Vector2(0f, -18.4f);
            GameManager.GenerateMap(GameManager.currentFloor + 1);
            GameManager.PauseWorld(false);
        }
    }
    void Close()
    {
        GameManager.CloseLevelTransitionMenu();
    }
}
