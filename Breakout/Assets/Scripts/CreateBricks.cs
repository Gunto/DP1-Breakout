using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBricks : MonoBehaviour {

    private Camera cam;
    private Vector2 screenPos;
    private List<List<GameObject>> bricks;
    private int rows;
    private int bricksInRow;
    private float rowLength;
    private RectTransform vRect;
    public GameObject sprite;
    private SpriteRenderer spRen;
    private int margin, padding;
    private float startingY;
    private float offsetY;
    private Vector2 spriteScale;


    // Use this for initialization
    void Start()
    {
        cam = Camera.main;
        vRect = GetComponentInChildren<RectTransform>();
        startingY = Screen.height * 0.85f;
        margin = 15;
        padding = 15;
        rows = GetComponent<SetLayout>().rows;
        bricksInRow = GetComponent<SetLayout>().bricksInRow;
        spRen = sprite.GetComponent<SpriteRenderer>();
        screenPos = new Vector2(Screen.width - margin, 0);
        vRect.sizeDelta = ScreenToWorld(screenPos) * 2f;
        rowLength = GetRowLength();
        spriteScale = GetBrickScale();
        offsetY = cam.WorldToScreenPoint(Vector3.Scale(spRen.size, spriteScale)).y + padding - (Screen.height / 2);
        bricks = CreateBrickRows(rows);
    }

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
            tempList.Add(CreateRowArray(bricksInRow, rowLength, startingY - (offsetY * i)));
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
    private List<GameObject> CreateRowArray(int amount, float rowLength, float y)
    {
        List<GameObject> tempRow = new List<GameObject>(amount);
        float x = rowLength / amount;
        float startingPos = cam.WorldToScreenPoint(new Vector2(vRect.rect.xMin, 0)).x;
        for (int i = 0; i < amount; i++)
        {
            tempRow.Add(CreateBrick(new Vector2((x * (i + 1)) - (x / 2) + startingPos, y)));
        }
        return tempRow;
    }

    /// <summary>
    /// Create an individual brick at position
    /// </summary>
    /// <param name="position">Position of brick in screen space</param>
    /// <returns></returns>
    private GameObject CreateBrick(Vector2 position)
    {
        GameObject g = Instantiate(sprite) as GameObject;
        g.transform.position = ScreenToWorld(position) - Vector3.forward * cam.transform.position.z;
        g.transform.localScale = spriteScale;
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
