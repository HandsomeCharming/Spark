﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class Fit : MonoBehaviour {

	public Transform pedestal;
	public bool active;
	public float speed;
	public UnityEvent onFit;
	public  List<Renderer>lights;
	private bool _fit = false;
	private Rigidbody _rigidBody;
	private Vector3 _firstTargetPos;
	private Vector3 _secondTargetPos;




	void Start()
	{
		_rigidBody = GetComponent<Rigidbody> ();
		_firstTargetPos = new Vector3 (pedestal.position.x, pedestal.position.y + 2.0f, pedestal.position.z);
		_secondTargetPos = new Vector3(pedestal.position.x, pedestal.position.y + 1.0f, pedestal.position.z);
	}




	void OnTriggerStay(Collider other)
	{
		
		if (!_fit)
		{
			if (other.gameObject.tag == "Pedestal" && !CloseEnoughXZ ())
			{
				Vector3 direction = _firstTargetPos - transform.position;
				_rigidBody.velocity = direction.normalized * speed * Time.deltaTime;
			} else if (other.gameObject.tag == "Pedestal")
			{
				Vector3 direction = _secondTargetPos - transform.position;
				_rigidBody.velocity = direction.normalized * speed * Time.deltaTime;
				if (isFit ())
				{
					_fit = true;
				}
			}
		} 
		else
		{
			transform.position = _secondTargetPos;
			onFit.Invoke ();
			foreach (Renderer light in lights)
			{
				if (light.material.color != Color.green)
				{
					light.material.color = Color.green;
				}
			}
		}




	}

	bool CloseEnoughXZ()
	{
		Vector2 pedestalXZ = new Vector2 (pedestal.position.x, pedestal.position.z);
		Vector2 currentXZ = new Vector2 (transform.position.x, transform.position.z);
		if (Vector2.Distance(pedestalXZ,currentXZ) < 0.02)
		{
			return true;
		}
		return false;

	}
	bool isFit()
	{
		if (Vector3.Distance(_secondTargetPos,transform.position) <0.01)
		{
			return true;
		}
		return false;
	}






}
