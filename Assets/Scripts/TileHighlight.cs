using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHighlight : MonoBehaviour
{
    public Tilemap tilemap;
    public Material highlightMaterial;

    private Vector3Int lastCellPosition;

    private void Start()
    {
        lastCellPosition = tilemap.WorldToCell(transform.position);
        HighlightAdjacentCells(lastCellPosition);
    }

    private void Update()
    {
        Vector3Int cellPosition = tilemap.WorldToCell(transform.position);

        if (cellPosition != lastCellPosition)
        {
            UnhighlightCells();
            HighlightAdjacentCells(cellPosition);
            lastCellPosition = cellPosition;
        }
    }

    void HighlightAdjacentCells(Vector3Int centerCell)
    {
        HighlightCell(centerCell + new Vector3Int(1, 0, 0)); // Right
        HighlightCell(centerCell + new Vector3Int(-1, 0, 0)); // Left
        HighlightCell(centerCell + new Vector3Int(0, 1, 0)); // Up
        HighlightCell(centerCell + new Vector3Int(0, -1, 0)); // Down
    }

    void HighlightCell(Vector3Int cellPosition)
    {
        tilemap.SetTileFlags(cellPosition, TileFlags.None);
        tilemap.SetColor(cellPosition, Color.gray);
    }

    void UnhighlightCells()
    {
        BoundsInt bounds = tilemap.cellBounds;
        foreach (var position in bounds.allPositionsWithin)
        {
            tilemap.SetTileFlags(position, TileFlags.None);
            tilemap.SetColor(position, Color.white);
        }
    }
}
