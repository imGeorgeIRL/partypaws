using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DistractController : MonoBehaviour
{
    public TextMeshProUGUI distractText;
    public GameObject[] remainingMovesArray;

    public int distractionsForLevel;

    public Button distractButton;

    public GameObject[] distractionCounterArray;
    // Start is called before the first frame update
    void Start()
    {
        distractText.gameObject.SetActive(false);

        foreach (GameObject moves in remainingMovesArray)
        {
            moves.SetActive(false);
        }

        switch (distractionsForLevel)
        {
            case 0:
                foreach (GameObject counter in distractionCounterArray)
                {
                    counter.SetActive(false);
                }
                break;
            case 1:
                distractionCounterArray[0].gameObject.SetActive(true);
                distractionCounterArray[1].gameObject.SetActive(false);
                distractionCounterArray[2].gameObject.SetActive(false);
                break;
            case 2:
                distractionCounterArray[0].gameObject.SetActive(true);
                distractionCounterArray[1].gameObject.SetActive(true);
                distractionCounterArray[2].gameObject.SetActive(false);
                break;
            case 3:
                distractionCounterArray[0].gameObject.SetActive(true);
                distractionCounterArray[1].gameObject.SetActive(true);
                distractionCounterArray[2].gameObject.SetActive(true);
                break;
            default:
                Debug.Log("There is no case for this many distractions in the Level");
                break;
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

        if (GameManager.distractionsUsed >= distractionsForLevel)
        {
            Color newColor = new Color(140 / 255.0f, 140 / 255.0f, 140 / 255.0f, 255 / 255.0f);
            ColorBlock colorBlock = distractButton.colors;
            colorBlock.normalColor = newColor;

            // Assign the modified ColorBlock back to the button
            distractButton.colors = colorBlock;
        }

        switch (GameManager.distractionsUsed)
        {
            case 0:
                break;
            case 1:
                switch (distractionsForLevel)
                {
                    case 1:
                        distractionCounterArray[0].gameObject.SetActive(false);
                        break;
                    case 2:
                        distractionCounterArray[1].gameObject.SetActive(false);
                        break;
                    case 3:
                        distractionCounterArray[2].gameObject.SetActive(false);
                        break;
                    default:
                        break;
                }
                break;
            case 2:
                switch (distractionsForLevel)
                {
                    case 2:
                        distractionCounterArray[0].gameObject.SetActive(false);
                        break;
                    case 3:
                        distractionCounterArray[1].gameObject.SetActive(false);
                        break;
                    default:
                        break;
                }
                break;
            case 3:
                switch (distractionsForLevel)
                {
                    case 3:
                        distractionCounterArray[0].gameObject.SetActive(false);
                        break;
                    default:
                        break;
                }
                break;
            default:
                Debug.Log("There is no case for this many distractions in the Level");
                break;
        }

    }

    public void DistractButton()
    {
        if (GameManager.distractionsUsed != distractionsForLevel)
        {
            if (!GameManager.isDistracting)
            {
                GameManager.distractionsUsed++;
                GameManager.isDistracting = true;
                distractText.gameObject.SetActive(true);
                foreach (GameObject moves in remainingMovesArray)
                {
                    moves.SetActive(true);
                }
            }
        }
    }
}
