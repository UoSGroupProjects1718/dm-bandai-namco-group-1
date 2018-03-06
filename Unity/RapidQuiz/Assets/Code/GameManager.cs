using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public PuzzleSpawner PuzzleSpawner;
	public Timer Timer;
	public GameObject[] ActivePuzzles;
	private Puzzle _activePuzzleScript;

	public Text[] playerScores = new Text[2];

	public NumberMatch NumberMatchScript;

	void Start ()
	{
		PuzzleSpawner.RemoveActivePuzzles();
		SpawnColourMatchingPuzzle();
		Begin();
	}

	void Update()
	{
		_activePuzzleScript.Tick();
	}

	public void Begin()
	{
		_activePuzzleScript.Begin();
	}

	public void Stop()
	{
		_activePuzzleScript.End();
	}

	public void SetActivePuzzles(GameObject[] puzzles)
	{
		ActivePuzzles = puzzles;
	}

	public void SetActiveScript(Puzzle puzzle)
	{
		
	}

	private void SpawnColourMatchingPuzzle()
	{
		PuzzleSpawner.SpawnPuzzle("NumberMatch");
		_activePuzzleScript = NumberMatchScript.GetComponent<NumberMatch>();
		_activePuzzleScript.SetPlayer1PuzzleObject(ActivePuzzles[0]);
		_activePuzzleScript.SetPlayer2PuzzleObject(ActivePuzzles[1]);
		_activePuzzleScript.SetTimer();
	}

	public void IncreasePlayer1Score(int amount = 1)
	{
		playerScores[0].text = (int.Parse(playerScores[0].text) + amount).ToString();
	}
	
	public void IncreasePlayer2Score(int amount = 1)
	{
		playerScores[1].text = (int.Parse(playerScores[1].text) + amount).ToString();
	}
	
	public void DecreasePlayer1Score(int amount = 1)
	{
		playerScores[0].text = (int.Parse(playerScores[0].text) - amount).ToString();
	}
	
	public void DecreasePlayer2Score(int amount = 1)
	{
		playerScores[1].text = (int.Parse(playerScores[1].text) - amount).ToString();
	}
}
