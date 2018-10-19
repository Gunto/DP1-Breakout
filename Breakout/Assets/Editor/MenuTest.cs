using NUnit.Framework;
using UnityEngine;

namespace Assets.Editor
{
    class MenuTest
    {
        [Test]
        public void PlayGameButtonChangesScene()
        {
            MenuController mc = GameObject.Find("MenuController").GetComponent<MenuController>();
            mc.ToggleMenu();
            bool expected = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Main";
            bool actual = mc.InMenu;
            Assert.AreEqual(expected, actual); 
        }

        [Test]
        public void InstructionsButtonPressTest()
        {
            MenuController mc = GameObject.Find("MenuController").GetComponent<MenuController>();
            mc.InstructionsButton();
            bool actual = mc.InstructionsOpened;
            Assert.IsTrue(actual);
        }

        [Test]
        public void ExitButtonPressTest()
        {
            MenuController mc = GameObject.Find("MenuController").GetComponent<MenuController>();
            mc.ExitGame();
            bool actual = mc.Exited;
            Assert.IsTrue(actual);
        }
    }
}