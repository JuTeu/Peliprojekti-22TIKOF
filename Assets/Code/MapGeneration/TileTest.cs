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
    [SerializeField] private Tilemap map;
    [SerializeField] private Vector2 startRoomPosition;
    Tilemap rooms;

    // Start is called before the first frame update
    void Start()
    {
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
        GameObject toimi = GameObject.Find("Rooms");
        rooms = toimi.GetComponent<Tilemap>();
        MapGenerator mapGenerator = new MapGenerator();
        mapGenerator.GenerateMap(mapWidth, mapHeight, roomCount, minBottomWidth, maxBottomWidth);
        int[,] generatedMap = mapGenerator.GetMap();
        
        for (int i = 0; i < generatedMap.GetLength(0); i++)
        {
            for (int j = 0; j < generatedMap.GetLength(1); j++)
            {
                PlaceRoom(j, i * -1, generatedMap[i, j]);
            }
        }
        SceneManager.UnloadSceneAsync(roomsScene);
        map.RefreshAllTiles();
    }
    void PlaceRoom(int x, int y, int tile)
    {
        BoundsInt roomPosition = new BoundsInt(new Vector3Int(12 * x + ((int) startRoomPosition.x / 2), 12 * y + ((int) startRoomPosition.y), 0), size: new Vector3Int(12, 12, 1));
        BoundsInt roomSelection = new BoundsInt(new Vector3Int(0, 12 * tile + tile, 0), size: new Vector3Int(12, 12, 1));
        TileBase[] room = rooms.GetTilesBlock(roomSelection);
        map.SetTilesBlock(roomPosition, room);
    }
}
