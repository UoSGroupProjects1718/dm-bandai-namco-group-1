using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public PuzzleSpawner PuzzleSpawner;
	public Timer Timer;
	public GameObject[] ActivePuzzles;

	void Start ()
	{
		PuzzleSpawner.RemoveActivePuzzles();
		PuzzleSpawner.SpawnPuzzle("Puzzle1");
	}

	public void Begin()
	{
		
	}

	public void SetActivePuzzles(GameObject[] puzzles)
	{
		ActivePuzzles = puzzles;
	}
}
