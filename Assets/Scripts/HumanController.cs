using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HumanController : MonoBehaviour
{
    public Transform humanFull;
    public Transform rotationAnchor;

    public float startingAngle;
    public float rotatedAngle;

    private bool hasRotated = false;

    private BoxCollider2D bxCollider;

    // Start is called before the first frame update
    void Start()
    {
        if (humanFull == null || rotationAnchor == null)
        {
            Debug.LogError("Please assign the Object to Rotate and Rotation Anchor in the Inspector.");
            enabled = false;
        }

        bxCollider = GetComponent<BoxCollider2D>();
    }

    //ROTATING------------------------------------------------------------------
    public void RotateObject()
    {
        // Calculate the angle of rotation around the Z-axis.
        float angleToRotate = rotatedAngle;

        // Get the direction from the anchor to the object.
        Vector3 direction = humanFull.position - rotationAnchor.position;

        // Calculate the new rotation angle.
        float newZRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + angleToRotate;

        // Apply the new rotation only to the Z-axis.
        humanFull.rotation = Quaternion.Euler(0, 0, newZRotation);
    }

    public void ResetObject()
    {
        // Calculate the angle of rotation around the Z-axis.
        float angleToRotate = startingAngle;

        // Get the direction from the anchor to the object.
        Vector3 direction = humanFull.position - rotationAnchor.position;

        // Calculate the new rotation angle.
        float newZRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + angleToRotate;

        // Apply the new rotation only to the Z-axis.
        humanFull.rotation = Quaternion.Euler(0, 0, newZRotation);
    }

    private void Update()
    {
        if (GameManager.isDistracting && !hasRotated)
        {
            RotateObject();
            hasRotated = true;
        }
        if (!GameManager.isDistracting && hasRotated)
        {
            StartCoroutine(WaitForReset());
            hasRotated = false;
        }
    }

    private IEnumerator WaitForReset()
    {
        yield return new WaitForSeconds(0.25f);
        ResetObject();
    }


    //DEATH-------------------------------

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Collided with Player");
            StartCoroutine(DeathCoroutine());
        }
    }

    private IEnumerator DeathCoroutine()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        yield return new WaitForSeconds(0.5f);
        GameManager.isDistracting = false;
        GameManager.distractionsUsed = 0;
        GameManager.foodCollected = 0;
        GameManager.distractCounter = 0;
        SceneManager.LoadScene(currentSceneName);
    }
}
