using UnityEngine;
using UnityEngine.UI;

public class ScoreScreenManager : MonoBehaviour
{
	public Text WinnerText, ScoreText;
	private GameManager _gm;
	void Start()
	{
		_gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		SetWinnerText();
		SetScoreText();
	}

	private void SetWinnerText()
	{
		if (_gm.Player1Score > _gm.Player2Score)
		{
			WinnerText.text = "Player 1 Wins!";
		}
		else if (_gm.Player2Score > _gm.Player1Score)
		{
			WinnerText.text = "Player 2 Wins!";
		}
		else
		{
			WinnerText.text = "It's a draw!";
		}
	}
	
	private void SetScoreText()
	{
		ScoreText.text = string.Format("Player 1: {0}\nPlayer 2: {1}", _gm.Player1Score, _gm.Player2Score);
		
	}
}
