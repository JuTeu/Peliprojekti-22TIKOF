using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static Bounds levelBounds;

    void Awake()
    {
        Instance = this;
    }
}
