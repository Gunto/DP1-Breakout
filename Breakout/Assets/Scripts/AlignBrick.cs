using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlignBrick : MonoBehaviour
{

    private Transform layoutTransform;
    private Vector3 rectPos;
    private HorizontalLayoutGroup layout;

    public Transform LayoutTransform
    {
        get
        {
            return layoutTransform;
        }

        set
        {
            layoutTransform = value;
        }
    }

    // Use this for initialization
    public void Align()
    {
        rectPos = LayoutTransform.position;
        Debug.Log(rectPos);
        transform.position = rectPos;
    }
}	
