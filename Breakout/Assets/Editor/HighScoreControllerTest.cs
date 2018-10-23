using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreControllerTest
{
	[Test]
	public void NameValidation()
	{
		GameObject go = new GameObject();
		go.AddComponent<HighScoreController>();
		HighScoreController sc = go.GetComponent<HighScoreController>();
		Assert.IsTrue(sc.IsValidName("AAA"));
		Assert.IsFalse(sc.IsValidName("AA"));
		Assert.IsFalse(sc.IsValidName("123"));
		Assert.IsFalse(sc.IsValidName("aaa"));
		Assert.IsFalse(sc.IsValidName("!@#"));
	}

	[Test]
	public void JSONConversion()
	{
		GameObject go = new GameObject();
		go.AddComponent<HighScoreController>();
		HighScoreController sc = go.GetComponent<HighScoreController>();
		Assert.AreEqual(sc.ScoresFromJSON("{ \"scores\": [] }").Count, 0);
		Assert.AreEqual(sc.ScoresFromJSON("{ \"scores\": [ {\"name\": \"AAA\", \"score\": 10 } ] }").Count, 1);
		Assert.Throws<System.ArgumentException>(() => sc.ScoresFromJSON("{ \"scores\": [ {\"name\": \"AAA\", } ] }")); // Check for malformed JSON
	}

	[Test]
	public void ScoresReceived()
	{
		GameObject go = new GameObject();
		go.AddComponent<HighScoreController>();
		HighScoreController sc = go.GetComponent<HighScoreController>();
		sc.ScoresPanel = new GameObject();
		GameObject templateText = new GameObject();
		templateText.AddComponent<Text>();
		templateText.transform.SetParent(sc.ScoresPanel.transform);
		sc.Initialize();

		List<HighScoreController.ScoreEntry> scores = new List<HighScoreController.ScoreEntry>();
		var entry1 = new HighScoreController.ScoreEntry();
		entry1.name = "AAA";
		entry1.score = 10;

		var entry2 = new HighScoreController.ScoreEntry();
		entry2.name = "BBB";
		entry2.score = 20;

		scores.Add(entry1);
		scores.Add(entry2);

		sc.PopulateHighScores(scores);

		Assert.AreEqual("1. " + entry1.name + " [" + entry1.score + "]", sc.ScoreText[0].text);
		Assert.IsTrue(sc.ScoreText[0].enabled);
		Assert.AreEqual("2. " + entry2.name + " [" + entry2.score + "]", sc.ScoreText[1].text);
		Assert.IsTrue(sc.ScoreText[1].enabled);
		Assert.AreEqual("3.", sc.ScoreText[2].text);
		Assert.IsFalse(sc.ScoreText[2].enabled);
	}

	[Test]
	public void ScoreSent()
	{
		GameObject go = new GameObject();
		go.AddComponent<HighScoreController>();
		go.AddComponent<InputField>();
		HighScoreController sc = go.GetComponent<HighScoreController>();
		InputField nameField = go.GetComponent<InputField>();
		sc.NameField = nameField;
		nameField.text = "AAA";
		sc.Score = 10;
		sc.SendHighScore();
		Assert.IsTrue(sc.SuccessfullySentScore);

		nameField.text = "AA";
		sc.Score = 10;
		sc.SendHighScore();
		Assert.IsFalse(sc.SuccessfullySentScore);

		nameField.text = "AAA";
		sc.Score = 0;
		sc.SendHighScore();
		Assert.IsFalse(sc.SuccessfullySentScore);

		nameField.text = "AA";
		sc.Score = 0;
		sc.SendHighScore();
		Assert.IsFalse(sc.SuccessfullySentScore);
	}
}
