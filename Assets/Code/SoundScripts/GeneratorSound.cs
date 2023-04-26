using UnityEngine;

public class GeneratorSound : MonoBehaviour
{
    public AudioClip generatorSound;
    private AudioSource audioSource;

    // the maximum distance at which the sound will be heard at full volume
    public float maxDistance = 10f;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = generatorSound;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = 1f;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // calculate the distance between the player and the generator
            float distance = Vector3.Distance(other.transform.position, transform.position);

            // adjust the volume based on the distance
            float volume = Mathf.Clamp01(1f - (distance / maxDistance));
            audioSource.volume = volume;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Stop();
        }
    }
}




