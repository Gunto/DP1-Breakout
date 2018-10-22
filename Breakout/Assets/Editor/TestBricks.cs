using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections.Generic;

namespace Assets.Editor
{
    class TestBricks
    {
        [Test]
        public void BricksHaveColliders()
        {
            GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
            bool expected = true;
            bool actual = true;

            foreach(GameObject brick in bricks)
            {
                if (brick.GetComponent<BoxCollider2D>() == null)
                {
                    actual = false;
                }
            }

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BricksHaveRenderers()
        {
            GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
            bool expected = true;
            bool actual = true;

            foreach (GameObject brick in bricks)
            {
                if (brick.GetComponent<SpriteRenderer>() == null)
                {
                    actual = false;
                }
            }

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CorrectNumberofBricks()
        {
            GameObject g = GameObject.FindGameObjectWithTag("Brick Manager");
            GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
            int expected = g.GetComponent<SetLayout>().rows * g.GetComponent<SetLayout>().bricksInRow;
            int actual = bricks.Length;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BricksAreCorrectSize()
        {
            GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
            bool expected = true;
            bool actual = true;
            Vector3 size = new Vector3(1.0f, 1.0f, 1.0f);

            foreach (GameObject brick in bricks)
            {
                if (brick.transform.localScale != size)
                {
                    actual = false;
                }
            }

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AllBricksAreVisible()
        {
            GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
            bool expected = true;
            bool actual = true;

            foreach (GameObject brick in bricks)
            {
                Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
                actual = GeometryUtility.TestPlanesAABB(planes, brick.GetComponent<SpriteRenderer>().bounds);
            }

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BricksDoNotExceedHalfway()
        {
            GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
            bool expected = true;
            bool actual = true;

            foreach (GameObject brick in bricks)
            {
                if (brick.transform.position.y < Camera.main.ScreenToWorldPoint(Vector2.up * (Screen.height / 2)).y)
                {
                    actual = false;
                }
            }

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BrickArrayNotNull()
        {
            List<List<GameObject>> bricks = GameObject.FindGameObjectWithTag("Brick Manager").GetComponent<CreateBricks>().Bricks;
            bool expected = true;
            bool actual = false;

            if (bricks != null)
            {
                actual = true;
            }

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RowObjIsParentOfBricksRow()
        {
            List<List<GameObject>> bricks = GameObject.FindGameObjectWithTag("Brick Manager").GetComponent<CreateBricks>().Bricks;
            bool expected = true;
            bool actual = true;

            foreach (List<GameObject> row in bricks)
            {
                for (int i = 0; i < row.Count; i++)
                {
                    if (row[i].transform.parent != GameObject.Find("Row " + (i + 1)))
                    {
                        actual = false;
                    }
                }
            }

            Assert.AreEqual(expected, actual);
        }
    }
}
