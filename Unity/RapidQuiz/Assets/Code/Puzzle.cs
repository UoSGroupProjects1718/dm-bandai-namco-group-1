using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
	protected GameManager Gm;
	protected Timer Timer;
	protected GameObject Player1Puzzle;
	protected GameObject Player2Puzzle;
	public bool Running = false;
	
	public virtual void Begin() {}
	public virtual void Tick() {}
	public virtual void End() {}

	private void Start()
	{
		Gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public virtual void SetTimer()
	{
		Timer = GameObject.Find("Timer").GetComponent<Timer>();
	}

	public void SetPlayer1PuzzleObject(GameObject player1Puzzle)
	{
		Player1Puzzle = player1Puzzle;
	}
	
	public void SetPlayer2PuzzleObject(GameObject player2Puzzle)
	{
		Player2Puzzle = player2Puzzle;
	}
}
