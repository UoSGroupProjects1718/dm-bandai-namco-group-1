using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public PuzzleSpawner PuzzleSpawner;
	public Timer Timer;
	public GameObject CountDown;
	public GameObject[] ActivePuzzles;
	private Puzzle _activePuzzleScript;
	private Text _countdownText;

	public int Player1Score { get; private set; }
	public int Player2Score { get; private set; }
	public Text[] PlayerScores = new Text[2];

	public NumberMatch NumberMatchScript;
	public GridColours GridColoursScript;
	public OddOneOut OddOneOutScript;
	public ColourWord ColourWordScript;

	void Awake()
	{
		DontDestroyOnLoad(this);
	}

	void Start ()
	{
		_countdownText = CountDown.GetComponent<Text>();
		SpawnNextPuzzle(SpawnColourWordPuzzle);
	}

	void Update()
	{
		if (_activePuzzleScript)
		{
			_activePuzzleScript.Tick();
		}
	}

	public void Begin()
	{
		_activePuzzleScript.Begin();
	}


	public void SetActivePuzzles(GameObject[] puzzles)
	{
		ActivePuzzles = puzzles;
	}

	public void SetActiveScript(Puzzle puzzle)
	{
		
	}

	public void SpawnOddOneOutPuzzle()
	{
		Timer.gameObject.SetActive(true);
		PuzzleSpawner.SpawnPuzzle("OddOneOut");
		_activePuzzleScript = OddOneOutScript.GetComponent<OddOneOut>();
		_activePuzzleScript.SetPlayer1PuzzleObject(ActivePuzzles[0]);
		_activePuzzleScript.SetPlayer2PuzzleObject(ActivePuzzles[1]);
		_activePuzzleScript.SetTimer();
		Begin();
	}

	public void SpawnNumberMatchingPuzzle()
	{

		Timer.gameObject.SetActive(true);
		PuzzleSpawner.SpawnPuzzle("NumberMatch");
		_activePuzzleScript = NumberMatchScript.GetComponent<NumberMatch>();
		_activePuzzleScript.SetPlayer1PuzzleObject(ActivePuzzles[0]);
		_activePuzzleScript.SetPlayer2PuzzleObject(ActivePuzzles[1]);
		_activePuzzleScript.SetTimer();
		Begin();
	}


	public void SpawnGridColoursPuzzle()
	{
		Timer.gameObject.SetActive(true);
		PuzzleSpawner.SpawnPuzzle("GridColours");
		_activePuzzleScript = GridColoursScript.GetComponent<GridColours>();
		_activePuzzleScript.SetPlayer1PuzzleObject(ActivePuzzles[0]);
		_activePuzzleScript.SetPlayer2PuzzleObject(ActivePuzzles[1]);
		_activePuzzleScript.SetTimer();
		Begin();
	}
	
	public void SpawnColourWordPuzzle()
	{
		Timer.gameObject.SetActive(true);
		PuzzleSpawner.SpawnPuzzle("ColourWord");
		_activePuzzleScript = ColourWordScript.GetComponent<ColourWord>();
		_activePuzzleScript.SetPlayer1PuzzleObject(ActivePuzzles[0]);
		_activePuzzleScript.SetPlayer2PuzzleObject(ActivePuzzles[1]);
		_activePuzzleScript.SetTimer();
		Begin();
	}

	public void IncreasePlayer1Score(int amount = 1)
	{
		PlayerScores[0].text = (int.Parse(PlayerScores[0].text) + amount).ToString();
		Player1Score++;
	}
	
	public void IncreasePlayer2Score(int amount = 1)
	{
		PlayerScores[1].text = (int.Parse(PlayerScores[1].text) + amount).ToString();
		Player2Score++;
	}
	
	public void DecreasePlayer1Score(int amount = 1)
	{
		PlayerScores[0].text = (int.Parse(PlayerScores[0].text) - amount).ToString();
		Player1Score--;
	}
	
	public void DecreasePlayer2Score(int amount = 1)
	{
		PlayerScores[1].text = (int.Parse(PlayerScores[1].text) - amount).ToString();
		Player2Score--;
	}

	public void SpawnNextPuzzle(Action puzzle)
	{
		PuzzleSpawner.RemoveActivePuzzles();		
		Timer.gameObject.SetActive(false);
		StartCoroutine(Countdown(3, puzzle));
	}

	private IEnumerator Countdown(int seconds, Action puzzle)
	{
		CountDown.SetActive(true);
		var countdownTimer = (float)seconds;
		while (countdownTimer >= float.Epsilon)
		{
			countdownTimer -= Time.deltaTime;
			_countdownText.text = countdownTimer < 0.5f ? "Go!" : countdownTimer.ToString("F0");
			
			yield return new WaitForEndOfFrame();
		}
		CountDown.SetActive(false);
		puzzle.Invoke();
	}
}
