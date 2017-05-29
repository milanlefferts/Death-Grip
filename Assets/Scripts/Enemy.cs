using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private AudioSource audioSource;
	private BoxCollider boxCollider;
	private SpriteRenderer sprite;
	private Animator anim;
	private Rigidbody rb;

	public AudioClip enemyHit, enemyDeath;

	private int life;

	void Start () {
		
		audioSource = GetComponent<AudioSource> ();
		boxCollider = GetComponent<BoxCollider> ();
		sprite = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody> ();

		life = 4;
	}
		
	public void EnemyTakeDamage(int damage) {

		life -= damage;

		if (life > 0) {
			HitFeedback ();
		} else {
			Death ();

		}
	}

	private void HitFeedback() {
		anim.SetTrigger("Hit");

		audioSource.PlayOneShot (enemyHit, 0.3f);
	}

	private void Death () {

		anim.SetTrigger("Death");

		audioSource.PlayOneShot (enemyDeath);

	}
}
