using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class TileTest : MonoBehaviour
{
    const int ice = 0;
    const int kelp = 1;
    const int seabed = 2;
    const int shallow = 3;
    const int deep = 4;
    const int abyss = 5;
    List<int>[] regularRooms = new List<int>[47];
    List<int>[] enemyRooms = new List<int>[47];
    List<int>[] chestRooms = new List<int>[47];
    List<int[]> deadEndPositions = new List<int[]>();
    [SerializeField] private Tilemap map;
    [SerializeField] private Vector2 startRoomPosition;
    Tilemap rooms;
    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(BuildMap(-1));
    }
    IEnumerator BuildMap(int mapNum)
    {
        string roomsScene = "SampleRooms";
        int mapWidth = 9;
        int mapHeight = 5;
        int roomCount = 16;
        int minBottomWidth = 1;
        int maxBottomWidth = 2;
        if(mapNum == ice)
        {
            roomsScene = "IceRooms";
            mapWidth = 9;
            mapHeight = 5;
            roomCount = 16;
            minBottomWidth = 1;
            maxBottomWidth = 2;
        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(roomsScene, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        rooms = GameObject.Find("Rooms").GetComponent<Tilemap>();
        for (int i = 0; i < 47; i++)
        {
            int nextRoom = 0;
            string roomType;
            regularRooms[i] = new List<int>();
            enemyRooms[i] = new List<int>();
            chestRooms[i] = new List<int>();
            while (rooms.GetTile(new Vector3Int(12 + 13 * nextRoom, 12 * i + i + 11, 0)) != null)
            {
                roomType = rooms.GetTile(new Vector3Int(12 + 13 * nextRoom, 12 * i + i + 11, 0)).name;
                switch (roomType)
                {
                    case "regular":
                        regularRooms[i].Add(nextRoom);
                        break;
                    case "enemy":
                        enemyRooms[i].Add(nextRoom);
                        break;
                    case "chest":
                        chestRooms[i].Add(nextRoom);
                        break;
                }
                nextRoom++;
            }
        }
        MapGenerator mapGenerator = new MapGenerator();
        mapGenerator.GenerateMap(mapWidth, mapHeight, roomCount, minBottomWidth, maxBottomWidth);
        int[,] generatedMap = mapGenerator.GetMap();
        deadEndPositions = mapGenerator.GetDeadEndPositions();
        for (int i = 0; i < generatedMap.GetLength(0); i++)
        {
            for (int j = 0; j < generatedMap.GetLength(1); j++)
            {
                PlaceRoom(j, i * -1, generatedMap[i, j]);
            }
        }
        SceneManager.UnloadSceneAsync(roomsScene);
        map.RefreshAllTiles();
        //map.CompressBounds();
        GameManager.levelBounds = map.localBounds;
        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        float cameraMinX = GameManager.levelBounds.min.x + cameraWidth;
        float cameraMaxX = GameManager.levelBounds.extents.x - cameraWidth;
        float cameraMinY = GameManager.levelBounds.min.y + cameraHeight;
        // Tää ei toimi niinkuin sen pitäisi jos tän yrittää tehdä kunnolla. Kentän yläraja pysyy aina samana niin ihan sama, kovakoodaan sen.
        float cameraMaxY = -18 - cameraHeight;
        CameraMovement cameraObject = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
        cameraObject.cameraBounds = new Bounds();
        cameraObject.cameraBounds.SetMinMax(new Vector3(cameraMinX, cameraMinY, -10), new Vector3(cameraMaxX, cameraMaxY, -10));
    }
    void PlaceRoom(int x, int y, int tile)
    {
        BoundsInt roomPosition = new BoundsInt(new Vector3Int(12 * x + ((int) startRoomPosition.x / 2), 12 * y + ((int) startRoomPosition.y), 0), size: new Vector3Int(12, 12, 1));
        //BoundsInt roomSelection = new BoundsInt(new Vector3Int(0, 12 * tile + tile, 0), size: new Vector3Int(12, 12, 1));
        BoundsInt roomSelection;
        int randomNumber;
        bool isChestRoom = false;
        foreach (int[] deadEnd in deadEndPositions)
        {
            if (deadEnd[1] == x && deadEnd[0] == y * -1)
            {
                isChestRoom = true;
                break;
            }
        }
        if (isChestRoom)
        {
            //roomSelection = new BoundsInt(new Vector3Int(0, 0, 0), size: new Vector3Int(12, 12, 1));
            randomNumber = Random.Range(0, chestRooms[tile].Count);
            roomSelection = new BoundsInt(new Vector3Int(12 * chestRooms[tile][randomNumber] + chestRooms[tile][randomNumber], 12 * tile + tile, 0), size: new Vector3Int(12, 12, 1));
        }
        else
        {
            randomNumber = Random.Range(0, regularRooms[tile].Count);
            roomSelection = new BoundsInt(new Vector3Int(12 * regularRooms[tile][randomNumber] + regularRooms[tile][randomNumber], 12 * tile + tile, 0), size: new Vector3Int(12, 12, 1));
        }
        TileBase[] room = rooms.GetTilesBlock(roomSelection);
        map.SetTilesBlock(roomPosition, room);
    }
}
