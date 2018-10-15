using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScale : MonoBehaviour {

    private Transform[] bricks;
    public float scaleAmount;
    private Vector3 scale;

	// Use this for initialization
	void Start () {
        bricks = GetComponentsInChildren<Transform>();
        scale = new Vector3(scaleAmount, scaleAmount, 1);
        //Start at one to skip over parent
        for (int i = 1; i < bricks.Length; i++)
        {
            bricks[i].localScale = scale;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
