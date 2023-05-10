using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FlipTutorial : MonoBehaviour
{
    int page = 0;
    float delay = -1.2f;
    public TextMeshProUGUI tutorialText;

    private void Start()
    {
        gameObject.SetActive(true);
        GameManager.PauseWorld(true);
    }

    private void FixedUpdate()
    {
        delay += Time.deltaTime;
    }
    public void PressScreen()
    {
        if (delay < 0f) return;
        if(page == 0)
        {
            tutorialText.text = "Voit uida kuplien läpi aktivoimalla räpylät";
            GameManager.cameraMode = 131;
            delay = -0.5f;
    
        }
        else if (page == 1)
        {
            GameManager.cameraMode = 127;
            GameManager.PauseWorld(false);
            SceneManager.UnloadSceneAsync("TutorialScene");
        }
        page++;
    }
}
