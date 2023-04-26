using UnityEngine;

public class FreezerSound : MonoBehaviour
{
    public AudioClip freezerSound;
    private AudioSource audioSource;
    private CircleCollider2D circleCollider;

    void Start()
    {
        Debug.Log("FreezerSound script has started");

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = freezerSound;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = 1f;

        // Get the circle collider component
        circleCollider = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider that triggered the event is the circle collider
        if (other == circleCollider)
        {
            audioSource.Play();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the collider that triggered the event is the circle collider
        if (other == circleCollider)
        {
            audioSource.Stop();
        }
    }
}


