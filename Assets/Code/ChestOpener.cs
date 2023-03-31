using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpener : MonoBehaviour
{
    public GameObject Chest,ChestAnimated,Paper;
    public GameObject QuestionBG;


    // Start is called before the first frame update
    void Start()
    {
        ChestAnimated.SetActive(false);
        Paper.SetActive(false);
        QuestionBG.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(Chest);
        ChestAnimated.SetActive(true);
        Paper.SetActive(true);
        QuestionBG.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        QuestionBG.SetActive(false);
    }
}
