using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetRows : MonoBehaviour {

    public InputField rowInput;
    public InputField brickInput;
    private int rows;
    private int bricks;
    private bool rowsValid;
    private bool bricksValid;

    public int Rows
    {
        get
        {
            return rows;
        }

        set
        {
            rows = value;
        }
    }

    public int Bricks
    {
        get
        {
            return bricks;
        }

        set
        {
            bricks = value;
        }
    }

    // Use this for initialization
    public void ValidateRows () {
        rowsValid = int.TryParse(rowInput.text, out rows);
        bricksValid = int.TryParse(brickInput.text, out bricks);
        if (rowsValid && bricksValid)
        {
            bricks = Mathf.Clamp(bricks, 1, 14);
            rows = Mathf.Clamp(rows, 1, bricks / 2 + 1);
            rowInput.text = rows.ToString();
            brickInput.text = bricks.ToString();
        }
	}
}
