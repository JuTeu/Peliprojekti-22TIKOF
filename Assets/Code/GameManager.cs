using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static Bounds levelBounds;
    public static bool roomsSceneNoLongerLoaded = true;
    public static bool playerInControl = true;

    void Awake()
    {
        Instance = this;
    }
}
