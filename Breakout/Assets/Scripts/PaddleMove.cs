using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMove : MonoBehaviour {

    public Camera mainCam;
    public float spd;
    private Collider2D collider2D;
    private float paddleHalfWidth;

    private float camHeight, camWidth;

    // Use this for initialization
    void Start () {
        //mainCam.
        camHeight = 2f * mainCam.orthographicSize;
        camWidth = camHeight * mainCam.aspect;
        collider2D = GetComponent<BoxCollider2D>();
        paddleHalfWidth = (collider2D.bounds.size.x / 2);
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.LogError(camWidth);
        if (gameObject.transform.position.x < ((camWidth / 2 ) - paddleHalfWidth))
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    gameObject.transform.position -= new Vector3(-0.1f * spd, 0, 0);
                }
            }

        if (gameObject.transform.position.x > (-camWidth / 2) + paddleHalfWidth)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                gameObject.transform.position -= new Vector3(0.1f * spd, 0, 0);
            }
        }
    }
}
