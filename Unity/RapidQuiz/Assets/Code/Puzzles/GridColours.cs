using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GridColours : Puzzle
{

	private GameObject Player1Grid, Player2Grid;
	private Button Player1Button, Player2Button;
	private bool matchingGrid = false;

	// Use this for initialization
	public override void Begin()
	{
		SetupGameobjectData();
		Player1Button.onClick.AddListener(RegisterPlayer1Match);
		Player2Button.onClick.AddListener(RegisterPlayer2Match);
	
		running = true;
		Timer.TimerComplete += TimerEnded;
		Timer.Begin(15f);

		StartCoroutine(RandomiseGrid());
	}

	private IEnumerator RandomiseGrid()
	{
		while (running)
		{
			GenerateGrid();
			yield return new WaitForSeconds(1);
		}
	}	

	private void GenerateGrid()
	{
		var colours = GenerateColourEnumerable(Player1Grid.transform.childCount);
		var rng = Random.Range(0f, 1f);
		// Generate matching grid
		if (rng < 0.5f)
		{
			var colour = colours[Random.Range(0, colours.Length)];
			foreach (Transform tileTransform in Player1Grid.transform)
			{
				tileTransform.gameObject.GetComponent<Image>().color = colour;
			}
			foreach (Transform tileTransform in Player2Grid.transform)
			{
				tileTransform.gameObject.GetComponent<Image>().color = colour;
			}

			matchingGrid = true;
		}
		else
		{
			for (var i = 0; i < Player1Grid.transform.childCount; i++)
			{
				var tiletransform = Player1Grid.transform.GetChild(i);
				tiletransform.gameObject.GetComponent<Image>().color = colours[i];
			}
			for (var i = 0; i < Player2Grid.transform.childCount; i++)
			{
				var tiletransform = Player2Grid.transform.GetChild(i);
				tiletransform.gameObject.GetComponent<Image>().color = colours[i];
			}

			matchingGrid = false;
		}
	}

	private Color[] GenerateColourEnumerable(int size)
	{
		var colours = new Color[size];
		for (var i = 0; i < colours.Length; i++)
		{
			colours[i] = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
		}

		return colours;
	}

	public override void Tick()
	{
		throw new System.NotImplementedException();
	}

	public override void End()
	{
		running = false;
		StopAllCoroutines();
	}
	
	private void SetupGameobjectData()
	{
		Player1Grid = Player1Puzzle.transform.GetChild(0).gameObject;
		Player2Grid = Player2Puzzle.transform.GetChild(0).gameObject;
		Player1Button = Player1Puzzle.transform.Find("Button").GetComponent<Button>();
		Player2Button = Player2Puzzle.transform.Find("Button").GetComponent<Button>();
	}
	
	private void TimerEnded(object sender, System.EventArgs e)
	{
		Debug.Log("Timer Ended");
		End();
	}
	
	public void RegisterPlayer1Match()
	{
		if (matchingGrid)
		{
			Debug.Log("Right!");
			GameObject.Find("GameManager").GetComponent<GameManager>().IncreasePlayer1Score();
			StopAllCoroutines();
			StartCoroutine(RandomiseGrid());

		}
		else
		{
			Debug.Log("Wrong!");
			GameObject.Find("GameManager").GetComponent<GameManager>().DecreasePlayer1Score();
		}
	}


	public void RegisterPlayer2Match()
	{
		if (matchingGrid)
		{
			Debug.Log("Right!");
			GameObject.Find("GameManager").GetComponent<GameManager>().IncreasePlayer2Score();
			StopAllCoroutines();
			StartCoroutine(RandomiseGrid());
		}
		else
		{
			Debug.Log("Wrong!");
			GameObject.Find("GameManager").GetComponent<GameManager>().DecreasePlayer2Score();
		}
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
