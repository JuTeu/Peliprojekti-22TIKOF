using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static Bounds levelBounds;
    public static bool roomsSceneNoLongerLoaded = true;
    public static bool playerInControl = true;
    public static int chestsInLevel = 0;
    public static int questionsAnswered = 0;
    public static int correctAnswers = 0;
    public static bool newQuestion;

    void Awake()
    {
        Instance = this;
    }

    public static void OpenQuestionMenu()
    {
        playerInControl = false;
        newQuestion = true;
        SceneManager.LoadSceneAsync("QuestionMenu", LoadSceneMode.Additive);
    }
    public static void CloseQuestionMenu()
    {
        playerInControl = true;
        SceneManager.UnloadSceneAsync("QuestionMenu");
    }
}
