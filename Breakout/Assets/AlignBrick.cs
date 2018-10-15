using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlignBrick : MonoBehaviour {

    public RectTransform rect;
    private Vector3 rectPos;
    private HorizontalLayoutGroup layout;

	// Use this for initialization
	void Start () {
        //layout = rect.gameObject.GetComponentInParent<HorizontalLayoutGroup>();
        //layout.enabled = false;
        rectPos = rect.transform.position;
        Debug.Log(rectPos);
        transform.position = rectPos;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
