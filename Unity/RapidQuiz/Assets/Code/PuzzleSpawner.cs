using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleSpawner : MonoBehaviour
{
	public GameObject Puzzle1Area, Puzzle2Area;
	public GameObject[] Puzzles;

	// Use this for initialization
	void Start ()
	{
		SpawnPuzzle(1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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
}
