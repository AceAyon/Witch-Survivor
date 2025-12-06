using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileInfiniteGenerator : MonoBehaviour
{
    public Tilemap tilemap;

    public TileBase basicTile;
    public TileBase plantTile;
    public TileBase crackedTile;

    public int chunkSize = 10;
    public int renderDistance = 3; // number of chunks around the camera

    private Dictionary<Vector2Int, bool> generatedChunks = new Dictionary<Vector2Int, bool>();
    private HashSet<Vector3Int> placedTiles = new HashSet<Vector3Int>();

    private Vector2Int[] directions = {
        Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right,
        new Vector2Int(1,1), new Vector2Int(1,-1), new Vector2Int(-1,1), new Vector2Int(-1,-1)
    };

    void Update()
    {
        Vector3 camPos = Camera.main.transform.position;
        Vector2Int currentChunk = WorldToChunk(camPos);

        for (int x = -renderDistance; x <= renderDistance; x++)
        {
            for (int y = -renderDistance; y <= renderDistance; y++)
            {
                Vector2Int chunkCoord = new Vector2Int(currentChunk.x + x, currentChunk.y + y);

                if (!generatedChunks.ContainsKey(chunkCoord))
                {
                    GenerateChunk(chunkCoord);
                    generatedChunks[chunkCoord] = true;
                }
            }
        }
    }

    Vector2Int WorldToChunk(Vector3 pos)
    {
        return new Vector2Int(
            Mathf.FloorToInt(pos.x / chunkSize),
            Mathf.FloorToInt(pos.y / chunkSize)
        );
    }

    void GenerateChunk(Vector2Int chunkPos)
    {
        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                Vector3Int tilePos = new Vector3Int(
                    chunkPos.x * chunkSize + x,
                    chunkPos.y * chunkSize + y,
                    0
                );

                if (placedTiles.Contains(tilePos))
                    continue;

                TileBase tile = ChooseTile(tilePos);
                tilemap.SetTile(tilePos, tile);
                placedTiles.Add(tilePos);
            }
        }
    }

    TileBase ChooseTile(Vector3Int pos)
    {
        // Weighted base probability
        int r = Random.Range(0, 100);

        TileBase baseChoice;

        if (r < 70)
            baseChoice = basicTile;
        else if (r < 90)
            baseChoice = plantTile;
        else
            baseChoice = crackedTile;

        // Cluster logic for plant tile
        if (baseChoice == basicTile)
        {
            if (NearbyPlant(pos) && Random.value < 0.4f)
                return plantTile;
        }

        return baseChoice;
    }

    bool NearbyPlant(Vector3Int pos)
    {
        foreach (Vector2Int dir in directions)
        {
            Vector3Int p = pos + new Vector3Int(dir.x, dir.y, 0);

            TileBase t = tilemap.GetTile(p);

            if (t == plantTile)
                return true;
        }
        return false;
    }
}
