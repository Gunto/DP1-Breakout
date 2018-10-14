using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBricks : MonoBehaviour {

    public Sprite sprite;
    public float width, height;
    private GameObject brick;
    private SpriteRenderer spRen;
    private Vector2 scale;
    private Vector3 position;
    private Vector3 screenPosition;
    private Camera cam;

    public int margin; //Gap from screen edge
    public int padding; //Gap in between bricks

	// Use this for initialization
	void Start ()
    {
        cam = Camera.main;
        scale = new Vector2(width, height);
        screenPosition = new Vector3(0, Screen.height * 4/5);
        position = cam.ScreenToWorldPoint(screenPosition);
        CreateSprite();
        Debug.Log(Screen.height);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void CreateSprite()
    {
        brick = new GameObject();
        brick.AddComponent<SpriteRenderer>();
        spRen = brick.GetComponent<SpriteRenderer>();
        spRen.sprite = sprite;
        spRen.drawMode = SpriteDrawMode.Sliced;
        spRen.size = scale;
        brick.transform.position = new Vector2(position.x + scale.x / 2, position.y);
    }

    private void LayoutBrickRow()
    {

    }
   
}
