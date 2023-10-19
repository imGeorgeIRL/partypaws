using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCollection : MonoBehaviour
{
    private BoxCollider2D bxCollider;
    // Start is called before the first frame update
    void Start()
    {
        bxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.distractCounter++;
            Destroy(gameObject);
        }
    }
}
