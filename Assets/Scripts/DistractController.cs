using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DistractController : MonoBehaviour
{
    public TextMeshProUGUI distractText;
    public GameObject[] remainingMovesArray;

    // Start is called before the first frame update
    void Start()
    {
        distractText.gameObject.SetActive(false);

        foreach (GameObject moves in remainingMovesArray)
        {
            moves.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isDistracting)
        {
            if (GameManager.distractCounter == 0)
            {
                return;
            }
            else if (GameManager.distractCounter == 1)
            {
                remainingMovesArray[0].gameObject.SetActive(false);
            }
            else if (GameManager.distractCounter == 2)
            {
                remainingMovesArray[1].gameObject.SetActive(false);
            }
            else if (GameManager.distractCounter == 3)
            {
                remainingMovesArray[2].gameObject.SetActive(false);
            }
        }
    }

    public void DistractButton()
    {
        if (!GameManager.isDistracting)
        {
            GameManager.isDistracting = true;
            distractText.gameObject.SetActive(true);
            foreach (GameObject moves in remainingMovesArray)
            {
                moves.SetActive(true);
            }
        }
    }
}
