using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuBehaviour : MonoBehaviour
{
    private float sequence = 0f;
    private float exponentialSequence = 0f;
    private bool sequenceStopped = false;
    private bool exiting = false;
    private RectTransform rt;
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
