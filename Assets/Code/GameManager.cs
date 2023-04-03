using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static Bounds levelBounds;
    public static bool roomsSceneNoLongerLoaded = true;
    public static bool playerInControl = true;
    public static int chestsInLevel = 0;
    public static int questionsAnswered, questionsAnsweredTotal = 0;
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
        EnablePauseButton(false);
        SceneManager.LoadSceneAsync("QuestionMenu", LoadSceneMode.Additive);
    }

    public static void CloseQuestionMenu()
    {
        playerInControl = true;
        EnablePauseButton(true);
        SceneManager.UnloadSceneAsync("QuestionMenu");
    }

    public void OpenPauseMenu()
    {
        playerInControl = false;
        EnablePauseButton(false);
        SceneManager.LoadSceneAsync("PauseMenu", LoadSceneMode.Additive);
    }

    public static void ClosePauseMenu()
    {
        playerInControl = true;
        EnablePauseButton(true);
        SceneManager.UnloadSceneAsync("PauseMenu");
    }

    public static void GenerateMap(int mapNum)
    {
        GameObject.Find("");
    }
    public static void EnablePauseButton(bool toggle)
    {
        //GameObject.Find("PauseButton").GetComponent<EnableDisableChild>().Enable(toggle);
        GameObject.Find("PauseButton").GetComponent<Button>().interactable = toggle;
    }
}
