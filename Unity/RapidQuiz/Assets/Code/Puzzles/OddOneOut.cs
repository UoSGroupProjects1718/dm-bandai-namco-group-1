using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OddOneOut : Puzzle
{
	public GameObject TilePrefab;
	private GameObject _tileContainerPlayer1, _tileContainerPlayer2;
	public List<Sprite> Sprites = new List<Sprite>();
	private List<List<GameObject>> _tilesPlayer1 = new List<List<GameObject>>();
	private	List<List<GameObject>> _tilesPlayer2 = new List<List<GameObject>>();
	public int Height = 2;
	public int Width = 5;

	public override void Begin()
	{
		_tileContainerPlayer1 = Player1Puzzle.transform.Find("Tiles").gameObject;
		
		for (var i = 0; i < Height; i++)
		{
			_tilesPlayer1.Add(new List<GameObject>());
			for (var j = 0; j < Width; j++)
			{
				_tilesPlayer1[i].Add(Instantiate(TilePrefab,
					new Vector2(
						i * TilePrefab.GetComponent<RectTransform>().rect.width,
						j * TilePrefab.GetComponent<RectTransform>().rect.height
					), Quaternion.identity, _tileContainerPlayer1.transform));
			}
		}
		
		SetAllPlayer1Sprites(Sprites[Random.Range(0, Sprites.Count)]);
	}

	private void SetAllPlayer1Sprites(Sprite sprite)
	{		
		foreach (var tileList in _tilesPlayer1)
		{
			foreach (var tile in tileList)
			{
				SetTileSprite(tile, sprite);
			}
		}
	}

	private void SetTileSprite(GameObject tile, Sprite sprite)
	{
		var spriteRenderer = tile.GetComponent<SpriteRenderer>();
		if (spriteRenderer)
		{
			tile.GetComponent<SpriteRenderer>().sprite = sprite;
		}
	}

	public override void Tick()
	{
		base.Tick();
	}

	public override void End()
	{
		base.End();
	}
}
