using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour {
	
	private AudioSource audio;
	private SphereCollider collider;
	private SpriteRenderer sprite;
	public AudioClip projectileImpactSound;
	private Animator anim;
	private Rigidbody rb;

	public float speed;
	public int damage;

	
	void Start () {
		audio = GetComponent<AudioSource> ();
		collider = GetComponent<SphereCollider> ();
		sprite = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody> ();

		speed = 20;
		rb.velocity = transform.forward * speed;
		rb.rotation = Quaternion.identity;

		damage = Player.Instance.GetWeaponDamage ();

		StartCoroutine (SelfDestruct());
	}

	void OnTriggerEnter(Collider other) {

		switch (other.tag) {
		case "Enemy":
			anim.SetTrigger ("Impact");

			EnemyImpact (other.gameObject);
			StartCoroutine (DestroyProjectile ());
			break;
		case "Wall":
			anim.SetTrigger ("Impact");

			WallImpact ();
			StartCoroutine (DestroyProjectile ());
			break;

		case "Grate":
			sprite.sortingLayerName = "Wall";
			break;

		default:
			
			break;
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

	void WallImpact() {
		audio.PlayOneShot (projectileImpactSound, 0.2f);
	}

	void EnemyImpact(GameObject enemy) {

		// Enemy impact event
		enemy.GetComponent<Enemy>().EnemyTakeDamage(damage);
	}

	private IEnumerator SelfDestruct () {
		yield return new WaitForSeconds (5f);
		Destroy (this.gameObject);
	}

}
