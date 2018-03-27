using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Collections;
using UnityEngine.UI;

public class NumberMatch : Puzzle
{
	private const int MaxNumber = 100;
	
	private Dictionary<int, string> _dictionary;
	private Text Player1Number, Player1NumberString;
	private Text Player2Number, Player2NumberString;
	private Button Player1Button, Player2Button;
	
	public System.Random rng = new System.Random();
	

	// Use this for initialization
	void Start () {
		_dictionary = new Dictionary<int, string>();

		for (int i = 1; i <= MaxNumber; i++)
		{
			_dictionary.Add(i, HumanFriendlyInteger.IntegerToWritten(i));	
		}
	}
	
	public override void Begin()
	{
		SetupGameobjectData();
		Player1Button.onClick.AddListener(RegisterPlayer1Match);
		Player2Button.onClick.AddListener(RegisterPlayer2Match);
	
		running = true;
		Timer.TimerComplete += TimerEnded;
		Timer.Begin(15f);

		StartCoroutine(WordCycle());

	}
	
	// Update is called once per frame
	public override void Tick ()
	{
		
	}
	
	public override void End()
	{
		running = false;
		StopAllCoroutines();
	}

	

	private void SetupGameobjectData()
	{
		Player1Number = Player1Puzzle.transform.Find("Number").GetComponent<Text>();
		Player2Number = Player2Puzzle.transform.Find("Number").GetComponent<Text>();
		Player1NumberString = Player1Puzzle.transform.Find("NumberString").GetComponent<Text>();
		Player2NumberString = Player2Puzzle.transform.Find("NumberString").GetComponent<Text>();
		Player1Button = Player1Puzzle.transform.Find("MatchButton").GetComponent<Button>();
		Player2Button = Player2Puzzle.transform.Find("MatchButton").GetComponent<Button>();
	}

	private void TimerEnded(object sender, EventArgs e)
	{
		Debug.Log("Timer Ended");
		End();
	}
	
	

	private void SetPlayerNumberTexts(int number, string literalNumber)
	{
		RandomiseNumberTextColours();
		Player1Number.text = number.ToString("F0");
		Player2Number.text = number.ToString("F0");
		Player1NumberString.text = literalNumber;
		Player2NumberString.text = literalNumber;
	}

	private void RandomiseNumberTextColours()
	{
		Player1Number.color = new Color(
			UnityEngine.Random.Range(0f, 1f),
			UnityEngine.Random.Range(0f, 1f),
			UnityEngine.Random.Range(0f, 1f)
		);
		Player2Number.color = new Color(
			UnityEngine.Random.Range(0f, 1f),
			UnityEngine.Random.Range(0f, 1f),
			UnityEngine.Random.Range(0f, 1f)
		);
		Player1NumberString.color = new Color(
			UnityEngine.Random.Range(0f, 1f),
			UnityEngine.Random.Range(0f, 1f),
			UnityEngine.Random.Range(0f, 1f)
		);
		Player2NumberString.color = new Color(
			UnityEngine.Random.Range(0f, 1f),
			UnityEngine.Random.Range(0f, 1f),
			UnityEngine.Random.Range(0f, 1f)
		);
	}

	private void SetPlayerNumberText(bool player1, int number, string literalNumber)
	{
		if (player1)
		{
			Player1Number.text = number.ToString();
			Player1NumberString.text = literalNumber;
		}
		else
		{
			Player2Number.text = number.ToString();
			Player2NumberString.text = literalNumber;
		}
	}

	private IEnumerator WordCycle()
	{
		while (running)
		{
			GenerateMatch();
			yield return new WaitForSeconds(2);
		}
	}

	private void GenerateMatch()
	{
		var gen = rng.Next(1,3); // Either 1 or 2
		if (gen == 1)
		{
			var roll = rng.Next(1, MaxNumber+1);
			SetPlayerNumberTexts(roll, _dictionary[roll]);
		}
		else
		{
			var roll = rng.Next(1, MaxNumber+1);
			SetPlayerNumberTexts(rng.Next(1, MaxNumber+1), _dictionary[roll]);
		}
	}

	private bool CheckMatch(int num, string literalNum)
	{
		if (HumanFriendlyInteger.IntegerToWritten(num) == literalNum)
		{
			return true;
		}

		return false;
	}

	public void RegisterPlayer1Match()
	{
		if (CheckMatch(int.Parse(Player1Number.text), Player1NumberString.text))
		{
			Debug.Log("Right!");
			GameObject.Find("GameManager").GetComponent<GameManager>().IncreasePlayer1Score();
			StopAllCoroutines();
			StartCoroutine(WordCycle());

		}
		else
		{
			Debug.Log("Wrong!");
			GameObject.Find("GameManager").GetComponent<GameManager>().DecreasePlayer1Score();
		}
	}
	
	public void RegisterPlayer2Match()
	{
		if (CheckMatch(int.Parse(Player2Number.text), Player2NumberString.text))
		{
			Debug.Log("Right!");
			GameObject.Find("GameManager").GetComponent<GameManager>().IncreasePlayer2Score();
			StopAllCoroutines();
			StartCoroutine(WordCycle());
		}
		else
		{
			Debug.Log("Wrong!");
			GameObject.Find("GameManager").GetComponent<GameManager>().DecreasePlayer2Score();
		}
	}
	
	
}

public static class HumanFriendlyInteger
{
	private static readonly string[] Ones = new string[] { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
	private static readonly string[] Teens = new string[] { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
	private static readonly string[] Tens = new string[] { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
	private static readonly string[] ThousandsGroups = { "", " Thousand", " Million", " Billion" };

	private static string FriendlyInteger(int n, string leftDigits, int thousands)
	{
		if (n == 0)
		{
			return leftDigits;
		}

		string friendlyInt = leftDigits;

		if (friendlyInt.Length > 0)
		{
			friendlyInt += " ";
		}

		if (n < 10)
		{
			friendlyInt += Ones[n];
		}
		else if (n < 20)
		{
			friendlyInt += Teens[n - 10];
		}
		else if (n < 100)
		{
			friendlyInt += FriendlyInteger(n % 10, Tens[n / 10 - 2], 0);
		}
		else if (n < 1000)
		{
			friendlyInt += FriendlyInteger(n % 100, (Ones[n / 100] + " Hundred"), 0);
		}
		else
		{
			friendlyInt += FriendlyInteger(n % 1000, FriendlyInteger(n / 1000, "", thousands+1), 0);
			if (n % 1000 == 0)
			{
				return friendlyInt;
			}
		}

		return friendlyInt + ThousandsGroups[thousands];
	}

	public static string IntegerToWritten(int n)
	{
		if (n == 0)
		{
			return "Zero";
		}
		else if (n < 0)
		{
			return "Negative " + IntegerToWritten(-n);
		}

		return FriendlyInteger(n, "", 0);
	}
}
