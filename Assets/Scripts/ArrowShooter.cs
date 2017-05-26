using UnityEngine;
using System.Collections;

public class ArrowShooter : MonoBehaviour {

	public GameObject positiveArrow;
	public GameObject negativeArrow;
	public Transform arrowSpawner;
	public float fireRate;
	
	private float nextFire;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1") && Time.time > nextFire){
			nextFire = Time.time + fireRate;
			Instantiate(positiveArrow, arrowSpawner.position, arrowSpawner.rotation);
		}
			
	}
}
