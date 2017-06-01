using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
	public bool isAttacking;
	public float attackRate;
	public int attackDamage;
	public bool isMeleeAttack;

	public Transform bulletSpawner;
	public GameObject bullet;

	private Animator anim;

	void Start () {
		anim = GetComponentInChildren<Animator> ();
	
	}

	public IEnumerator StartAttack() {
		if (!isAttacking) {
			isAttacking = true;
			anim.SetTrigger ("Attack");

			while (isAttacking) {
				if (isMeleeAttack) {
					EventManager.Instance.PlayerDamage (-attackDamage);
				} else {
					GameObject newProjectile = Instantiate(bullet, bulletSpawner.position, bulletSpawner.rotation);
					newProjectile.GetComponent<ProjectileCollision>().damage = attackDamage;
					newProjectile.GetComponent<ProjectileCollision> ().owner = this.gameObject.tag;
				}
				yield return new WaitForSeconds (attackRate);
			}
			isAttacking = false;
		}
	}
}
