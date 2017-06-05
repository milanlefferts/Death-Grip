using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWall : MonoBehaviour {

	void OnCollisionEnter (Collision other) {
		if (other.collider.tag == "Enemy") {
			if (other.collider.GetComponent<Enemy> () != null) {
				other.collider.GetComponent<Enemy> ().EnemyTakeDamage (10);
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			if (other.GetComponent<Player> () != null) {
				StartCoroutine ("LavaDamage");
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			StopCoroutine ("LavaDamage");
		}
	}

	IEnumerator LavaDamage() {
		while (true) {
			EventManager.Instance.PlayerDamage (-10);
			yield return new WaitForSeconds (0.5f);
		}
	}
}