using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static MapGenerator;

public class TileTest : MonoBehaviour
{
    [SerializeField] private Tilemap rooms;
    [SerializeField] private Tilemap map;
    [SerializeField] private Vector2 startRoomPosition;
    // Start is called before the first frame update
    void Start()
    {
        int mapWidth = 9;
        int mapHeight = 5;
        int roomCount = 16;
        int minBottomWidth = 1;
        int maxBottomWidth = 2;
        int[,] generatedMap = MapGenerator.GenerateMap(mapWidth, mapHeight, roomCount, minBottomWidth, maxBottomWidth);
        
        for (int i = 0; i < generatedMap.GetLength(0); i++)
        {
            for (int j = 0; j < generatedMap.GetLength(1); j++)
            {
                PlaceRoom(j, i * -1, generatedMap[i, j]);
            }
        }

        int huone = Random.Range(0, 15);
        map.RefreshAllTiles();
    }
    void PlaceRoom(int x, int y, int tile)
    {
        BoundsInt roomPosition = new BoundsInt(new Vector3Int(12 * x + ((int) startRoomPosition.x / 2), 12 * y + ((int) startRoomPosition.y), 0), size: new Vector3Int(12, 12, 1));
        BoundsInt roomSelection = new BoundsInt(new Vector3Int(0, 12 * tile + tile, 0), size: new Vector3Int(12, 12, 1));
        TileBase[] room = rooms.GetTilesBlock(roomSelection);
        map.SetTilesBlock(roomPosition, room);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
