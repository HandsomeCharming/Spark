﻿using UnityEngine;
using System.Collections;

public class CharacterHealth : Health {

	public override void TakeDamage(float amount)
	{
		GetComponent<FuelReservoir>().UseFuel(amount);
		if (amount > 1)
		{
			onTakeDamage.Invoke ();
		}
	}

	// Update is called once per frame
	void Update () {

	}
}