using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class OddOneOut : Puzzle
{
	private Button _player1Button, _player2Button;
	private GameObject _tileContainerPlayer1, _tileContainerPlayer2;
	public List<Sprite> Sprites = new List<Sprite>();
	private bool _switched;
	private int _startingSpriteIndex;

	[Range(1, 10f)]
	public float MaxWaitTime = 1f;
	[Range(1, 10f)] public float MinWaitTime = 10f;

	public override void Begin()
	{
		_player1Button = Player1Puzzle.transform.Find("Button").GetComponent<Button>();
		_player2Button = Player2Puzzle.transform.Find("Button").GetComponent<Button>();
		_tileContainerPlayer1 = Player1Puzzle.transform.Find("Tiles").gameObject;
		_tileContainerPlayer2 = Player2Puzzle.transform.Find("Tiles").gameObject;
		_player1Button.onClick.AddListener(RegisterPlayer1Button);
		_player2Button.onClick.AddListener(RegisterPlayer2Button);

		_startingSpriteIndex = Random.Range(0, Sprites.Count);
		var startingSprite = Sprites[_startingSpriteIndex];
		SetAllSprites(startingSprite, _tileContainerPlayer1);
		SetAllSprites(startingSprite, _tileContainerPlayer2);
		
		Timer.TimerComplete += TimerEnded;
		StartCoroutine(StartRandomisation());
		Running = true;
		Timer.Begin(15f);
	}

	private void TimerEnded(object sender, EventArgs e)
	{
		End();
	}

	private IEnumerator StartRandomisation()
	{
		var timeToWait = Random.Range(MinWaitTime, MaxWaitTime);
		yield return new WaitForSeconds(timeToWait);
		SwitchRandomSprite();
	}

//	private List<List<GameObject>> SpawnTiles(GameObject container, GameObject tilePrefab)
//	{
//		var output = new List<List<GameObject>>();
//		var containerWidth = container.GetComponent<RectTransform>().rect.width;
//		var tileWidth = tilePrefab.GetComponent<RectTransform>().rect.width;
//		var rightGap = (containerWidth - Width * tileWidth) / (Width - 1);
//		
//		for (var i = 0; i < Height; i++)
//		{
//			output.Add(new List<GameObject>());
//			for (var j = 0; j < Width; j++)
//			{
//				// For some reason doesn't move the instantiated gameobject's position?
//				output[i].Add(Instantiate(
//					tilePrefab,
//					new Vector3(i * 100, j * 100, 1),
//					Quaternion.identity,
//					container.transform)
//				);
//				
//				// Instead we just set the position here
//				
//				if (rightGap > 0)
//				{
//					output[i][j].transform.localPosition = j != 0 ?
//						new Vector3(j * 100 + rightGap * j, -i * 100, 1) :
//						new Vector3(j * 100, -i * 100, 1);
//				}
//				else
//				{
//					output[i][j].transform.localPosition =
//						new Vector3(j * 100, -i * 100, 1);
//				}
//			}
//		}
//
//		return output;
//	}

	private void SwitchRandomSprite()
	{
		int spriteIndex;
		do
		{
			spriteIndex = Random.Range(0, _tileContainerPlayer1.transform.childCount);
		} while (spriteIndex == _startingSpriteIndex);

		var newSprite = Sprites[Random.Range(0, Sprites.Count)];
		
		SetTileSprite(_tileContainerPlayer1.transform.GetChild(spriteIndex).gameObject, newSprite);
		SetTileSprite(_tileContainerPlayer2.transform.GetChild(spriteIndex).gameObject, newSprite);
		_switched = true;
	}

	private void SetAllSprites(Sprite sprite, GameObject container)
	{		
		foreach (Transform child in container.transform)
		{
			SetTileSprite(child.gameObject, sprite);
		}
	}

	private void SetTileSprite(GameObject tile, Sprite sprite)
	{
		var image = tile.GetComponent<Image>();
		if (image)
		{
			image.sprite = sprite;
		}
	}

	public override void Tick()
	{
		base.Tick();
	}

	public override void End()
	{
		StopAllCoroutines();
		Timer.TimerComplete -= TimerEnded;
		Debug.Log("OddOneOut Ended");
		Debug.Log("End of game!");
	}
	
	private void RegisterPlayer1Button()
	{
		if (_switched)
		{
			GameObject.Find("GameManager").GetComponent<GameManager>().IncreasePlayer1Score();
			End();
		}
		else
		{
			GameObject.Find("GameManager").GetComponent<GameManager>().DecreasePlayer1Score();
		}
	}


	private void RegisterPlayer2Button()
	{
		if (_switched)
		{
			GameObject.Find("GameManager").GetComponent<GameManager>().IncreasePlayer2Score();
			End();
		}
		else
		{
			Debug.Log("Wrong!");
			GameObject.Find("GameManager").GetComponent<GameManager>().DecreasePlayer2Score();
		}
	}
}
