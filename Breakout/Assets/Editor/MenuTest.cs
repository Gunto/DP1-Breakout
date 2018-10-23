using NUnit.Framework;
using UnityEngine;

namespace Assets.Editor
{
    class MenuTest
    {
        [Test]
        public void PlayGameButtonChangesScene()
        {
			GameObject go = new GameObject();
			go.AddComponent<MenuController>();
			MenuController mc = go.GetComponent<MenuController>();
			mc.ToggleMenu();
            bool expected = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Main";
            bool actual = mc.InMenu;
            Assert.AreEqual(expected, actual); 
        }

        [Test]
        public void InstructionsButtonPressTest()
        {
			GameObject go = new GameObject();
			go.AddComponent<MenuController>();
			MenuController mc = go.GetComponent<MenuController>();
			mc.InstructionsButton();
            bool actual = mc.InstructionsOpened;
            Assert.IsTrue(actual);
        }

        [Test]
        public void ExitButtonPressTest()
        {
			GameObject go = new GameObject();
			go.AddComponent<MenuController>();
			MenuController mc = go.GetComponent<MenuController>();
			mc.ExitGame();
            bool actual = mc.Exited;
            Assert.IsTrue(actual);
        }

		[Test]
		public void PlayAgainClicked()
		{
			GameObject go = new GameObject();
			go.AddComponent<MenuController>();
			MenuController mc = go.GetComponent<MenuController>();

			Assert.IsFalse(mc.PlayAgainClicked);
			mc.PlayAgain();
			Assert.IsTrue(mc.PlayAgainClicked);
		}
	}
}