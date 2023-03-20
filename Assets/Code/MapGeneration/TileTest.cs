using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
//using MapGenerator;

public class TileTest : MonoBehaviour
{
    [SerializeField] private Tilemap rooms;
    [SerializeField] private Tilemap map;
    [SerializeField] private Vector2 startRoomPosition;
    Tilemap roomssssssssss;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BuildMap("SampleRooms"));
    }
    IEnumerator BuildMap(string rooms)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(rooms, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        GameObject toimi = GameObject.Find("Rooms");
        roomssssssssss = toimi.GetComponent<Tilemap>();
        int mapWidth = 9;
        int mapHeight = 5;
        int roomCount = 16;
        int minBottomWidth = 1;
        int maxBottomWidth = 2;
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
        SceneManager.UnloadSceneAsync(rooms);
        int huone = Random.Range(0, 15);
        map.RefreshAllTiles();
    }
    void PlaceRoom(int x, int y, int tile)
    {
        BoundsInt roomPosition = new BoundsInt(new Vector3Int(12 * x + ((int) startRoomPosition.x / 2), 12 * y + ((int) startRoomPosition.y), 0), size: new Vector3Int(12, 12, 1));
        BoundsInt roomSelection = new BoundsInt(new Vector3Int(0, 12 * tile + tile, 0), size: new Vector3Int(12, 12, 1));
        TileBase[] room = roomssssssssss.GetTilesBlock(roomSelection);
        map.SetTilesBlock(roomPosition, room);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
