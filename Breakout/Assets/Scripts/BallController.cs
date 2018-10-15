using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {


    public float speed;

    private Rigidbody2D rigid2D;
    private bool ballInPlay;

    // Use this for initialization
    void Start () {
        rigid2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1") && ballInPlay == false)
        {
            rigid2D.velocity = Vector2.down * speed;
            ballInPlay = true;
        }
	}

    float hitFactor(Vector2 ballPosition, Vector2 paddlePosition, float paddleWidth)
    {
        return (ballPosition.x - paddlePosition.x) / paddleWidth;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Paddle")
        {
            float x = hitFactor(transform.position, other.transform.position, other.collider.bounds.size.x);
            Vector2 direction = new Vector2(x, 1).normalized;
            rigid2D.velocity = direction * speed;
        }
        else if (other.gameObject.tag == "Wall")
        {
            Vector2 newVelocity = Vector2.Reflect(rigid2D.velocity, other.contacts[0].normal);
            rigid2D.velocity = newVelocity;
        }
        else if (other.gameObject.tag == "Brick")
        {
            Vector2 newVelocity = Vector2.Reflect(rigid2D.velocity, other.contacts[0].normal);
            rigid2D.velocity = newVelocity;
        }
    }


}
