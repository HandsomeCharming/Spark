﻿using UnityEngine;
using System.Collections;

public class MoveForward : MonoBehaviour {

    public float speed;

    public bool doMove = false;

    private Transform _transform;

    void Awake()
    {
        _transform = GetComponent<Transform>();
    }

	
	// Update is called once per frame
	void Update () 
	{
        if (!doMove) return;
        var step = speed * Time.deltaTime;
        _transform.Translate(_transform.forward * step);
	}

	public void StartMoving()
	{
		doMove = true;
	}
	public void StopMoving()
	{
		doMove = false;
	}
}
