using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHighlight : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject adjacentHighlightPrefab; // Assign the adjacent highlight prefab

    private Vector3Int lastCellPosition;
    private List<GameObject> adjacentHighlights = new List<GameObject>();

    private void Start()
    {
        Vector3Int startCellPosition = tilemap.WorldToCell(transform.position);
        lastCellPosition = startCellPosition;
        HighlightAdjacentTiles(startCellPosition);
    }

    private void Update()
    {
        Vector3Int cellPosition = tilemap.WorldToCell(transform.position);

        if (cellPosition != lastCellPosition)
        {
            UnhighlightAdjacentTiles();
            HighlightAdjacentTiles(cellPosition);
            lastCellPosition = cellPosition;
        }
    }

    void HighlightAdjacentTiles(Vector3Int centerCell)
    {
        HighlightTile(centerCell + new Vector3Int(1, 0, 0)); // Right
        HighlightTile(centerCell + new Vector3Int(-1, 0, 0)); // Left
        HighlightTile(centerCell + new Vector3Int(0, 1, 0)); // Up
        HighlightTile(centerCell + new Vector3Int(0, -1, 0)); // Down
    }

    void HighlightTile(Vector3Int cellPosition)
    {
        // Check if the tile is within the bounds of the Tilemap
        if (tilemap.HasTile(cellPosition))
        {
            Vector3 tilePosition = tilemap.GetCellCenterWorld(cellPosition);
            GameObject adjacentHighlight = Instantiate(adjacentHighlightPrefab, tilePosition, Quaternion.identity);
            adjacentHighlights.Add(adjacentHighlight);
        }
    }

    void UnhighlightAdjacentTiles()
    {
        foreach (var adjacentHighlight in adjacentHighlights)
        {
            Destroy(adjacentHighlight);
        }

        adjacentHighlights.Clear();
    }
}
