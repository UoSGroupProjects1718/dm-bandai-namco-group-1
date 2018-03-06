using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Puzzle : MonoBehaviour
{
	protected GameManager gm;
	protected Timer Timer;
	protected GameObject Player1Puzzle;
	protected GameObject Player2Puzzle;
	public bool running = false;
	
	public abstract void Begin();
	public abstract void Tick();
	public abstract void End();

	private void Start()
	{
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
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
