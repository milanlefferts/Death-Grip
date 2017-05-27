using UnityEngine;
using System.Collections;

public class ArrowMover : MonoBehaviour {

	public float speed;
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
		//GetComponent<Rigidbody> ().rotation = Quaternion.Euler (90.0f, 0.0f, 0.0f);
		GetComponent<Rigidbody> ().rotation = Quaternion.identity;

		StartCoroutine(ScreenShake.Instance.ScreenShaker (0.01f, 0.2f));
	}

	void OnCollisionEnter(Collision other) {
		//GetComponent<Rigidbody> ().isKinematic = true;
	//	Destroy (this.gameObject);
		//print ("hit");

	}
	

}
