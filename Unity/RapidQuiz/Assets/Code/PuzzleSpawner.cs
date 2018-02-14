using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleSpawner : MonoBehaviour
{
	public GameObject Puzzle1Area, Puzzle2Area;
	public GameObject[] Puzzles;
	public GameManager GameManager;

	public void SpawnPuzzle(int puzzleIndex)
	{
		var puzzlesToSpawn = Puzzles.SkipWhile(puzzle =>
			!puzzle.name.Contains("Puzzle" + puzzleIndex.ToString())).ToArray();
		if (puzzlesToSpawn.Count() == 2)
		{
			Instantiate(puzzlesToSpawn[0], Puzzle1Area.transform);
			Instantiate(puzzlesToSpawn[1], Puzzle2Area.transform);
		}
		else
		{
			Debug.LogWarning("Only found " + puzzlesToSpawn.Count() + " puzzles!"); 
		}
	}

	public void SpawnPuzzle(string name)
	{
		var puzzlesToSpawn = Puzzles.Where(puzzle => puzzle.name.Contains(name)).ToArray();
		if (puzzlesToSpawn.Length >= 2)
		{
			Instantiate(puzzlesToSpawn[0], Puzzle1Area.transform);
			Instantiate(puzzlesToSpawn[1], Puzzle2Area.transform);
		}
		else
		{
			Debug.LogWarning("Only found " + puzzlesToSpawn.Count() + " puzzles!"); 
		}
	}

	public void RemoveActivePuzzles()
	{
		DestroyPuzzlesInPuzzleArea(Puzzle1Area);
		DestroyPuzzlesInPuzzleArea(Puzzle2Area);
	}

	private void DestroyPuzzlesInPuzzleArea(GameObject puzzleArea)
	{
		foreach (Transform puzzle in puzzleArea.transform)
		{
			Destroy(puzzle.gameObject);
		}
	}

	private void SetActivePuzzles(params GameObject[] puzzles)
	{
		foreach (var puzzle in puzzles)
		{
			GameManager.SetActivePuzzles(puzzles);
		}
	}
}
