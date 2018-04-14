using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Utility
{
	public static class Colours {
		public static Color[] GenerateRandomColourEnumerable(int size)
		{
			var colours = new Color[size];
			for (var i = 0; i < size; i++)
			{
				colours[i] = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
			}

			return colours;
		}
	}

}
