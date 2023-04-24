using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitionMenuBehaviour : MonoBehaviour
{
    private float sequence = 0f;
    bool playerIsInTheTransition = false;
    RectTransform mainTransform;

    void Start()
    {
        mainTransform = GetComponent<RectTransform>();
        mainTransform.anchoredPosition = new Vector2(0, 1000);
        GameManager.ChangeLightSize(50, 0, 10);
    }

    // Update is called once per frame
    void Update()
    {
        sequence += Time.deltaTime;

        if (!playerIsInTheTransition && sequence > 2)
        {
            playerIsInTheTransition = true;
            GameObject.FindWithTag("Player").transform.position = new Vector2(20f + (GameManager.currentFloor * 50), 330f);
            GameManager.cameraMode = 1;
            Camera.main.gameObject.GetComponent<Camera>().orthographicSize = 7.5f;
            GameManager.ChangeLightSize(0, 50, 10);
        }
    }
}
