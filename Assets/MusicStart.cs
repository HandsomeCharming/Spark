﻿using UnityEngine;
using System.Collections;

public class MusicStart : MonoBehaviour {

	// Use this for initialization
	void Awake () 
	{
		AudioManager.instance.Play ("standardLevel");
	}
}
