using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefrigeratorBehaviour : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] GameObject effect;
    private SpriteRenderer doorSprite;
    private SpriteRenderer effectSprite;
    [SerializeField] Sprite doorFront;
    [SerializeField] Sprite doorBack;
    [SerializeField] Sprite bodyOff;
    [SerializeField] LineRenderer cable;
    [SerializeField] Vector3 cableEnd;
    private bool playerEntered = false;
    private GameObject player;
    private Rigidbody2D playerRigidbody;
    private PlayerHealth playerHealth;
    private float sequence = 0f;
    private float doorRotation = 0f;
    private Vector3 hinge;
    private Vector2 refrigeratorCentre;

    void Start()
    {
        doorSprite = door.GetComponent<SpriteRenderer>();
        effectSprite = effect.GetComponent<SpriteRenderer>();
        cable.SetPosition(0, cable.gameObject.transform.position);
        cable.SetPosition(1, cableEnd);

        hinge = transform.position;
        hinge.x -= 0.5f;
        refrigeratorCentre = transform.position;
        refrigeratorCentre.y -= 0.3f;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && sequence < 1f)
        {
            player = collision.gameObject;
            playerRigidbody = player.GetComponent<Rigidbody2D>();
            playerHealth = player.GetComponent<PlayerHealth>();
            playerEntered = true;
            GameManager.playerInControl = false;
            GameManager.EnablePauseButton(false);
        }
    }
    void FixedUpdate()
    {
        if (playerEntered)
        {
            refrigeratorSequence();
        }
    }
    void refrigeratorSequence()
    {
        sequence += Time.deltaTime;
        if (sequence < 1f)
        {
            doorRotation = 10 * sequence;
            if (doorRotation > 4.62f) doorRotation = 4.62f;
            door.transform.RotateAround(hinge, Vector3.up, doorRotation);
            if (sequence > 0.6f)
            {
                doorSprite.flipX = true;
                doorSprite.sprite = doorBack;
            }
        }
        if (sequence < 1.1f && sequence > 1f) doorSprite.sortingOrder = 5;
        if (sequence < 2.15f && sequence > 1.15f)
        {
            doorRotation = -10 * sequence;
            if (doorRotation < -3.6f) doorRotation = -3.6f;
            door.transform.RotateAround(hinge, Vector3.up, doorRotation);
            if (sequence > 1.6f)
            {
                doorSprite.flipX = false;
                doorSprite.sprite = doorFront;
            }
        }

        if (sequence > 2.15f)
        {
            effectSprite.color = new Color(1f, 1f, 1f, Mathf.Sin(32 * sequence) / 2 + 0.5f); 
        }
        playerRigidbody.rotation = Mathf.MoveTowards(playerRigidbody.rotation, 0f, 600 * Time.deltaTime);
        playerRigidbody.MovePosition(Vector2.MoveTowards(player.transform.position, refrigeratorCentre, 1 * Time.deltaTime));
        playerHealth.Heal(10 * Time.deltaTime);

        if (sequence > 4f)
        {
            GetComponent<SpriteRenderer>().sprite = bodyOff;
            door.transform.RotateAround(hinge, Vector3.up, 25f);
            playerEntered = false;
            playerRigidbody.velocity = new Vector2(0.1f, 0.1f);
            GameManager.playerInControl = true;
            GameManager.EnablePauseButton(true);
            effectSprite.color = new Color(1f, 1f, 1f, 0f);
            doorSprite.sortingOrder = -1;
        }

    }
}
