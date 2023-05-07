using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExitBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject deepCaveFinalReward, rope, bubble, arrow, kelpDoor, dynamite, dynamiteBubble, chain1, chain2, endLock;
    SpriteRenderer deepCaveFinalRewardSprite;
    Animator chain1Animator, chain2Animator, lockAnimator;
    CharacterController characterController;
    Collider2D exitCollider;
    GameObject player;
    Rigidbody2D playerRigidbody;
    PlayerAnimator playerAnimator;
    Vector2 aboveReward;
    bool levelFinished = false, boostSkipable = false, exitSequence = false;
    Animator musicFade;
    void Start()
    {
        musicFade = GameObject.Find("LevelTransitionBackground").GetComponent<Animator>();
        exitCollider = GetComponent<Collider2D>();
        if (GameManager.currentFloor == 0)
        {
            bubble.SetActive(true);
            characterController = GameObject.FindWithTag("Player").GetComponent<CharacterController>();
            boostSkipable = true;
        }
        else if (GameManager.currentFloor == 1)
        {
            kelpDoor.SetActive(true);
        }
        else if (GameManager.currentFloor == 3)
        {
            dynamite.SetActive(true);
        }
        if (GameManager.currentFloor == 4) 
        {
            BoxCollider2D rewardCollider = GetComponent<BoxCollider2D>();
            rewardCollider.size = new Vector2(2f, 2f);
            rewardCollider.offset = new Vector2(0f, -2.5f);
            deepCaveFinalRewardSprite = deepCaveFinalReward.GetComponent<SpriteRenderer>();
            deepCaveFinalReward.SetActive(true);
            chain1.SetActive(true);
            chain2.SetActive(true);
            endLock.SetActive(true);
            chain1Animator = chain1.GetComponent<Animator>();
            chain2Animator = chain2.GetComponent<Animator>();
            lockAnimator = endLock.GetComponent<Animator>();
            rope.SetActive(true);
            rope.transform.localPosition = new Vector2(rope.transform.localPosition.x, 1000f);
            aboveReward = new Vector2(deepCaveFinalReward.transform.position.x, deepCaveFinalReward.transform.position.y + 1.5f);
        }
    }
    public void Open()
    {
        levelFinished = true;
        exitCollider.isTrigger = true;
        if (GameManager.currentFloor != 4) arrow.SetActive(true);
        if (GameManager.currentFloor == 0)
        {
            bubble.GetComponent<ParticleSystem>().Stop();
            bubble.GetComponent<Collider2D>().enabled = false;
        }
        else if (GameManager.currentFloor == 1)
        {
            kelpDoor.GetComponent<Animator>().Play("Open");
        }
        else if (GameManager.currentFloor == 3)
        {
            dynamite.GetComponent<Animator>().Play("Explode");
        }
        else if (GameManager.currentFloor == 4)
        {
            chain1Animator.Play("Open");
            chain2Animator.Play("Open");
            lockAnimator.Play("Open");
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (exitSequence) DoExitSequence();
        if (boostSkipable)
        {
            if (!levelFinished)
            {
                exitCollider.isTrigger = characterController.boostMode;
            }
            else
            {
                exitCollider.isTrigger = true;
                boostSkipable = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            musicFade.Play("fadeOut");
            GameObject.Find("Hat").GetComponent<CompassHat>().HideAllArrows();
            player = collision.gameObject;
            playerRigidbody = player.GetComponent<Rigidbody2D>();
            playerAnimator = player.GetComponent<PlayerAnimator>();
            if (GameManager.questionsAnswered + 50 >= GameManager.chestsInLevel)
            {
                GameManager.PauseWorld(true);
                if (GameManager.currentFloor == 4) Debug.Log("Oot kaikista paras ihminen ikinä");
                
                if (GameManager.currentFloor == 0)
                {
                    if (GameManager.questionsAnswered >= GameManager.chestsInLevel)
                    {
                        GameObject.Find("IceFan").GetComponent<Animator>().Play("Broken");
                        GameObject.Find("IceFanBubbles").GetComponent<ParticleSystem>().Stop();
                        GameObject.Find("IceFanBubbles").GetComponent<ParticleSystem>().Clear();
                    }
                    else
                    {
                        GameObject.Find("IceFan").GetComponent<Animator>().Play("Working");
                        GameObject.Find("IceFanBubbles").GetComponent<ParticleSystem>().Simulate(1f);
                        GameObject.Find("IceFanBubbles").GetComponent<ParticleSystem>().Play();
                    }
                }
                else if (GameManager.currentFloor == 4)
                {
                    GameManager.unlocks = GameManager.unlocks | 0b_10;
                    GameManager.Save();
                    exitSequence = true;
                    GameManager.PlayerClipping(false);
                    playerAnimator.Play("MiddleUpSwimIdle");
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
            GameManager.OpenLevelTransitionMenu();
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
            aboveReward = new Vector2(deepCaveFinalReward.transform.position.x, deepCaveFinalReward.transform.position.y + 1.2f);
            deepCaveFinalRewardSprite.sortingOrder = 5;
        }
    }
}
