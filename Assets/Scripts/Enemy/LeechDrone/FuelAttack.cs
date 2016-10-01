﻿using UnityEngine;
using System.Collections;

public class FuelAttack : MonoBehaviour {

	private LineRenderer _lineRenderer;
	private CharacterHealth _health;
	public Transform origin;
	public float damage;





	void OnTriggerStay(Collider other)
	{

		if (other.gameObject.tag == "Character")
		{	
			_health.TakeDamage (damage * Time.deltaTime);
			_lineRenderer.SetPosition (0, origin.position);
			_lineRenderer.GetComponent<Renderer> ().enabled = true;
			_lineRenderer.SetPosition (1, Character.current.transform.position);
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Character")
		{
			_lineRenderer.GetComponent<Renderer> ().enabled = false;
		}
	}






	// Use this for initialization
	void Start () 
	{
		_health = Character.current.GetComponent<CharacterHealth> ();
		_lineRenderer = GetComponent<LineRenderer> ();
		//
		_lineRenderer.SetWidth(0.45f, 0.45f);


	}

	// Update is called once per frame
	void Update () 
	{

	}
}
