using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWall : MonoBehaviour {

	void OnCollisionEnter(Collision other) {
		if (other.collider.tag == "Enemy") {
			print ("collided");
			if (other.collider.GetComponent<Enemy> () != null) {
				other.collider.GetComponent<Enemy> ().EnemyTakeDamage (10);
			}
		}
	}
}
