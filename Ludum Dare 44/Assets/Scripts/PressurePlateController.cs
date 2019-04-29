using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateController : MonoBehaviour
{
    public Transform button;
    public GameObject[] walls;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        button.Translate(0, -.1f, 0);
        foreach(GameObject wall in walls)
        {
            wall.GetComponentInChildren<SpriteRenderer>().enabled = false;
            wall.GetComponent<BoxCollider2D>().enabled = false;
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (GameObject wall in walls)
        {
            wall.GetComponentInChildren<SpriteRenderer>().enabled = false;
            wall.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        button.Translate(0, .1f, 0);
        foreach (GameObject wall in walls)
        {
            wall.GetComponentInChildren<SpriteRenderer>().enabled = true;
            wall.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
