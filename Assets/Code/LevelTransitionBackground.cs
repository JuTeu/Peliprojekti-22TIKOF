using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitionBackground : MonoBehaviour
{
    public AudioSource bgMusic;
    public AudioClip[] audioClips;
    public Animator musicFade;
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
        if (!toggle)
        {
            bgMusic.Play();
            musicFade.Play("fadeIn");
        }
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
        bgMusic.Stop();
        if (type < 4)
        {
            bgMusic.clip = audioClips[type + 1];
        }
    }
}
