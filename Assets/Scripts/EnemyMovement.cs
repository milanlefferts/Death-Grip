using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

	// Actication
	public bool isActivated;
	public bool playerFound;

	private float detectionDistance, detectionDistanceSquared;

	// Movement
	float movementSpeed;

	// Charging
	float minimumDistance;


	public GameObject player;

	NavMeshAgent agent;

	void Start () {
		detectionDistance = 5f;
		detectionDistanceSquared = detectionDistance * detectionDistance;
		StartCoroutine (AwaitActivation());
		movementSpeed = 1f;
		minimumDistance = 2f;

		agent = GetComponent<NavMeshAgent> ();
	}

	// Checks to see if the player is in range
	// If so, raycast until the player is seen, then activate this enemy
	private IEnumerator AwaitActivation() {

		while (!isActivated && !playerFound) {
			RotateTowards(player.transform);

			FindPlayer ();

			yield return null;
		}

		StartCoroutine (Charge ());
	}

	private IEnumerator CanSeePlayer() {
		yield return null;
	}

	// Moves the enemy in a random direction when it loses track of the player
	private void FindPlayer() {
		Vector3 rayDirection = player.transform.position - transform.position;

		bool playerCloseToEnemy =  rayDirection.sqrMagnitude < detectionDistanceSquared;

		if (playerCloseToEnemy) {
			
			Debug.DrawRay (transform.position, rayDirection);

			RaycastHit hit;
			if (Physics.Raycast (transform.position, rayDirection, out hit) && hit.collider.tag == "Player") {
				isActivated = true;
				playerFound = true;
			} else {
				isActivated = false;
				playerFound = false;
			}
		}

	}

	// Charge moves the enemy towards the player
	private IEnumerator Charge() {
		print ("Start charing");
		while (playerFound) {
			RotateTowards(player.transform);

			// Only moves if enemy is not too close to the player
			if (Vector3.Distance (transform.position, player.transform.position) >= minimumDistance) {

				// Move Enemy forward towards the player's position
				agent.isStopped = false;
				agent.SetDestination(player.transform.position);
			}
			// Else stop moving and Attack
			else {
				agent.isStopped = true;

				// If an Attack script is attached, 
				if (GetComponent<EnemyAttack> () != null) {
					// Start the Attack cycle, stop attacking when moving out of range or attack is interrupted

					IEnumerator newAttack = GetComponent <EnemyAttack> ().StartAttack();
					StartCoroutine (newAttack);

					while (GetComponent<EnemyAttack> ().isAttacking && Vector3.Distance (transform.position, player.transform.position) < minimumDistance) {
						yield return null;
					}
					// Stop attacking
					GetComponent<EnemyAttack> ().isAttacking = false;
					StopCoroutine (newAttack);
				}
			}

			yield return null;
		}
	}

	// Rotates this object's transform towards the position of the target
	private void RotateTowards(Transform target) {
		
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
	}

	/*
	 * if (Vector3.Distance (transform.position, player.transform.position) <= MaxDist) {
					//Here Call any function U want Like Shoot at here or something
				} */

}