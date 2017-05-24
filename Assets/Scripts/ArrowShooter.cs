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
		if (Input.GetButton("Fire1") && Time.time > nextFire){
			nextFire = Time.time + fireRate;
			Instantiate(positiveArrow, arrowSpawner.position, arrowSpawner.rotation);
		}

		if (Input.GetButton("Fire2") && Time.time > nextFire){
			nextFire = Time.time + fireRate;
			Instantiate(negativeArrow, arrowSpawner.position, arrowSpawner.rotation);
		}
	}
}
