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
    public static bool playerIsInvulnerable = false;
    public static bool playerClipping = true;
    public static bool enemiesPaused = false;
    public static int chestsInLevel = 0;
    public static int questionsAnswered, questionsAnsweredTotal = 0;
    public static int correctAnswers = 0;
    public static int currentFloor = 0;
    public static int cameraMode = 0;
    public static bool newQuestion;
    public static List<Vector3Int> spawnerTiles = new List<Vector3Int>();

    void Awake()
    {
        Instance = this;
    }

    public static void OpenQuestionMenu()
    {
        PauseWorld(true);
        SceneManager.LoadSceneAsync("QuestionMenu", LoadSceneMode.Additive);
    }

    public static void PauseWorld(bool toggle)
    {
        GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        playerInControl = !toggle;
        playerIsInvulnerable = toggle;
        enemiesPaused = toggle;
        EnablePauseButton(!toggle);
    }

    public static void CloseQuestionMenu()
    {
        PauseWorld(false);
        SceneManager.UnloadSceneAsync("QuestionMenu");
    }

    public void OpenPauseMenu()
    {
        PauseWorld(true);
        SceneManager.LoadSceneAsync("PauseMenu", LoadSceneMode.Additive);
    }

    public static void ClosePauseMenu()
    {
        PauseWorld(false);
        SceneManager.UnloadSceneAsync("PauseMenu");
    }

    public static void GenerateMap(int mapNum)
    {
        GameObject.Find("MapGenerator").GetComponent<MapGeneratorGameObject>().GenerateMap(mapNum);
        GameObject.Find("Background").GetComponent<BackgroundManager>().ChangeBackground(mapNum);

        /*if (mapNum == -1)
        GameObject.Find("Background").GetComponent<BackgroundManager>().ChangeBackground(0);
        else
        GameObject.Find("Background").GetComponent<BackgroundManager>().ChangeBackground(4);*/
    }

    public static void EnablePauseButton(bool toggle)
    {
        //GameObject.Find("PauseButton").GetComponent<EnableDisableChild>().Enable(toggle);
        GameObject.Find("PauseButton").GetComponent<Button>().interactable = toggle;
    }

    public static void PlayerClipping(bool toggle)
    {
        GameObject.FindWithTag("Player").GetComponent<Collider2D>().isTrigger = !toggle;
        playerClipping = toggle;
    }
}
