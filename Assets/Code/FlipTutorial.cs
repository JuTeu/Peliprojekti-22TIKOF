using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FlipTutorial : MonoBehaviour
{
    int page = 0;
    public TextMeshProUGUI tutorialText;

    private void Start()
    {
        gameObject.SetActive(true);
        GameManager.PauseWorld(true);
    }
    public void PressScreen()
    {
        if(page == 0)
        {
            tutorialText.text = "Voit uida kuplien läpi aktivoimalla räpylät";
            GameManager.cameraMode = 131;
        
    
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
