using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class BallTest {
    [Test]
    public void BallExists()
    {
        GameObject[] ball = GameObject.FindGameObjectsWithTag("Ball");
        bool expected = true;
        bool actual = true;

        if (ball.Length <= 0)
        {
            actual = false;
        }

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void BallColliderExists()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        bool expected = true;
        bool actual = true;
        
        foreach(GameObject b in balls)
        {
            if (b.GetComponent<BoxCollider2D>() == null)
            {
                actual = false;
            }
        }
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CorrectNumberOfBalls()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        int expected = 1;
        int actual = balls.Length;

        
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void BallIsVisable()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        bool expected = true;
        bool actual = true;

        foreach (GameObject b in balls)
        {
            if (!b.GetComponent<SpriteRenderer>().isVisible)
            {
                actual = false;
            }
        }
        Assert.AreEqual(expected, actual);
    }
}
