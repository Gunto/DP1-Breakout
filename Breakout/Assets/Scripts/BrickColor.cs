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
    public Sprite[] rowSprites = new Sprite[8];
    private Sprite[] sprites;
    private List<List<GameObject>> bricks = new List<List<GameObject>>();




    public void ChangeBrickColours()
    {
        //Get List of bricks
        bricks = GameObject.FindGameObjectWithTag("Brick Manager").GetComponent<CreateBricks>().Bricks;
        //Create sprite array for rows
        sprites = new Sprite[8];
        for (int i = 0; i < sprites.Length; i++)
        {
            //Add sprites to array
            sprites[i] = rowSprites[i]; //row1 = colour1 etc
        }
        //Loop through List
        for (int i = 0; i < bricks.Count; i++)
        {
            //Loop through row
            foreach (GameObject brick in bricks[i])
            {
                //Assign sprite
                brick.GetComponent<SpriteRenderer>().sprite = sprites[i];
            }
        }
    }
}
