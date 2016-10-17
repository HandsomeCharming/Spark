﻿using UnityEngine;
using System.Collections;

public class IncreaseSpeedOnCollision : MonoBehaviour {

	private Rigidbody _rigidBody;
	public float force;

	void Start()
	{
		_rigidBody = Character.current.GetComponent<Rigidbody> ();
	}


	// Use this for initialization
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Character")
		{
            AudioManager.instance.Play("speedUpAura");
			Vector3 speedForce = (_rigidBody.velocity.normalized * force) ;
			_rigidBody.AddForce (speedForce, ForceMode.Force);
		}
	}
}
