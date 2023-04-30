using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KelpFruit : MonoBehaviour
{
    [SerializeField] GameObject anim, fruit, stalk;
    SpriteRenderer fruitSprite;
    Collider2D fruitCollider;
    bool hasExploded = false;
    float sequence = 0f;
    float sineSequence = 0f;
    float pulsingSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        sequence = Random.Range(0, 10);
        fruitSprite = fruit.GetComponent<SpriteRenderer>();
        fruitCollider = GetComponent<Collider2D>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !hasExploded)
        {
            pulsingSpeed = 8f - Vector2.Distance(fruitCollider.transform.position, collision.transform.position) * 2;
            if (Vector2.Distance(fruitCollider.transform.position, collision.transform.position) < 1.4f)
            {
                fruit.SetActive(false);
                stalk.SetActive(false);
                anim.SetActive(true);
                anim.GetComponent<Animator>().Play("Explode");
                //collision.gameObject.GetComponent<PlayerHealth>().TakeDamageFromEnemy(gameObject, 30f, 9f, 1.5f);
                hasExploded = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            pulsingSpeed = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hasExploded) return;
        if (sequence > 90f)
        {
            sequence = 0f;
        }
        sequence += pulsingSpeed * Time.deltaTime;
        sineSequence = Mathf.Sin(sequence) / 2 + 0.5f;
        fruitSprite.color = new Color(1f, sineSequence / 2f + 0.75f, 1f);
        fruit.transform.localScale = new Vector3(1f + sineSequence / 4f, 1f + sineSequence / 4f, 1f);
        fruit.transform.localPosition = new Vector2(Mathf.Sin((pulsingSpeed - 1) * 50 * sequence / 2) * pulsingSpeed * 0.01f, Mathf.Sin((pulsingSpeed - 1) * 50 * sequence) * pulsingSpeed * 0.01f);
    }
}
