using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class GridColours : Puzzle
{
    private GameObject _player1Grid, _player2Grid;
    private Button _player1Button, _player2Button;
    private bool _matchingGrid;

    [Range(0.5f, 5f)] public float SecondsBetweenGridSwitch = 2f;

    [Range(0, 1f)] public float MatchChance = 0.5f;

    // Use this for initialization
    public override void Begin()
    {
        SetupGameobjectData();
        _player1Button.onClick.AddListener(RegisterPlayer1Match);
        _player2Button.onClick.AddListener(RegisterPlayer2Match);

        Running = true;
        Timer.TimerComplete += TimerEnded;
        Timer.Begin(15f);

        StartCoroutine(RandomiseGrid());
    }


    private IEnumerator RandomiseGrid()
    {
        while (Running)
        {
            GenerateGrids();
            yield return new WaitForSeconds(SecondsBetweenGridSwitch);
        }
    }

    private void GenerateGrids()
    {
        var matching = Random.Range(0f, 1f) < MatchChance;
        
        if (matching)
            GenerateMatchingGrid();
        else
            GenerateUnlikeGrids();
    }

    private void GenerateUnlikeGrids()
    {
        var colours = Colours.GenerateRandomColourEnumerable(_player1Grid.transform.childCount);
        ApplyColoursToGrid(_player1Grid, colours);
        colours = Colours.GenerateRandomColourEnumerable(_player1Grid.transform.childCount);
        ApplyColoursToGrid(_player2Grid, colours);
        _matchingGrid = false;
    }

    private void GenerateMatchingGrid()
    {
        var colours = Colours.GenerateRandomColourEnumerable(_player1Grid.transform.childCount);
        ApplyColoursToGrid(_player1Grid, colours);
        ApplyColoursToGrid(_player2Grid, colours);
        _matchingGrid = true;
    }

    private void ApplyColoursToGrid(GameObject grid, IList<Color> colours)
    {
        for (var i = 0; i < grid.transform.childCount; i++)
        {
            ApplyColourToTile(grid.transform.GetChild(i), colours[i]);
        }
    }    

    private void ApplyColourToTile(Transform tile, Color colour)
    {
        tile.gameObject.GetComponent<Image>().color = colour;
    }

    
    public override void End()
    {
        Running = false;
        StopAllCoroutines();
    }

    private void SetupGameobjectData()
    {
        _player1Grid = Player1Puzzle.transform.GetChild(0).gameObject;
        _player2Grid = Player2Puzzle.transform.GetChild(0).gameObject;
        _player1Button = Player1Puzzle.transform.Find("Button").GetComponent<Button>();
        _player2Button = Player2Puzzle.transform.Find("Button").GetComponent<Button>();
    }

    private void TimerEnded(object sender, System.EventArgs e)
    {
        Debug.Log("Timer Ended");
        End();
    }

    private void RegisterPlayer1Match()
    {
        if (_matchingGrid)
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


    private void RegisterPlayer2Match()
    {
        if (_matchingGrid)
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
}