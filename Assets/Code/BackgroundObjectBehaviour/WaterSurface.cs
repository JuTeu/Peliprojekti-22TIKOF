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
        anim.Play("Splash");
        splash.Play();
    }

    public void PlayIdle()
    {
        anim.Play("Idle");
    }
}
