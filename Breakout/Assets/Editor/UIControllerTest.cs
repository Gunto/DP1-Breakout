using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class UIControllerTest {

	[Test]
	public void UIControllerIncrementScoreTest() {
        int expected = 1;
        GameObject UIController = GameObject.FindGameObjectWithTag("UIController");
        UIController.GetComponent<UIController>().IncrementScore();
        int actual = UIController.GetComponent<UIController>().score;

        Assert.AreEqual(expected, actual);
    }
}
