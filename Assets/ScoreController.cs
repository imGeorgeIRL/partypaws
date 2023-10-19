using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreController : MonoBehaviour
{
    public int scoreRequirement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.foodCollected == scoreRequirement)
        {
            StartCoroutine(WaitForLoad());
        }
    }

    private IEnumerator WaitForLoad()
    {
        yield return new WaitForSeconds(1);
        LoadNextLevel();
    }

    private void LoadNextLevel()
    {
        GameManager.isDistracting = false;
        GameManager.distractionsUsed = 0;
        GameManager.foodCollected = 0;
        GameManager.distractCounter = 0;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

        SceneManager.LoadScene(nextSceneIndex);
    }
}
