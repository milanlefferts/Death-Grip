using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	
	private AudioSource audioSource;
	private BoxCollider boxCollider;
	//private SpriteRenderer sprite;
	private Animator anim;
	//private Rigidbody rb;

	public AudioClip enemyHit, enemyDeath;

	private int life;
	public int attackDamage;

	void Start () {
		
		audioSource = GetComponent<AudioSource> ();
		boxCollider = GetComponent<BoxCollider> ();
		//sprite = GetComponent<SpriteRenderer> ();
		anim = GetComponentInChildren<Animator> ();
		//rb = GetComponent<Rigidbody> ();

		life = 4;
		attackDamage = 10;
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

		// Stop attacking on hit
		if (GetComponent<EnemyAttack> () != null) {
			GetComponent <EnemyAttack> ().isAttacking = false;
		}
	}

	private void Death () {

		anim.SetTrigger("Death");

		// Stop all movement on death
		if (GetComponent<EnemyMovement> () != null) {
			GetComponent <EnemyMovement> ().StopMovement ();
		}

		if (GetComponent<EnemyAttack> () != null) {
			GetComponent <EnemyAttack> ().isAttacking = false;
		}

		boxCollider.enabled = false;
		audioSource.PlayOneShot (enemyDeath);
	}
}
