using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulledProjectile : MonoBehaviour {

	private new AudioSource audio;
	private new SphereCollider collider;
	private SpriteRenderer sprite;
	public AudioClip projectileImpactSound;
	private Animator anim;
	private Rigidbody rb;

	public float speed;
	public int damage;
	public string owner;

	void Start () {
		audio = GetComponent<AudioSource> ();
		collider = GetComponent<SphereCollider> ();
		sprite = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody> ();

		speed = 20;
		rb.velocity = transform.forward * speed;
		rb.rotation = Quaternion.identity;

		StartCoroutine (SelfDestruct());
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Enemy") {
			anim.SetTrigger ("Impact");
			EnemyPull (other.gameObject);
			//StartCoroutine (DestroyProjectile ());
		}
	}

	IEnumerator DestroyProjectile() {
		collider.enabled = false;

		rb.velocity = Vector3.zero;

		while (!anim.GetCurrentAnimatorStateInfo(0).IsName("ProjectileExit")) {
			yield return null;
		}
		Destroy (this.gameObject);
	}

	void EnemyPull(GameObject enemy) {
		if (enemy.GetComponent<EnemyMovement> () != null) {
			StartCoroutine(enemy.GetComponent<EnemyMovement> ().EnemyPulled ());
		}
	}
		
	private IEnumerator SelfDestruct () {
		yield return new WaitForSeconds (5f);
		Destroy (this.gameObject);
	}

}
