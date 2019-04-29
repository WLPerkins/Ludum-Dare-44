using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnTouch : MonoBehaviour
{
    public float moveSpeed;
    private SpriteRenderer sprite;
    private bool moving;
    private bool againstWall;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Collided");
        if (collision.gameObject.CompareTag("Player"))
        {
            moving = true;
            collision.collider.transform.SetParent(transform);
            Invoke("DestroyPlatform", 3f);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(collision.gameObject.ToString() + " left platform");
            collision.collider.transform.SetParent(null);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moving)
        {
            
            transform.Translate(Vector3.right * moveSpeed * Time.fixedDeltaTime);
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - .1f * Time.fixedDeltaTime);
            
        }
    }

    void DestroyPlatform()
    {
        Destroy(gameObject);
    }
}
