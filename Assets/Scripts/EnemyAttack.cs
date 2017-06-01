using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
	public bool isAttacking;
	public float attackRate;
	public int attackDamage;

	void Start () {
		attackRate = 2f;
		attackDamage = 10;
	}

	public IEnumerator StartAttack() {
		if (!isAttacking) {
			isAttacking = true;

			while (isAttacking) {
				EventManager.Instance.PlayerDamage(-attackDamage);
				yield return new WaitForSeconds (attackRate);
			}
			isAttacking = false;
		}
	}
}
