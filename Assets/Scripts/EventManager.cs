using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour {

	// Allows remote access for unique instance
	public static EventManager Instance {
		get{
			return instance;
		}
	}
	private static EventManager instance;

	void Start () {
		instance = this;
	}

	public delegate void SingleParameterDelegate<T> (T para);

	public static event SingleParameterDelegate<int> HealthPickupEvent;
	public void HealthPickup(int health) {
		HealthPickupEvent (health);
	}

	public static event Action HealthChangeEvent;
	public void HealthChange() {
		HealthChangeEvent ();
	}

	public static event SingleParameterDelegate<int> PlayerDamageEvent;
	public void PlayerDamage(int damage) {
		PlayerDamageEvent (damage);
	}

	public static event Action PlayerDamageUIEvent;
	public void PlayerDamageUI() {
		PlayerDamageUIEvent ();
	}

	public static event Action OpenDoorEvent;
	public void OpenDoor() {
		OpenDoorEvent ();
	}


}
