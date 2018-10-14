using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

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
        public void CorrectNumberofBricks()
        {
            GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
            int expected = 84;
            int actual = bricks.Length;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BricksAreCorrectSize()
        {
            GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
            bool expected = true;
            bool actual = true;
            Vector2 size = new Vector2(3.0f, 1.0f);

            foreach (GameObject brick in bricks)
            {
                if (brick.GetComponent<SpriteRenderer>().size != size)
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
                //NOTE: Will return true if editor camera can see the sprite
                if (!brick.GetComponent<SpriteRenderer>().isVisible)
                {
                    actual = false;
                }
            }

            Assert.AreEqual(expected, actual);
        }
    }
}
