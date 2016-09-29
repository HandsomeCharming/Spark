﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Collections;

public class Character : MonoBehaviour 
{
//    public GameObject primaryFireProjectilePrefab;
//    public GameObject secondaryFireProjectilePrefab;
	public GameObject blackHoleExplosionPrefab;
    public GameObject outOfFuelPrefab;
//	public ParticleSystem ramEffect;
	public AudioClip [] soundEffects;
	public Text fuelDepletedText;
	public Text fuelCountText;


    private int _charDeathDelay;
    private int _fuelChange;
	public int maxVelocity;
    private Rigidbody _rigidBody;
	private FuelReservoir _fuelReservoir;
    private bool _cheatMode;
    private bool _alertSound;
    private bool _dyingCountdown = false;
    private Coroutine _runningCoroutine = null;

    public static Character current;

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
        current = this;
        _charDeathDelay = 5;
        _fuelChange = 0;
//		ramEffect.Stop ();
		maxVelocity = 10;
        _rigidBody = GetComponent<Rigidbody>();
        _fuelReservoir = GetComponent<FuelReservoir>();
        _alertSound = false;
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.Events.GOAL_REACHED, stopMoving);
        EventManager.StartListening(EventManager.Events.B_KEY, ToggleCheatCode);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.Events.GOAL_REACHED, stopMoving);
        EventManager.StopListening(EventManager.Events.B_KEY, ToggleCheatCode);
    }

    IEnumerator charSleep()
    {
        _dyingCountdown = true;
        yield return new WaitForSeconds(_charDeathDelay);
        gameObject.SetActive(false);
        DestroyedByOutOfFuel();
        _dyingCountdown = true;
    }

	void Update()
	{
        //if (Input.GetKeyDown (KeyCode.B))
        //{
        //	ToggleCheatCode ();
        //}
        //Is used to alert player with everyone 10 fuel usage after the fuel count drops under 100
        if(_fuelChange == 10)
        {
            _alertSound = true;
            _fuelChange = 0;
        }
        // Constant value is our max velocity magnitude it can be changed from here 
        //if (_rigidBody.velocity.magnitude > maxVelocity)
        //{
        //    _rigidBody.velocity = _rigidBody.velocity.normalized * maxVelocity;
        //}
        if (_fuelReservoir.fuelCount <= 100)
        {
            if (_alertSound)
            {
                AudioSource.PlayClipAtPoint(soundEffects[1], Camera.main.transform.position, 0.4f);
                _alertSound = false;
            }
                
        }
		if (_fuelReservoir.fuelCount <= 0)
		{
            //Calling destroyed by blackhole for now
            if (!_dyingCountdown)
            {
                _runningCoroutine = StartCoroutine(charSleep());
                EventManager.TriggerEvent(EventManager.Events.DEATH_COUNTDOWN);
                Debug.Log("Death countdown started.");
            }

			//fuelDepletedText.text = "You are out of Fuel!";
			//fuelDepletedText.color = Color.red;
		}
        else
        {
            if (_dyingCountdown)
            {
                _dyingCountdown = false;
                EventManager.TriggerEvent(EventManager.Events.STOP_DEATH_COUNTDOWN);
                StopCoroutine(_runningCoroutine);
            }
        }
	}

	//Cheat code
	public void ToggleCheatCode()
	{
        _cheatMode = !_cheatMode;

	}


    //public GameObject GetPrimaryFireProjectile()
    //{
    //    if (!_cheatMode)
    //    {
    //        if (!_fuelReservoir.UseFuel(FuelReservoir.FuelUseType.PlasmaBullet))
    //        {
    //            return null;
    //        }
    //    }
        
    //    var bullet = Instantiate(primaryFireProjectilePrefab);
    //    Destroy(bullet, 5.0f);
    //    // Since fuel is used
    //    _fuelChange += 1;
    //    return bullet;
    //}

    //public GameObject GetSecondaryFireProjectile()
    //{
    //    if (!_cheatMode)
    //    {
    //        if (!_fuelReservoir.UseFuel(FuelReservoir.FuelUseType.BlackHole))
    //        {
    //            return null;
    //        }
    //    }
    //    var bullet = Instantiate(secondaryFireProjectilePrefab);
    //    Destroy(bullet, 30.0f);

    //    return bullet;
    //}

    public void AddFuel(int amount)
	{
        _fuelReservoir.AddFuel(amount);
		//fuelCountText.text = "Total Fuel : " + _fuelReservoir.fuelCount;
		Invoke ("RemoveFuelDepletedText", 1.0f);
		Invoke ("RemoveFuelText", 1.0f);
	}

    public void DestroyedByBlackHole()
    {
        Character.current = null;
        EventManager.TriggerEvent(EventManager.Events.PLAYER_DEAD);
        AudioSource.PlayClipAtPoint(soundEffects[0], Camera.main.transform.position, 0.8f);
        Instantiate(blackHoleExplosionPrefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

    public void DestroyedByBullet()
    {
        Character.current = null;
        EventManager.TriggerEvent(EventManager.Events.PLAYER_DEAD);
        AudioSource.PlayClipAtPoint(soundEffects[0], Camera.main.transform.position, 0.8f);
        Instantiate(blackHoleExplosionPrefab, transform.position, transform.rotation);
    }

    public void DestroyedByOutOfFuel()
    {
        Character.current = null;
        EventManager.TriggerEvent(EventManager.Events.PLAYER_DEAD);
        AudioSource.PlayClipAtPoint(soundEffects[0], Camera.main.transform.position, 0.8f);
        Instantiate(outOfFuelPrefab, transform.position, transform.rotation);
    }

    void RemoveFuelText()
	{
		//fuelCountText.text = "";
	}

	void RemoveFuelDepletedText()
	{
		//fuelDepletedText.text = "";
	}
	

    void stopMoving()
    {
        _rigidBody.velocity = new Vector3(0, 0, 0);
    }
}
