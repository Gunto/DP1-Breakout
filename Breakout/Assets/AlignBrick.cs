using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignBrick : MonoBehaviour {

    public RectTransform rect;
    private Vector2 rectPos;

	// Use this for initialization
	void Start () {
        rectPos = Camera.main.ScreenToWorldPoint(rect.rect.center);
        transform.position = rectPos;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
