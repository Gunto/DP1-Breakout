using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


/// <summary>
/// 
/// A Unity component that tracks the scores of the current game, fetches the scores from
/// the high-score server, and validates and sends new scores to the high-score server.
/// 
/// Resources used in the creation of this script:
/// -> https://docs.unity3d.com/ScriptReference/Networking.UnityWebRequest.Get.html
/// -> https://docs.unity3d.com/Manual/UnityWebRequest-SendingForm.html
/// -> https://docs.unity3d.com/Manual/JSONSerialization.html
/// </summary>
public class HighScoreController : MonoBehaviour
{
	private int _score = 0;
	private string _name;
	private bool _gameOverTriggered = false;

	public UIController UIController;
	public List<Text> ScoreText;
	public GameObject Menu;
	public GameObject ScoresPanel;
	public InputField NameField;
	public List<ScoreEntry> RetrievedScores;
	public Text TextFailed;
	public Text TextLoading;
	public Text TextSend;
	public Text TextScore;
	public bool SuccessfullySentScore;

	/// <summary>
	/// Struct for storing an array of scores; used for parsing JSON
	/// </summary>
	[Serializable]
	public struct Scores
	{
		public List<ScoreEntry> scores;
	}

	/// <summary>
	/// Struct for storing individual scores; used for parsing JSON
	/// </summary>
	[Serializable]
	public struct ScoreEntry
	{
		public string name;
		public int score;
	}

	/// <summary>
	/// Inbuilt Unity function; used to call the Initialize method
	/// </summary>
	public void Start()
	{
		Initialize();
	}

	/// <summary>
	/// Initializes the menu when the game is started and initialies values (effectively the constructor of the MonoBehaviour)
	/// </summary>
	public void Initialize()
	{
		ScoreText = new List<Text>();
		RetrievedScores = new List<ScoreEntry>();
		GameObject scoreEntryText = ScoresPanel.transform.GetChild(0).gameObject;
		ScoreText.Add(scoreEntryText.GetComponent<Text>());
		for (int i = 1; i < 10; i++)
		{
			GameObject newEntry = Instantiate(scoreEntryText);
			newEntry.GetComponent<Text>().text = (i + 1) + ".";
			newEntry.GetComponent<Text>().enabled = false;
			newEntry.name = "Score" + (i + 1);
			Vector2 previousPos = newEntry.transform.GetComponent<RectTransform>().anchoredPosition;
			previousPos.y -= 25 * i;
			newEntry.transform.SetParent(ScoresPanel.transform);
			newEntry.transform.GetComponent<RectTransform>().anchoredPosition = previousPos;
			newEntry.transform.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
			ScoreText.Add(newEntry.GetComponent<Text>());
		}
	}

	/// <summary>
	/// Ensures the entered name matches the pattern of three uppercase alphabetic characters
	/// </summary>
	/// <param name="name">The name to validate</param>
	/// <returns>Whether the name matches the correct pattern</returns>
	public bool IsValidName(string name)
	{
		Regex regex = new Regex("[A-Z]{3}");
		return regex.IsMatch(name);
	}

	/// <summary>
	/// Is called when the game ends (the player loses) and opens the 'GAME OVER' menu
	/// </summary>
	public void GameOver()
	{
		if (!_gameOverTriggered)
		{
			_score = UIController.score;
			TextScore.text = "Score: " + _score;
			Menu.SetActive(true);
			TextSend.enabled = false;
			RetrievedScores.Clear();
			foreach (Text text in ScoreText)
				text.enabled = false;

			StartCoroutine(FetchHighScores());
		}
		_gameOverTriggered = true;
	}

	/// <summary>
	/// Initiates the sending of the name and score to the server (activated with button press)
	/// </summary>
	public void SendHighScore()
	{
		string name = NameField.text;
		if (_score > 0 && IsValidName(name))
		{
			_name = name;
			if (!Application.isConsolePlatform)
				StartCoroutine(PostHighScore());
			SuccessfullySentScore = true;
			return;
		}
		SuccessfullySentScore = false;
	}

	/// <summary>
	/// Creates a Coroutine that sends the score of the game just played to the server, with the associated name
	/// </summary>
	/// <returns></returns>
	IEnumerator PostHighScore()
	{
		WWWForm form = new WWWForm();
		form.AddField("name", _name);
		form.AddField("score", _score);

		UnityWebRequest www = UnityWebRequest.Post("http://139.99.173.44:11000/", form);

		yield return www.Send();

		NameField.interactable = false;

		if (www.isNetworkError || www.isHttpError)
		{
			Debug.Log(www.error);
			TextFailed.enabled = true;
		}
		else
		{
			Debug.Log(www.downloadHandler.text);
			TextSend.enabled = true;

			StartCoroutine(FetchHighScores());
		}
	}

	/// <summary>
	/// Creates a Coroutine that loads all scores from the server and displays them on the 'GAME OVER' screen
	/// </summary>
	/// <returns></returns>
	IEnumerator FetchHighScores()
	{
		using (UnityWebRequest www = UnityWebRequest.Get("http://139.99.173.44:11000/"))
		{
			yield return www.Send();

			TextLoading.enabled = false;

			if (www.isNetworkError || www.isHttpError)
			{
				Debug.Log(www.error);
				TextFailed.enabled = true;
				NameField.interactable = false;
			}
			else
			{
				string data = www.downloadHandler.text;

				RetrievedScores = ScoresFromJSON("{ \"scores\": " + data + " }");

				if (RetrievedScores.Count <= 0)
				{
					ScoreText[0].text = "There are no high-scores!";
					ScoreText[0].enabled = true;
				}
				else
				{
					PopulateHighScores(RetrievedScores);
				}
			}
		}
	}

	/// <summary>
	/// Updates the high-score UI with the new names
	/// </summary>
	public void PopulateHighScores(List<ScoreEntry> scores)
	{
		for (int i = 0; i < scores.Count; i++)
		{
			ScoreText[i].text = (i + 1) + ". " + scores[i].name + " [" + scores[i].score + "]";
			ScoreText[i].enabled = true;
		}
	}

	/// <summary>
	/// Converts the server's JSON response to a list of score entries
	/// </summary>
	/// <param name="json">Raw JSON returned froms server, containing score entries</param>
	/// <returns>A parsed List of ScoreEntry structs</returns>
	public List<ScoreEntry> ScoresFromJSON(string json)
	{
		return JsonUtility.FromJson<Scores>(json).scores;
	}

	/// <summary>
	/// The score to be sent to the server
	/// </summary>
	public int Score
	{
		get { return _score; }
		set { _score = value; }
	}
}
