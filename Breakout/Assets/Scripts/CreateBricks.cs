using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBricks : MonoBehaviour {

    public Sprite sprite;
    private GameObject brick;
    private GameObject vertical;
    private GameObject horizontal;
    public GameObject layout;
    private SpriteRenderer spRen;
    public Vector2 scale;
    private Transform[] row;
    private Transform[] column;
    public float scaleAmount = 1f;

	// Use this for initialization
	void Start ()
    {
        column = new Transform[layout.transform.childCount];
        for (int i = 0; i < layout.transform.childCount; i++)
        {
            column[i] = layout.transform.GetChild(i);
        }
        row = layout.transform.GetChild(0).GetComponentsInChildren<Transform>();
        vertical = new GameObject
        {
            name = "Bricks Group"
        };
        CreateSets();
        layout.SetActive(false); //Disable UI used for layout
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void CreateSets()
    {
        for (int i = 0; i < column.Length; i++)
        {
            horizontal = new GameObject
            {
                name = "Row " + (i + 1)
            };
            horizontal.transform.SetParent(vertical.transform, true); //Parent under Bricks Group
            horizontal.transform.position = column[0].transform.position; //Set initial position to match row used
            CreateRows();
            horizontal.transform.position = column[i].transform.position; //Reposition row to match BUG: Moves child transform as well
        }
    }

    private void CreateRows()
    {
        for (int i = 1; i < row.Length; i++)
        {
            CreateSprite();
            AlignBricks(row[i]);
        }
    }

    private void CreateSprite()
    {
        brick = new GameObject();
        brick.transform.SetParent(horizontal.transform, true);
        brick.AddComponent<SpriteRenderer>();
        //brick.AddComponent<BrickCollision>();
        spRen = brick.GetComponent<SpriteRenderer>();
        spRen.sprite = sprite;
        spRen.drawMode = SpriteDrawMode.Sliced;
        spRen.size = scale;
    }

    private void AlignBricks(Transform position)
    {
        brick.AddComponent<AlignBrick>();
        brick.GetComponent<AlignBrick>().LayoutTransform = position;
        brick.GetComponent<AlignBrick>().Align();
        brick.transform.localScale = new Vector3(scaleAmount, scaleAmount, 1);
    }
}
