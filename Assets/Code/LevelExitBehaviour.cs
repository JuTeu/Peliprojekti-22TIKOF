using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExitBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (GameManager.questionsAnswered >= GameManager.chestsInLevel)
            {
                if (GameManager.currentFloor == 4) Debug.Log("Oot kaikista paras ihminen ikinä");
                Debug.Log("Seuraavaan kenttään...");
                GameObject.FindWithTag("Player").transform.position = new Vector2(0f, -25f);
                GameManager.GenerateMap(GameManager.currentFloor + 1);
                
            }
        }
    }
}
