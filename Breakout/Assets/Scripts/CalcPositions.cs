using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//vRect.rect.xMin and yMin are the bottom left of the rect in world space
//Screen position origin is bottom left

[ExecuteInEditMode]
public class CalcPositions : MonoBehaviour {

    private Camera cam;
    private Vector2 screenPos;
    private List<List<Vector2>> bricks;
    public int rows;
    public int bricksInRow;
    private float rowLength;
    private RectTransform vRect;
    public SpriteRenderer sprite;
    public int width;
    public int margin, padding;
    public float startingY;
    private float offsetY;
    private Vector2 spriteScale;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        vRect = GetComponentInChildren<RectTransform>();
        //bricksInRow = 10;
        //rows = 6;
        //startingY = Mathf.Clamp(startingY, Screen.height / 2, Screen.height);
        startingY = Screen.height * 0.85f;
        screenPos = new Vector2(width - margin, 0);
        vRect.sizeDelta = ScreenToWorld(screenPos) * 2f;
        rowLength = GetRowLength();
        offsetY = cam.WorldToScreenPoint(spriteScale).y;
        bricks = CreateBrickRows(rows);
    }

    // Update is called once per frame
    void Update () {
        width = Mathf.Clamp(width, Screen.width / 2, Screen.width);
        margin = Mathf.Clamp(margin, 0, Screen.width / 10); //Needs work
        padding = Mathf.Clamp(padding, 0, Screen.width / bricksInRow); //Needs work
        bricksInRow = Mathf.Clamp(bricksInRow, 1, 14);
        rows = Mathf.Clamp(rows, 1, bricksInRow / 2 + 1);
        //startingY = Mathf.Clamp(startingY, Screen.height / 2, Screen.height);
        screenPos = new Vector2(width - margin, 0);
        vRect.sizeDelta = ScreenToWorld(screenPos) * 2f;
        rowLength = GetRowLength();
        spriteScale = GetBrickScale();
        offsetY = cam.WorldToScreenPoint(Vector3.Scale(sprite.size, spriteScale)).y + padding - (Screen.height / 2);
        for (int i = 0; i < bricks.Count; i++)
        {
            UpdateRowPositions(bricks[i], startingY - (offsetY * i));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        foreach (List<Vector2> row in bricks)
        {
            foreach (Vector2 position in row)
            {
                Gizmos.DrawWireCube(ScreenToWorld(position), Vector3.Scale(sprite.size, spriteScale));
            }
        }
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

    private List<List<Vector2>> CreateBrickRows(int amount)
    {
        List<List<Vector2>> tempList = new List<List<Vector2>>(amount);
        for (int i = 0; i < amount; i++)
        {
            tempList.Add(CreateRowArray(bricksInRow, rowLength, startingY - (offsetY * i)));
        }
        return tempList;
    }

    private List<Vector2> CreateRowArray(int amount, float rowLength, float y)
    {
        List<Vector2> tempRow = new List<Vector2>(amount);
        float x = rowLength / amount;
        float startingPos = cam.WorldToScreenPoint(new Vector2(vRect.rect.xMin, 0)).x;
        for (int i = 0; i < amount; i++)
        {
            tempRow.Add(new Vector2((x * (i + 1)) - (x / 2) + startingPos, y));
        }
        return tempRow;
    }

    private void UpdateRowPositions(List<Vector2> row, float y)
    {
        float x = rowLength / bricksInRow;
        float startingPos = cam.WorldToScreenPoint(new Vector2(vRect.rect.xMin, 1)).x;
        for (int i = 0; i < row.Count; i++)
        {
            row[i] = new Vector2((x * (i + 1)) - (x / 2) + startingPos, y);
        }
    }

    private Vector2 GetBrickScale()
    {
        float gaps = (bricksInRow - 1) * padding; //determine how many pixels are gaps
        float totalBrickLength = (rowLength - gaps) / bricksInRow; //calculate total pixel width required from single brick
        totalBrickLength = ScreenToWorld(new Vector2(totalBrickLength + Screen.width / 2, 0)).x;
        return new Vector2((totalBrickLength / sprite.size.x), (totalBrickLength / sprite.size.x));
    }

    private Vector3 ScreenToWorld (Vector3 screenPos)
    {
        return cam.ScreenToWorldPoint(screenPos);
    }
}
