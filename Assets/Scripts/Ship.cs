﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour {

    public GameObject BulletPrefab;
    public int AmmoCount = 10;
	public Text AmmoText;
	public Text FuelText;

    Rigidbody _rigidBody;

    public Vector3 Velocity
    {
        get
        {
            return _rigidBody.velocity;
        }
        set
        {
            _rigidBody.velocity = value;
        }
    }
		
    public float Mass
    {
        get
        {
            return _rigidBody.mass;
        }
    }

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();

    }

	void addBullet()
    {
        AmmoCount+=1;

    }
	void Update()
	{
		//Cheat code
		if (Input.GetKeyDown (KeyCode.B))
		{
			if (AmmoCount >= 300)
			{
				AmmoCount = 10;
				AmmoText.text = "Total Fuel(Cheat Disabled) : " + AmmoCount;
				AmmoText.color = new Color(1.0f,0.6f,0.0f);
			} else
			{
				AmmoCount = 5550;
				AmmoText.text = "Total Fuel(Cheat Enabled) : " + AmmoCount;
				AmmoText.color = new Color(1.0f,0.6f,0.0f);
			}
			Invoke ("removeAmmoText", 1.0f);
		}
		if (AmmoCount <= 0)
		{
			AmmoText.text = "You are out of Fuel!";
			AmmoText.color = Color.red;
		}
        else
        {
        }


	}

    public GameObject GetBullet()
    {
		
		if (AmmoCount <= 0)
		{
			return null;
		}
        var bullet = Instantiate(BulletPrefab);
        AmmoCount--;
        Destroy(bullet, 5.0f);

        return bullet;
    }

	public void GetAmmo(int amount)
	{
		AmmoCount += amount;
		AmmoText.text = "Total Fuel : " + AmmoCount;
		AmmoText.color = new Color(1.0f,0.6f,0.0f);
		Invoke ("removeAmmoText", 1.0f);


	}


	void removeAmmoText()
	{
		AmmoText.text = "";
	}
}
