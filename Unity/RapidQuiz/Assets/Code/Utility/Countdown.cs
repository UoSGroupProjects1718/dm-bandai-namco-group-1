﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void WaitForAnimation(Animation anim)
	{
		while (true)
		{
			if (!anim.isPlaying) return;
		}
	}
}