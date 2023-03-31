using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpener : MonoBehaviour
{
    public GameObject Chest,ChestAnimated,Paper;
    private bool opened = false;
    //public GameObject QuestionBG;


    // Start is called before the first frame update
    void Start()
    {
        ChestAnimated.SetActive(false);
        Paper.SetActive(false);
        //QuestionBG.SetActive(false);     Tähän on parempi käyttää erillistä sceneä ja scenen käyttäminen toimii paremmin prefabeilla.
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!opened)
        {
            opened = true;
            Destroy(Chest);
            ChestAnimated.SetActive(true);
            Paper.SetActive(true);
            //QuestionBG.SetActive(true);
            
            GameManager.OpenQuestionMenu();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //QuestionBG.SetActive(false);
    }
}
