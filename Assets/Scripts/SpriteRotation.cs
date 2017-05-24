using UnityEngine;
using System.Collections;

public class SpriteRotation : MonoBehaviour {

	public Camera playerCamera;

	void Start () {
		playerCamera = GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<Camera>();
	}

	void Update () {

		//transform.LookAt (playerCamera.transform);
		transform.LookAt (transform.position + playerCamera.transform.rotation * Vector3.forward, 
		                 playerCamera.transform.rotation * Vector3.up);



	}
}
