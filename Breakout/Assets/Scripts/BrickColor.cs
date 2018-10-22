using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickColor : MonoBehaviour {

    //public Color row1;
    //public Color row2;
    //public Color row3;
    //public Color row4;
    //public Color row5;
    //public Color row6;
    //public Color row7;
    //public Color row8;
    public Color[] rowColors = new Color[8];
    private Color[] colours;
    private List<List<GameObject>> bricks = new List<List<GameObject>>();




    public void ChangeBrickColours()
    {
        //Get List of bricks
        bricks = GameObject.FindGameObjectWithTag("Brick Manager").GetComponent<CreateBricks>().Bricks;
        //Create colour array for rows
        colours = new Color[8];
        for (int i = 0; i < colours.Length; i++)
        {
            //Add colours to array
            colours[i] = rowColors[i]; //row1 = colour1 etc
        }
        //Loop through List
        for (int i = 0; i < bricks.Count; i++)
        {
            //Loop through row
            foreach (GameObject brick in bricks[i])
            {
                //Assign colour
                brick.GetComponent<SpriteRenderer>().color = colours[i];
            }
        }
    }
}
