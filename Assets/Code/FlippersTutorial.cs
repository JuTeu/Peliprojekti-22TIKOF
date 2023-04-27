using UnityEngine;
using UnityEngine.SceneManagement;

public class FlippersTutorial : MonoBehaviour
{
    private bool isInTutorial = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isInTutorial)
        {
            isInTutorial = true;

            // Pause the game
            Time.timeScale = 0f;

            // Load the TutorialScene
            SceneManager.LoadSceneAsync("TutorialScene", LoadSceneMode.Additive);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isInTutorial)
        {
            isInTutorial = false;

            // Unload the TutorialScene
            SceneManager.UnloadSceneAsync("TutorialScene");
        }
    }

    private void Update()
    {
        // Check if the game is currently paused
        if (Time.timeScale == 0f)
        {
            // Check if the user has clicked on the screen
            if (Input.GetMouseButtonDown(0))
            {
                // Resume the game
                Time.timeScale = 1f;
            }
        }
    }
}


