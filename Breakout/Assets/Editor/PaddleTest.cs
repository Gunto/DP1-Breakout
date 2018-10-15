using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PaddleTest {

    [Test]
    public void PaddleExist()
    {
        GameObject[] paddle  = GameObject.FindGameObjectsWithTag("Paddle");
        bool expected = true;
        bool actual = false;

        if(paddle.Length > 0)
        {
            actual = true;
        }

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void PaddleColliderExist()
    {
        GameObject[] paddle = GameObject.FindGameObjectsWithTag("Paddle");
        bool expected = true;
        bool actual = false;

        foreach (GameObject p in paddle)
        {
            if (p.GetComponent<BoxCollider2D>() != null)
            {
                actual = true;
            }
        }

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void PaddleCameraExist()
    {
        GameObject[] paddle = GameObject.FindGameObjectsWithTag("Paddle");
        bool expected = true;
        bool actual = false;

        foreach (GameObject p in paddle)
        {
            if (p.GetComponent<PaddleMove>().mainCam != null)
            {
                actual = true;
            }
        }

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CorrectNumberofPaddles()
    {
        GameObject[] paddle = GameObject.FindGameObjectsWithTag("Paddle");
        int expected = 1;
        int actual = paddle.Length;

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void PaddleCorrectSize()
    {
        GameObject[] paddle = GameObject.FindGameObjectsWithTag("Paddle");
        bool expected = true;
        bool actual = false;

        Vector3 size = new Vector3(2.0f, 2.0f, 2.0f);

        foreach (GameObject p in paddle)
        {
            if (p.transform.localScale == size)
            {
                actual = true;
            }
        }

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void PaddleIsVisible()
    {
        GameObject[] paddle = GameObject.FindGameObjectsWithTag("Paddle");
        bool expected = true;
        bool actual = false;

        foreach (GameObject p in paddle)
        {
            //NOTE: Will return true if editor camera can see the sprite
            if (p.GetComponent<SpriteRenderer>().isVisible)
            {
                actual = true;
            }
        }
        Assert.AreEqual(expected, actual);
    }
}
