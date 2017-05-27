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
		

	// Health Pickup
	public static event Action HealthPickupEvent;
	public void HealthPickup() {
		HealthPickupEvent ();
	}



}
