using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SetLayout : MonoBehaviour
{
    public int rows;
    public int bricksInRow;

    // Update is called once per frame
    void Update()
    {
        bricksInRow = Mathf.Clamp(bricksInRow, 1, 14);
        rows = Mathf.Clamp(rows, 1, bricksInRow / 2 + 1);
    }
}
