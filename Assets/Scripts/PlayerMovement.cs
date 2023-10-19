using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public Tilemap tilemap; // Reference to the Tilemap

    private Vector3 targetPosition;
    private bool isMoving;

    void Update()
    {
        if (isMoving)
            return;

        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePosition.z = 0;

            Vector3Int cellPosition = tilemap.WorldToCell(mousePosition);

            if (IsOneTileAway(cellPosition))
            {
                // Smoothly move to the center of the cell
                StartCoroutine(MoveToTarget(tilemap.GetCellCenterWorld(cellPosition)));
            }
        }
    }

    IEnumerator MoveToTarget(Vector3 target)
    {
        isMoving = true;
        float journeyLength = Vector3.Distance(transform.position, target);
        float startTime = Time.time;

        while (transform.position != target)
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;

            transform.position = Vector3.Lerp(transform.position, target, fractionOfJourney);
            yield return null;
        }

        isMoving = false;
    }

    bool IsOneTileAway(Vector3Int position)
    {
        Vector3Int currentCell = tilemap.WorldToCell(transform.position);
        int deltaX = Mathf.Abs(position.x - currentCell.x);
        int deltaY = Mathf.Abs(position.y - currentCell.y);

        // Ensure the target is within the grid and is exactly one tile away (not diagonal)
        return IsPositionValid(position) && (deltaX == 1 || deltaY == 1) && deltaX + deltaY == 1;
    }

    bool IsPositionValid(Vector3Int position)
    {
        return tilemap.GetTile(position) != null;
    }
}
