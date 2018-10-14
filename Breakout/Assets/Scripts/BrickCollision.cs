using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class BrickCollision : MonoBehaviour {

    private Rigidbody2D rb;
    private BoxCollider2D box;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        box.size = GetComponent<SpriteRenderer>().size;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with " + collision.gameObject.name);

        //Add to score

        //Disable brick
        gameObject.SetActive(false);
    }
}
