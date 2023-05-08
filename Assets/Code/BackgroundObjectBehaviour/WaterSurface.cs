using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSurface : MonoBehaviour
{
    public AudioSource splash;
    // Start is called before the first frame update
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.tag == "Player" && GameManager.currentFloor == 0)
        {
            anim.Play("Splash");
            splash.Play();
            GameObject.Find("LevelTransitionBackground").GetComponent<LevelTransitionBackground>().ChangeSong(0);
            GameObject.Find("LevelTransitionBackground").GetComponent<Animator>().Play("fadeIn");
            GameObject.Find("Bear").GetComponent<AudioSource>().Stop();
            GameObject.Find("Hat").GetComponent<CompassHat>().SetArrows();
        }
    }

    public void PlayIdle()
    {
        anim.Play("Idle");
    }
}
