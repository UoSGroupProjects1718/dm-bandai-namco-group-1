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

	public Text[] PlayerScores = new Text[2];

	public NumberMatch NumberMatchScript;
	public GridColours GridColoursScript;

	void Start ()
	{
		PuzzleSpawner.RemoveActivePuzzles();
		SpawnGridColoursPuzzle();
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

	private void SpawnGridColoursPuzzle()
	{
		PuzzleSpawner.SpawnPuzzle("GridColours");
		_activePuzzleScript = GridColoursScript.GetComponent<GridColours>();
		_activePuzzleScript.SetPlayer1PuzzleObject(ActivePuzzles[0]);
		_activePuzzleScript.SetPlayer2PuzzleObject(ActivePuzzles[1]);
		_activePuzzleScript.SetTimer();
	}

	public void IncreasePlayer1Score(int amount = 1)
	{
		PlayerScores[0].text = (int.Parse(PlayerScores[0].text) + amount).ToString();
	}
	
	public void IncreasePlayer2Score(int amount = 1)
	{
		PlayerScores[1].text = (int.Parse(PlayerScores[1].text) + amount).ToString();
	}
	
	public void DecreasePlayer1Score(int amount = 1)
	{
		PlayerScores[0].text = (int.Parse(PlayerScores[0].text) - amount).ToString();
	}
	
	public void DecreasePlayer2Score(int amount = 1)
	{
		PlayerScores[1].text = (int.Parse(PlayerScores[1].text) - amount).ToString();
	}
}
