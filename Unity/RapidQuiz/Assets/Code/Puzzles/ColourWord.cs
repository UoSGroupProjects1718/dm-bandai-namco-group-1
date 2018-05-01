using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ColourWord : Puzzle
{
	public Text ColourWordPlayer1;
	public Text ColourWordPlayer2;
	public Button MatchButtonPlayer1;
	public Button MatchButtonPlayer2;

	[Range(0, 1f)] public float MatchChance = 0.2f;
	[Range(0, 5f)] public float CycleInterval = 1.5f;
	
	
	private Dictionary<string, Color> _colourWords;
	private bool _matching;


	// Use this for initialization
	void Start () {
		PopulateColourDictionary(ref _colourWords);
		Gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	// Update is called once per frame
	void Update () {
		
	}

	public override void Begin()
	{
		MatchButtonPlayer1 = Player1Puzzle.transform.Find("Button").GetComponent<Button>();
		MatchButtonPlayer2 = Player2Puzzle.transform.Find("Button").GetComponent<Button>();
		ColourWordPlayer1 = Player1Puzzle.transform.Find("ColourWord").GetComponent<Text>();
		ColourWordPlayer2 = Player2Puzzle.transform.Find("ColourWord").GetComponent<Text>();
		MatchButtonPlayer1.onClick.AddListener(RegisterPlayer1Button);
		MatchButtonPlayer2.onClick.AddListener(RegisterPlayer2Button);
		Running = true;
		StartCoroutine(CycleWords());
		Timer.TimerComplete += TimerEnded;
		Timer.Begin(15f);
	}

	private void TimerEnded(object sender, EventArgs e)
	{
		End();
	}

	public override void Tick()
	{
		base.Tick();
	}
	
	public override void End()
	{
		Running = false;
		StopAllCoroutines();
		Timer.TimerComplete -= TimerEnded;
		Gm.SpawnNextPuzzle(Gm.SpawnGridColoursPuzzle);
	}

	public override void SetTimer()
	{
		base.SetTimer();
	}

	private void PopulateColourDictionary(ref Dictionary<string, Color> dictionary)
	{
		dictionary = new Dictionary<string, Color>
		{
			{"Black", Color.black},
			{"Blue", Color.blue},
			{"Cyan", Color.cyan},
			{"Grey", Color.grey},
			{"Green", Color.green},
			{"Magenta", Color.magenta},
			{"Red", Color.red},
			{"Yellow", Color.yellow}
		};
	}
	
	private void RegisterPlayer2Button()
	{
		if (_matching)
		{
			GameObject.Find("GameManager").GetComponent<GameManager>().IncreasePlayer1Score();
			StopAllCoroutines();
			StartCoroutine(CycleWords());
		}
		else
		{
			GameObject.Find("GameManager").GetComponent<GameManager>().DecreasePlayer1Score();
		}
	}

	private void RegisterPlayer1Button()
	{
		if (_matching)
		{
			GameObject.Find("GameManager").GetComponent<GameManager>().IncreasePlayer1Score();
			StopAllCoroutines();
			StartCoroutine(CycleWords());
		}
		else
		{
			GameObject.Find("GameManager").GetComponent<GameManager>().DecreasePlayer1Score();
		}
	}
	
	private IEnumerator CycleWords()
	{
		while (Running)
		{
			Debug.Log("Cycling Words");
			if (Random.Range(0, 1f) < MatchChance)
			{
				GenerateWords(true);
			}
			else
			{
				GenerateWords();
			}
			yield return new WaitForSeconds(CycleInterval);
		}
	}

	private void GenerateWords(bool matching = false)
	{
		if (matching)
		{
			GenerateMatch();
			_matching = true;
		}
		else
		{
			_matching = false;
			var words = _colourWords.Keys.ToList();
			var colours = _colourWords.Values.ToList();
			
			var word = words[Random.Range(0, words.Count)];
			Color colour;
			do
			{
				colour = colours[Random.Range(0, colours.Count)];
			} while (_colourWords[word] == colour);
			
			var word2 = words[Random.Range(0, words.Count)];
			Color colour2;
			do
			{
				colour2 = colours[Random.Range(0, colours.Count)];
			} while (_colourWords[word2] == colour2);
			
			
			ColourWordPlayer1.text = word;
			ColourWordPlayer1.color = colour;
			ColourWordPlayer2.text = word2;
			ColourWordPlayer2.color = colour2;
		}
	}	

	private void GenerateMatch()
	{
		var words = _colourWords.Keys.ToList();
		var word = words[Random.Range(0, words.Count)];
		var colour = _colourWords[word];
		ColourWordPlayer1.text = word;
		ColourWordPlayer1.color = colour;
		ColourWordPlayer2.text = word;
		ColourWordPlayer2.color = colour;
	}
}
