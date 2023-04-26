using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExitBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject deepCaveFinalReward, rope;
    SpriteRenderer deepCaveFinalRewardSprite;
    GameObject player;
    Rigidbody2D playerRigidbody;
    Vector2 aboveReward;
    bool exitSequence = false;
    void Start()
    {
        if (GameManager.currentFloor == 0) 
        {
            deepCaveFinalRewardSprite = deepCaveFinalReward.GetComponent<SpriteRenderer>();
            deepCaveFinalReward.SetActive(true);
            rope.SetActive(true);
            rope.transform.localPosition = new Vector2(rope.transform.localPosition.x, 1000f);
            aboveReward = new Vector2(deepCaveFinalReward.transform.position.x, deepCaveFinalReward.transform.position.y + 1.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (exitSequence) DoExitSequence();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            playerRigidbody = player.GetComponent<Rigidbody2D>();

            if (GameManager.questionsAnswered + 50 >= GameManager.chestsInLevel)
            {
                GameManager.PauseWorld(true);
                if (GameManager.currentFloor == 4) Debug.Log("Oot kaikista paras ihminen ikinä");
                
                if (GameManager.currentFloor == 4)
                {
                    exitSequence = true;
                    GameManager.PlayerClipping(false);
                    return;
                }

                if (GameManager.currentFloor != 4)
                {
                    Debug.Log("Seuraavaan kenttään...");
                    GameManager.OpenLevelTransitionMenu();
                }
                
            }
        }
    }

    float sequence = 0f;
    int sequenceOrder = 0;
    private void DoExitSequence()
    {
        sequence += Time.deltaTime;
        if (sequence < 3f)
        {
            rope.transform.localPosition = new Vector2(rope.transform.localPosition.x, 60f - sequence * 20 + 47.95f);
        }
        if (sequence < 3.1f && sequence > 3f)
        {
            rope.transform.localPosition = new Vector2(rope.transform.localPosition.x, 47.95f);
        }
        if (sequence > 3.1f && sequenceOrder == 1)
        {
            sequenceOrder = 2;
            rope.transform.parent = transform;
            player.transform.parent = transform;
            deepCaveFinalReward.transform.parent = transform;
            GameManager.cameraMode = 3;
        }
        if (sequence > 4f && sequenceOrder == 2)
        {
            sequenceOrder = 3;
            GameManager.ChangeLightSize(50, 0, 10);
        }
        if (sequence > 3.1f && sequenceOrder > 1)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.1f);
        }
        if (sequence < 10f)
        {
            playerRigidbody.rotation = Mathf.MoveTowards(playerRigidbody.rotation, 0f, 600 * Time.deltaTime);
            playerRigidbody.MovePosition(Vector2.MoveTowards(player.transform.position, aboveReward, 2 * Time.deltaTime));
        }
        if (Vector2.Distance(player.transform.position, aboveReward) < 0.1f && sequenceOrder == 0)
        {
            sequenceOrder = 1;
            aboveReward = new Vector2(deepCaveFinalReward.transform.position.x, deepCaveFinalReward.transform.position.y + 1f);
            deepCaveFinalRewardSprite.sortingOrder = 5;
        }
    }
}
