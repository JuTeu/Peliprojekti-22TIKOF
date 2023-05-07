using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenuBehaviour : MonoBehaviour
{
    private float sequence = 0f;
    private float exponentialSequence = 0f;
    private bool sequenceStopped, exiting = false;
    private RectTransform rt;
    private UISounds sound;
    
    private bool clipping = GameManager.playerClipping;
    private bool invlunerablity = GameManager.playerIsInvulnerable;
    private int levelNum = -1;
    [SerializeField] private TextMeshProUGUI devLevelText, devClipText, devInvulnerabilityText, score;

    void Start()
    {
        score.text = GameManager.score + "";
        rt = GetComponent<RectTransform>();
        sound = GameObject.Find("UISounds").GetComponent<UISounds>();
        rt.anchoredPosition = new Vector2(-1000, 0);
        if (clipping == false)
        {
            devClipText.text = "Noclip = TRUE";
        }
        if (invlunerablity)
        {
            devInvulnerabilityText.text = "Haavoittumaton = TRUE";
        }
    }
    
    public void Close()
    {
        sound.PlaySound("Click");
        sequenceStopped = false;
        exiting = true;
        sequence = 0f;
    }

    public void RegenMap()
    {
        Debug.Log("Generoidaan karttaa...");
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

    public void ToggleClipping()
    {
        if (clipping)
        {
            devClipText.text = "Noclip = TRUE";
            clipping = false;
            GameManager.PlayerClipping(false);
        }
        else
        {
            devClipText.text = "Noclip = FALSE";
            clipping = true;
            GameManager.PlayerClipping(true);
        }
    }

    public void ReturnToSurface()
    {
        sound.PlaySound("Click");
        GameManager.ReturnToSurfaceButton();
    }

    public void ToggleInvulnerability()
    {
        if (invlunerablity)
        {
            devInvulnerabilityText.text = "Haavoittumaton = FALSE";
            invlunerablity = false;
            GameManager.playerIsInvulnerable = false;
        }
        else
        {
            devInvulnerabilityText.text = "Haavoittumaton = TRUE";
            invlunerablity = true;
            GameManager.playerIsInvulnerable = true;
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
                sound.PlaySound("Swipe");
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
                sound.PlaySound("Swipe");
                GameManager.ClosePauseMenu();
            }
        }
    }
}
