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

    public LayerMask obstacleLayer; // A layer mask to define which layers to consider as obstacles (Table, Human, etc.)
    public float raycastDistance = 1.0f; // The distance to cast the ray

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
                if (GameManager.isDistracting)
                {
                    GameManager.distractCounter++; // Increment the distract counter
                    if (GameManager.distractCounter >= 4)
                    {
                        GameManager.isDistracting = false; // Set isDistracting to false
                        GameManager.distractCounter = 0; // Reset the counter
                    }
                }
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
        int deltaX = position.x - currentCell.x;
        int deltaY = position.y - currentCell.y;

        // Ensure the target is within the grid and is exactly one tile away (not diagonal)
        if (IsPositionValid(position) && (Mathf.Abs(deltaX) + Mathf.Abs(deltaY) == 1))
        {
            // Check if there are obstacles in the target direction
            if (deltaX < 0 && IsObstacleInDirection(transform.position, Vector2.left)) return false;
            if (deltaX > 0 && IsObstacleInDirection(transform.position, Vector2.right)) return false;
            if (deltaY < 0 && IsObstacleInDirection(transform.position, Vector2.down)) return false;
            if (deltaY > 0 && IsObstacleInDirection(transform.position, Vector2.up)) return false;

            return true; // No obstacles in the target direction, so allow the move
        }

        return false;
    }

    bool IsObstacleInDirection(Vector3 position, Vector2 direction)
{
    Vector2 directionVector = direction.normalized;
    RaycastHit2D hit = Physics2D.Raycast(position, directionVector, raycastDistance, obstacleLayer);

    if (hit.collider != null)
    {
        if (hit.collider.CompareTag("Table") || hit.collider.CompareTag("Human"))
        {
            return true; // There's an obstacle (Table or Human) in this direction
        }
    }

    return false; // No obstacle in this direction
}


    bool IsPositionValid(Vector3Int position)
    {
        return tilemap.GetTile(position) != null;
    }
}
