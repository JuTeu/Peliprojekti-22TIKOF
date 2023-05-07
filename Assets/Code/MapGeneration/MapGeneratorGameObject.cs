using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class MapGeneratorGameObject : MonoBehaviour
{
    const int ice = 0;
    const int kelp = 1;
    const int seabed = 2;
    const int shallow = 3;
    const int deep = 4;
    const int abyss = 5;
    int mapHeight;
    List<int>[] regularRooms = new List<int>[47];
    List<int>[] enemyRooms = new List<int>[47];
    List<int>[] chestRooms = new List<int>[47];
    List<int>[] startRooms = new List<int>[47];
    List<int>[] endRooms = new List<int>[47];
    List<int>[] refrigeratorRooms = new List<int>[47];
    List<int[]> deadEndPositions = new List<int[]>();
    int startRoomXPosition, endRoomXPosition;
    [SerializeField] private Tilemap map, mapBackground;
    [SerializeField] private Vector2 startRoomPosition;
    [SerializeField] private Tile levelExit;
    [SerializeField] private GameObject seabedObjects, seabedMap, seabedMapBackground;

    Tilemap rooms, roomsBackground;
    Camera mainCamera;
    int chestRoomTotal = 0;

    void Start()
    {
        mainCamera = Camera.main;
        //GenerateMap(-1);
    }
    public void GenerateMap(int mapNum)
    {
        StartCoroutine(BuildMap(mapNum));
    }
    IEnumerator BuildMap(int mapNum)
    {
        map.gameObject.GetComponent<TilemapRenderer>().sortingOrder = mapNum == 4 ? 2 : 8;
        GameManager.currentFloor = mapNum;
        GameManager.questionsAnswered = 0;
        GameManager.unhurt = true;
        GameObject.Find("PaperCount").GetComponent<UIPaperCount>().SetCollectedPapers(0);

        GameObject[] spawnedObjects = GameObject.FindGameObjectsWithTag("SpawnedObject");
        foreach(GameObject spawnedObject in spawnedObjects)
        {
            GameObject.Destroy(spawnedObject);
        }

        chestRoomTotal = 0;
        string roomsScene = "SampleRooms";
        int mapWidth = 9;
        mapHeight = 5;
        int roomCount = 16;
        int minBottomWidth = 1;
        int maxBottomWidth = 2;
        int minDeadEnds = 4;
        int refrigerators = 2;
        int enemies = 4;
        if (mapNum == ice)
        {
            roomsScene = "IceRooms";
            mapWidth = 9;
            mapHeight = 5;
            roomCount = 16;
            minBottomWidth = 1;
            maxBottomWidth = 2;
            minDeadEnds = 4;

        }
        else if (mapNum == kelp)
        {
            roomsScene = "KelpRooms";
            mapWidth = 13;
            mapHeight = 5;
            roomCount = 20;
            minBottomWidth = 1;
            maxBottomWidth = 4;
            minDeadEnds = 5;
            refrigerators = 3;
            enemies = 3;
        }
        else if (mapNum == seabed)
        {
            roomsScene = "SeabedRooms";
            mapWidth = 9;
            mapHeight = 2;
            roomCount = 7;
            minBottomWidth = 1;
            maxBottomWidth = 5;
            minDeadEnds = 2;
        }
        else if (mapNum == shallow)
        {
            roomsScene = "ShallowRooms";
            mapWidth = 9;
            mapHeight = 5;
            roomCount = 16;
            minBottomWidth = 1;
            maxBottomWidth = 2;
            minDeadEnds = 4;
        }
        else if (mapNum == deep)
        {
            roomsScene = "DeepRooms";
            mapWidth = 9;
            mapHeight = 8;
            roomCount = 23;
            minBottomWidth = 1;
            maxBottomWidth = 9;
            minDeadEnds = 7;
            if (SceneManager.GetSceneByName("LevelTransitionMenu").isLoaded) GameManager.CloseLevelTransitionMenu();
            //GameManager.ChangeLightSize(7, 7, 10);
        }
        else if (mapNum == abyss)
        {
            //todo?
        }
        map.ClearAllTiles();
        mapBackground.ClearAllTiles();
        GameManager.roomsSceneNoLongerLoaded = false;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(roomsScene, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        if (mapNum != seabed)
        {
            seabedMap.SetActive(false);
            seabedMapBackground.SetActive(false);
            seabedObjects.SetActive(false);
            rooms = GameObject.Find("Rooms").GetComponent<Tilemap>();
            roomsBackground = GameObject.Find("RoomsBackground").GetComponent<Tilemap>();
            for (int i = 0; i < 47; i++)
            {
                int nextRoom = 0;
                string roomType;
                regularRooms[i] = new List<int>();
                enemyRooms[i] = new List<int>();
                chestRooms[i] = new List<int>();
                startRooms[i] = new List<int>();
                endRooms[i] = new List<int>();
                refrigeratorRooms[i] = new List<int>();
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
                        case "start":
                            startRooms[i].Add(nextRoom);
                            break;
                        case "end":
                            endRooms[i].Add(nextRoom);
                            break;
                        case "refrigerator":
                            refrigeratorRooms[i].Add(nextRoom);
                            break;
                    }
                    nextRoom++;
                }
            }
            MapGenerator mapGenerator = new MapGenerator();
            mapGenerator.GenerateMap(mapWidth, mapHeight, roomCount, minBottomWidth, maxBottomWidth, minDeadEnds);
            int[,] generatedMap = mapGenerator.GetMap();
            deadEndPositions = mapGenerator.GetDeadEndPositions();
            startRoomXPosition = mapGenerator.GetStartRoomXPosition();
            endRoomXPosition = mapGenerator.GetEndRoomXPosition();
            for (int i = 0; i < generatedMap.GetLength(0); i++)
            {
                for (int j = 0; j < generatedMap.GetLength(1); j++)
                {
                    PlaceRoom(j, i * -1, generatedMap[i, j], 0);
                }
            }

            int refrigeratorRoomsPlaced = 0;
            int[,] refrigeratorRoomPositions = new int[refrigerators, 2];
            for (int i = 0; i < 1000; i++)
            {
                if (refrigeratorRoomsPlaced >= refrigerators) break;
                int randomX = Random.Range(0, generatedMap.GetLength(1));
                int randomY = Random.Range(0, generatedMap.GetLength(0));
                bool validPosition = true;
                for (int j = 0; j < refrigeratorRoomsPlaced; j++)
                {
                    if (randomX == refrigeratorRoomPositions[j, 0] && randomY == refrigeratorRoomPositions[j, 1]) validPosition = false;
                }
                if (validPosition)
                {
                    if (PlaceRoom(randomX, randomY * -1, generatedMap[randomY, randomX], 2))
                    {
                        refrigeratorRoomPositions[refrigeratorRoomsPlaced, 0] = randomX;
                        refrigeratorRoomPositions[refrigeratorRoomsPlaced, 1] = randomY;
                        refrigeratorRoomsPlaced++;
                    }
                }
                if (i == 999) Debug.LogError("Kaikkia pakastinhuoneita ei pystytty laittamaan...");
            }

            int enemyRoomsPlaced = 0;
            int[,] enemyRoomPositions = new int[enemies, 2];
            for (int i = 0; i < 1000; i++)
            {
                if (enemyRoomsPlaced >= enemies) break;
                int randomX = Random.Range(0, generatedMap.GetLength(1));
                int randomY = Random.Range(0, generatedMap.GetLength(0));
                bool validPosition = true;
                for (int j = 0; j < enemyRoomsPlaced; j++)
                {
                    if (randomX == enemyRoomPositions[j, 0] && randomY == enemyRoomPositions[j, 1]) validPosition = false;
                }
                for (int j = 0; j < refrigeratorRoomsPlaced; j++)
                {
                    if (randomX == refrigeratorRoomPositions[j, 0] && randomY == refrigeratorRoomPositions[j, 1]) validPosition = false;
                }
                if (validPosition)
                {
                    if (PlaceRoom(randomX, randomY * -1, generatedMap[randomY, randomX], 1))
                    {
                        enemyRoomPositions[enemyRoomsPlaced, 0] = randomX;
                        enemyRoomPositions[enemyRoomsPlaced, 1] = randomY;
                        enemyRoomsPlaced++;
                    }
                }
                if (i == 999) Debug.LogError("Kaikkia vihollishuoneita ei pystytty laittamaan...");
            }

            map.SetTile(new Vector3Int(12 * mapGenerator.GetEndRoomXPosition() + ((int) startRoomPosition.x / 2) + 5, -12 * (generatedMap.GetLength(0) - 1) + ((int) startRoomPosition.y) + 5, 1), levelExit);
            
        }
        else if (mapNum == seabed)
        {
            seabedMap.SetActive(true);
            seabedMapBackground.SetActive(true);
            seabedObjects.SetActive(true);
            GameManager.RefreshSeabedObjects();
            chestRoomTotal = 6;
        }
        
        
        
        asyncLoad = SceneManager.UnloadSceneAsync(roomsScene);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        GameManager.roomsSceneNoLongerLoaded = true;
        map.RefreshAllTiles();
        map.CompressBounds();

        List<Vector3Int> spawnerTiles = GameManager.spawnerTiles;
        for (int i = 0; i < GameManager.spawnerTiles.Count; i++)
        {
            map.SetTile(GameManager.spawnerTiles[i], null);
        }
        GameManager.spawnerTiles.Clear();

        GameManager.chestsInLevel = chestRoomTotal;
        GameManager.correctAnswers = 0;
        GameObject.Find("PaperCount").GetComponent<UIPaperCount>().SetTotalPapers(chestRoomTotal);
        if (mapNum != seabed)
        {
            GameManager.levelBounds = map.localBounds;
        }
        else if (mapNum == seabed)
        {
            GameManager.levelBounds = seabedMap.GetComponent<Tilemap>().localBounds;
        }

        float cameraHeight = GameManager.cameraPlaySize;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        float cameraMinX = GameManager.levelBounds.min.x + cameraWidth;
        float cameraMaxX = GameManager.levelBounds.max.x - cameraWidth;
        float cameraMinY = GameManager.levelBounds.min.y + cameraHeight;
        // Tää ei toimi niinkuin sen pitäisi jos tän yrittää tehdä kunnolla. Kentän yläraja pysyy aina samana niin ihan sama, kovakoodaan sen.
        float cameraMaxY = -18 - cameraHeight;
        if (mapNum == 0) cameraMaxY = 20 - cameraHeight;
        CameraMovement cameraObject = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
        cameraObject.cameraBounds = new Bounds();
        cameraObject.cameraBounds.SetMinMax(new Vector3(cameraMinX, cameraMinY, -10), new Vector3(cameraMaxX, cameraMaxY, -10));

        GameManager.levelIsGenerated = true;
        if (mapNum == deep) GameObject.FindWithTag("Player").transform.position = new Vector2(0, -20);
        Invoke("QuickFix", 6.5f);
    }
    void QuickFix()
    {
        if (!GameManager.playerOnScreen)
        {
            if (GameManager.currentFloor != 1) GameObject.FindWithTag("Player").transform.position = new Vector2(0, -20);
        }
        if (SceneManager.GetSceneByName("LevelTransitionMenu").isLoaded) GameManager.CloseLevelTransitionMenu();
    }
    bool PlaceRoom(int x, int y, int tile, int mode)
    {
        bool successfullyPlaced = true;
        BoundsInt roomPosition = new BoundsInt(new Vector3Int(12 * x + ((int) startRoomPosition.x / 2), 12 * y + ((int) startRoomPosition.y), 0), size: new Vector3Int(12, 12, 1));
        //BoundsInt roomSelection = new BoundsInt(new Vector3Int(0, 12 * tile + tile, 0), size: new Vector3Int(12, 12, 1));
        BoundsInt roomSelection;
        int randomNumber;
        //0 = regular, 1 = chest, 2 = start, 3 = end, 4 = enemy, 5 = refrigerator
        int roomType = 0;
        foreach (int[] deadEnd in deadEndPositions)
        {
            if (deadEnd[1] == x && deadEnd[0] == y * -1)
            {
                roomType = 1;
                break;
            }
        }

        if (x == startRoomXPosition && y == 0) roomType = 2;
        if (x == endRoomXPosition && y == -mapHeight + 1) roomType = 3;
        if (mode > 0 && roomType > 0) return false;
        if (mode == 1) roomType = 4;
        if (mode == 2) roomType = 5;

        if (roomType == 1)
        {
            //roomSelection = new BoundsInt(new Vector3Int(0, 0, 0), size: new Vector3Int(12, 12, 1));
            randomNumber = Random.Range(0, chestRooms[tile].Count);
            roomSelection = new BoundsInt(new Vector3Int(12 * chestRooms[tile][randomNumber] + chestRooms[tile][randomNumber], 12 * tile + tile, 0), size: new Vector3Int(12, 12, 1));
            chestRoomTotal++;
        }
        else if (roomType == 2)
        {
            randomNumber = Random.Range(0, startRooms[tile].Count);
            roomSelection = new BoundsInt(new Vector3Int(12 * startRooms[tile][randomNumber] + startRooms[tile][randomNumber], 12 * tile + tile, 0), size: new Vector3Int(12, 12, 1));
        }
        else if (roomType == 3)
        {
            randomNumber = Random.Range(0, endRooms[tile].Count);
            roomSelection = new BoundsInt(new Vector3Int(12 * endRooms[tile][randomNumber] + endRooms[tile][randomNumber], 12 * tile + tile, 0), size: new Vector3Int(12, 12, 1));
        }
        else if (roomType == 4)
        {
            if (enemyRooms[tile].Count == 0) return false;
            randomNumber = Random.Range(0, enemyRooms[tile].Count);
            roomSelection = new BoundsInt(new Vector3Int(12 * enemyRooms[tile][randomNumber] + enemyRooms[tile][randomNumber], 12 * tile + tile, 0), size: new Vector3Int(12, 12, 1));
        }
        else if (roomType == 5)
        {
            if (refrigeratorRooms[tile].Count == 0) return false;
            randomNumber = Random.Range(0, refrigeratorRooms[tile].Count);
            roomSelection = new BoundsInt(new Vector3Int(12 * refrigeratorRooms[tile][randomNumber] + refrigeratorRooms[tile][randomNumber], 12 * tile + tile, 0), size: new Vector3Int(12, 12, 1));
        }
        else
        {
            randomNumber = Random.Range(0, regularRooms[tile].Count);
            roomSelection = new BoundsInt(new Vector3Int(12 * regularRooms[tile][randomNumber] + regularRooms[tile][randomNumber], 12 * tile + tile, 0), size: new Vector3Int(12, 12, 1));
        }
        TileBase[] room = rooms.GetTilesBlock(roomSelection);
        TileBase[] roomBackground = roomsBackground.GetTilesBlock(roomSelection);
        map.SetTilesBlock(roomPosition, room);
        mapBackground.SetTilesBlock(roomPosition, roomBackground);
        return successfullyPlaced;
    }
}
