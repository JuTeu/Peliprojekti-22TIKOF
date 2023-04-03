using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenuBehaviour : MonoBehaviour
{
    private float sequence = 0f;
    private float exponentialSequence = 0f;
    private bool sequenceStopped = false;
    private bool exiting = false;
    private RectTransform rt;

    private int levelNum = -1;
    [SerializeField] private TextMeshProUGUI devLevelText;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(-1000, 0);
    }
    
    public void Close()
    {
        sequenceStopped = false;
        exiting = true;
        sequence = 0f;
    }

    public void RegenMap()
    {
        Debug.Log("Wautsi");
        GameManager.GenerateMap(levelNum);
    }

    public void SceneNumber()
    {
        if (levelNum == -1)
        {
            levelNum = 0;
            devLevelText.text = "ice";
        }
        else if (levelNum == 0)
        {
            levelNum = 1;
            devLevelText.text = "kelp";
        }
        else if (levelNum == 1)
        {
            levelNum = 2;
            devLevelText.text = "seabed";
        }
        else if (levelNum == 2)
        {
            levelNum = 3;
            devLevelText.text = "shallow";
        }
        else if (levelNum == 3)
        {
            levelNum = 4;
            devLevelText.text = "deep";
        }
        else if (levelNum == 4)
        {
            levelNum = 5;
            devLevelText.text = "abyss";
        }
        else
        {
            levelNum = -1;
            devLevelText.text = "sample";
        }
    }
    void FixedUpdate()
    {
        if (!sequenceStopped && !exiting)
        {
            sequence += 50 * Time.deltaTime;
            exponentialSequence = Mathf.Pow(2, sequence);
            rt.anchoredPosition = new Vector2(-1000 + exponentialSequence / 10, 0);

            if (exponentialSequence > 10000f)
            {
                rt.anchoredPosition = new Vector2(0, 0);
                sequenceStopped = true;
            }
        }
        if (!sequenceStopped && exiting)
        {
            sequence += 50 * Time.deltaTime;
            exponentialSequence = Mathf.Pow(2, sequence);
            rt.anchoredPosition = new Vector2(0 + exponentialSequence / 10, 0);

            if (exponentialSequence > 10000f)
            {
                GameManager.ClosePauseMenu();
            }
        }
    }
}
