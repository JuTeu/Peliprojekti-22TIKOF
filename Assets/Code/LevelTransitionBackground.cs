using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitionBackground : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer background;
    [SerializeField] Sprite ice, kelp, seabed, shallow, deep;
    void Start()
    {
        background = GetComponent<SpriteRenderer>();
        background.enabled = false;
    }

    public void EnableBackground(bool toggle)
    {
        background.enabled = toggle;
    }

    public void ChangeBackground(int type)
    {
        switch (type)
        {
            case 0:
                background.sprite = ice;
                break;
            case 1:
                background.sprite = kelp;
                break;
            case 2:
                background.sprite = seabed;
                break;
            case 3:
                background.sprite = shallow;
                break;
            case 4:
                background.sprite = deep;
                break;
        }
    }
}
