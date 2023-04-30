using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static Bounds levelBounds;
    public static bool unhurt;
    public static bool levelIsGenerated = false;
    public static bool roomsSceneNoLongerLoaded = true;
    public static bool playerInControl = true;
    public static bool playerIsInvulnerable = false;
    public static bool playerClipping = true;
    public static bool enemiesPaused = false;
    public static int score;

    // Pysyvät tallennuksesta toiseen
    public static int totalScore;
    public static int highScore;
    public static bool flippersUnlocked = false;
    public static bool flippersEquipped = false;
    public static bool regularEndingReached = false;


    public static int chestsInLevel = 0;
    public static int questionsAnswered, questionsAnsweredTotal = 0;
    public static int correctAnswers, correctAnswersTotal = 0;
    public static int currentFloor = 0;
    public static int cameraMode = 0;
    public static int playMode = 0;
    public static bool newQuestion;
    public static List<Vector3Int> spawnerTiles = new List<Vector3Int>();
    public static string[] lines;

    public const float cameraPlaySize = 14;

    void Awake()
    {
        Instance = this;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        score = 0;
        BeginGame();
    }

    public static void OpenQuestionMenu()
    {
        PauseWorld(true);
        SceneManager.LoadSceneAsync("QuestionMenu", LoadSceneMode.Additive);
    }

    public static void PauseWorld(bool toggle)
    {
        HideStick();
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

    public static void OpenSurfaceMenu()
    {
        SceneManager.LoadSceneAsync("SurfaceMenu", LoadSceneMode.Additive);
    }
    public static void OpenDialogue()
    {
        
        SceneManager.LoadSceneAsync("DialogueMenu", LoadSceneMode.Additive);
    }

    public static void CloseDialogue()
    {
       
        SceneManager.UnloadSceneAsync("DialogueMenu");
    }

    public static void CloseSurfaceMenu()
    {
        SceneManager.UnloadSceneAsync("SurfaceMenu");
    }

    public static void OpenLevelTransitionMenu()
    {
        SceneManager.LoadSceneAsync("LevelTransitionMenu", LoadSceneMode.Additive);
    }

    public static void CloseLevelTransitionMenu()
    {
        SceneManager.UnloadSceneAsync("LevelTransitionMenu");
    }
    
    public static void RefreshSeabedObjects()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("SeabedObject");
        foreach (GameObject objecto in objects)
        {
            objecto.GetComponent<ObjectRefresher>().Refresh();
        }
    }
    public static void HideStick()
    {
        GameObject.Find("JoyStick").GetComponent<RectTransform>().anchoredPosition = new Vector2(10000, 10000);
    }
    public static bool GenerateMapWait(int mapNum)
    {
        GenerateMap(mapNum);
        return true;
    }
    public static void GenerateMap(int mapNum)
    {
        levelIsGenerated = false;
        GameObject.Find("Background").GetComponent<BackgroundManager>().ChangeBackground(mapNum);
        GameObject.Find("MapGenerator").GetComponent<MapGeneratorGameObject>().GenerateMap(mapNum);
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

    public static void ChangeLightSize(float size, float targetSize, float speed)
    {
        GameObject.Find("DarknessAndLight").GetComponent<DarknessBehaviour>().ChangeLight(size, targetSize, speed);
    }

    public static void ReturnToSurfaceButton()
    {
        ClosePauseMenu();
        BeginGame();
    }

    public static void BeginGame()
    {
        GameObject.Find("RegularEndingTrophy").GetComponent<SpriteRenderer>().enabled = regularEndingReached;
        HideStick();
        PauseWorld(true);
        Camera.main.gameObject.GetComponent<Camera>().orthographicSize = 7.5f;
        cameraMode = 1;
        playMode = 1;
        Rigidbody2D playerRigidbody = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        playerRigidbody.gameObject.GetComponent<PlayerHealth>().Heal(100f);
        playerRigidbody.rotation = 0f;
        playerRigidbody.position = new Vector2(-4.5f, -16.656f);
        playerRigidbody.gravityScale = 1f;
        GenerateMap(0);
        ChangeLightSize(0, 50, 10);
        OpenSurfaceMenu();
    }
}
