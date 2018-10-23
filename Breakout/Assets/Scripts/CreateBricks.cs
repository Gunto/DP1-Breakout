using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBricks : MonoBehaviour {

    private Camera cam;
    private Vector2 screenPos;
    private List<List<GameObject>> bricks = new List<List<GameObject>>();
    private int rows;
    private int bricksInRow;
    private float rowLength;
    private RectTransform vRect;
    public GameObject sprite;
    public SetRows setter;
    private SpriteRenderer spRen;
    private int margin, padding;
    private float startingY;
    private float offsetY;
    private Vector2 spriteScale;

    public List<List<GameObject>> Bricks
    {
        get
        {
            return bricks;
        }
    }


    // Use this for initialization
    void Start()
    {
        cam = Camera.main;
        vRect = GetComponentInChildren<RectTransform>();
        startingY = Screen.height * 0.85f;
        margin = 15;
        padding = 15;
        rows = setter.Rows;
        bricksInRow = setter.Bricks;
        spRen = sprite.GetComponent<SpriteRenderer>();
        screenPos = new Vector2(Screen.width - margin, 0);
        vRect.sizeDelta = ScreenToWorld(screenPos) * 2f;
        rowLength = GetRowLength();
        spriteScale = GetBrickScale();
        offsetY = cam.WorldToScreenPoint(Vector3.Scale(spRen.size, spriteScale)).y + padding - (Screen.height / 2);
        bricks = CreateBrickRows(rows);
        //Change the colors of the bricks
        GetComponent<BrickColor>().ChangeBrickColours();
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.cyan;
    //    foreach (List<GameObject> row in bricks)
    //    {
    //        foreach (GameObject g in row)
    //        {
    //            Gizmos.DrawWireCube(ScreenToWorld(g.transform.position), Vector3.Scale(spRen.size, spriteScale));
    //        }
    //    }
    //}

    /// <summary>
    /// Calculates length of row in screen pixels
    /// </summary>
    /// <returns></returns>
    private float GetRowLength()
    {
        Vector2 start = cam.WorldToScreenPoint(new Vector2(vRect.rect.xMin, 1));
        Vector2 end = cam.WorldToScreenPoint(new Vector2(vRect.rect.xMax, 1));
        Vector2 result = end - start;
        return result.x;
    }

    /// <summary>
    /// Creates all rows of bricks specified by an amount
    /// </summary>
    /// <param name="amount">How many rows to create</param>
    /// <returns></returns>
    private List<List<GameObject>> CreateBrickRows(int amount)
    {
        List<List<GameObject>> tempList = new List<List<GameObject>>(amount);
        for (int i = 0; i < amount; i++)
        {
            GameObject g = new GameObject();
            g.name = "Row " + (i + 1);
            tempList.Add(CreateRowArray(bricksInRow, rowLength, startingY - (offsetY * i), g));
        }
        return tempList;
    }

    /// <summary>
    /// Creates all the bricks in a row
    /// </summary>
    /// <param name="amount">How many bricks in the row</param>
    /// <param name="rowLength">How long the row is</param>
    /// <param name="y">Y co-ordinate in screen space</param>
    /// <returns></returns>
    private List<GameObject> CreateRowArray(int amount, float rowLength, float y, GameObject parent)
    {
        List<GameObject> tempRow = new List<GameObject>(amount);
        float x = rowLength / amount;
        float startingPos = cam.WorldToScreenPoint(new Vector2(vRect.rect.xMin, 0)).x;
        for (int i = 0; i < amount; i++)
        {
            tempRow.Add(CreateBrick(new Vector2((x * (i + 1)) - (x / 2) + startingPos, y), parent));
        }
        return tempRow;
    }

    /// <summary>
    /// Create an individual brick at position
    /// </summary>
    /// <param name="position">Position of brick in screen space</param>
    /// <returns></returns>
    private GameObject CreateBrick(Vector2 position, GameObject parent)
    {
        GameObject g = Instantiate(sprite) as GameObject;
        g.transform.position = ScreenToWorld(position) - Vector3.forward * cam.transform.position.z;
        g.transform.localScale = spriteScale;
        g.transform.parent = parent.transform;
        return g;
    }

    /// <summary>
    /// Calculate how big the bricks should be in a row
    /// </summary>
    /// <returns></returns>
    private Vector2 GetBrickScale()
    {
        float gaps = (bricksInRow - 1) * padding; //determine how many pixels are gaps
        float totalBrickLength = (rowLength - gaps) / bricksInRow; //calculate total pixel width required from single brick
        totalBrickLength = ScreenToWorld(new Vector2(totalBrickLength + Screen.width / 2, 0)).x;
        return new Vector2((totalBrickLength / spRen.size.x), (totalBrickLength / spRen.size.x));
    }

    /// <summary>
    /// Shorthand method for ScreenToWorldPoint
    /// </summary>
    /// <param name="screenPos">Position in screen space to be converted</param>
    /// <returns></returns>
    private Vector3 ScreenToWorld(Vector3 screenPos)
    {
        return cam.ScreenToWorldPoint(screenPos);
    }
}
