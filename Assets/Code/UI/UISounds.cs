using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISounds : MonoBehaviour
{
    AudioSource source;
    [SerializeField] AudioClip swipe, click;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(string type)
    {
        if (type == "Swipe") source.clip = swipe;
        else if (type == "Click") source.clip = click;
        source.Play();
    }
}
