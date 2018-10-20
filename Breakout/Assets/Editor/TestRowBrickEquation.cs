using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Editor
{
    class TestRowBrickEquation
    {
        [Test]
        public void TestHalfBPlusOne()
        {
            int b = 9;
            int r = 6;
            int expected = r;
            int actual = Mathf.CeilToInt((float)b / 2) + 1;

            Assert.AreEqual(expected, actual);
        }
        
    }
}
