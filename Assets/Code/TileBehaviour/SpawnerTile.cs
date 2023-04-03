using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SpawnerTile : Tile
{
    [SerializeField] GameObject prefab;
    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        base.RefreshTile(position, tilemap);
#if UNITY_EDITOR
        if (Application.isPlaying)
        {
#endif

        if (tilemap.GetTile(position) != null)
        {
            Vector3 prefabPosition = tilemap.GetComponent<Tilemap>().CellToWorld(position);
            prefabPosition.x += 0.5f;
            prefabPosition.y += 0.5f;
            Instantiate(prefab, prefabPosition, Quaternion.identity);
            GameManager.spawnerTiles.Add(position);
        }
        

#if UNITY_EDITOR
        }
#endif
    }
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
        tileData.color = Color.clear;

#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            tileData.color = Color.white;
        }
#endif
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/2D/Tiles/Spawner Tile")]
    public static void CreateSpawnerTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Spawner Tile", "New Spawner Tile", "Asset", "Save Spawner Tile", "Assets");
        if (path == "") return;

        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<SpawnerTile>(), path);
    }
#endif
}
