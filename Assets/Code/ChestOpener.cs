using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpener : MonoBehaviour
{
    public GameObject Chest,ChestAnimated,Paper;
    private bool opened = false;
    //public GameObject QuestionBG;

    private bool playerEntered = false;
    private GameObject player;
    private Rigidbody2D playerRigidbody;
    private SpriteRenderer playerSprite;
    private Vector2 playerDesiredPosition;


    // Start is called before the first frame update
    void Start()
    {
        ChestAnimated.SetActive(false);
        Paper.SetActive(false);
        //QuestionBG.SetActive(false);     Tähän on parempi käyttää erillistä sceneä ja scenen käyttäminen toimii paremmin prefabeilla.

        playerDesiredPosition = transform.position;
        playerDesiredPosition.x -= 0.7f;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!opened)
        {
            opened = true;
            //QuestionBG.SetActive(true);
            
            player = collision.gameObject;
            playerRigidbody = player.GetComponent<Rigidbody2D>();
            playerSprite = player.GetComponent<SpriteRenderer>();
            playerSprite.flipX = false;
            playerEntered = true;
            GameManager.EnablePauseButton(false);
            GameManager.playerInControl = false;
            //GameManager.OpenQuestionMenu();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //QuestionBG.SetActive(false);
    }

    void FixedUpdate()
    {
        if (playerEntered) OpeningSequence();
    }

    private float sequence = 0f;
    private bool chestOpened = false;
    void OpeningSequence()
    {
        sequence += Time.deltaTime;

        playerRigidbody.rotation = Mathf.MoveTowards(playerRigidbody.rotation, 0f, 600 * Time.deltaTime);
        playerRigidbody.MovePosition(Vector2.MoveTowards(player.transform.position, playerDesiredPosition, 2 * Time.deltaTime));

        if (sequence < 1f && player.transform.position.x == playerDesiredPosition.x)
        {
            sequence = 1f;
        }

        if (sequence > 1f && !chestOpened)
        {
            chestOpened = true;
            Destroy(Chest);
            ChestAnimated.SetActive(true);
            Paper.SetActive(true);
        }

        if (sequence > 4f)
        {
            playerEntered = false;
            GameManager.OpenQuestionMenu();
            playerRigidbody.velocity = new Vector2(0f, 0f);
        }
    }
}
