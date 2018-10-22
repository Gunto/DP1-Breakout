using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour {

    public GameObject UIController;

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            Destroy(gameObject);
            UIController = GameObject.FindWithTag("UIController");
            UIController.GetComponent<UIController>().IncrementScore();
        }
    }
}
