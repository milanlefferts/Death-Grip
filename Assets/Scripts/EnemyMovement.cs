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
	private Vector3 lastKnowPlayerPosition;
	public float minimumDistance;
	private float playerSearchTime;
	private float wanderDistance;
	private float wanderDirectionTime;

	public LayerMask layerMask;

	public GameObject player;
	private Rigidbody rb;
	private Animator anim;

	private NavMeshAgent agent;

	void Start () {
		//player = Player.Instance.gameObject;

		detectionDistance = 10f;
		detectionDistanceSquared = detectionDistance * detectionDistance;
		StartCoroutine (AwaitActivation());
		StartCoroutine (RotateTowardsPlayer());
		//minimumDistance = 2f;
		playerSearchTime = 2f;
		wanderDistance = 5f;
		wanderDirectionTime = 2f;

		anim = GetComponentInChildren<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
		rb = GetComponent<Rigidbody> ();
	}

	// Checks to see if the player is in range
	// If so, raycast until the player is seen, then activate this enemy
	private IEnumerator AwaitActivation() {

		StartCoroutine ("FindPlayer");

		while (!isActivated) {
			yield return null;
		}

		StartCoroutine ("Movement");
	}

	// Casts a ray every frame, attempting to locate the player
	private IEnumerator FindPlayer() {
		while (true) {
			Vector3 rayDirection = player.transform.position - transform.position;

			bool playerCloseToEnemy =  rayDirection.sqrMagnitude < detectionDistanceSquared;

			if (playerCloseToEnemy) {

				Debug.DrawRay (transform.position, rayDirection);

				RaycastHit hit;
				if (Physics.Raycast (transform.position, rayDirection, out hit, detectionDistance, layerMask) && hit.collider.tag == "Player") {
					isActivated = true;
					playerFound = true;
					lastKnowPlayerPosition = player.transform.position;
					//print (hit.collider.name);

				} else {
					//print (hit.collider.name);
					//isActivated = false;
					playerFound = false;
				}
			}
			yield return null;
		}
	}

	// Charge moves the enemy towards the player
	private IEnumerator Movement() {
		while (isActivated) {

			// Follow while the player is in view
			while (playerFound) {
				// Only moves if enemy is not too close to the player
				if (Vector3.Distance (transform.position, player.transform.position) >= minimumDistance) {

					// Move Enemy forward towards the player's position
					agent.isStopped = false;
					agent.SetDestination (lastKnowPlayerPosition);
				}
				// Else stop moving and Attack
				else {
					agent.isStopped = true;

					// If an Attack script is attached, 
					if (GetComponent<EnemyAttack> () != null) {
						
						// Start the Attack cycle, stop attacking when moving out of range or attack is interrupted
						IEnumerator newAttack = GetComponent <EnemyAttack> ().StartAttack ();
						StartCoroutine (newAttack);

						while (GetComponent<EnemyAttack> ().isAttacking && Vector3.Distance (transform.position, player.transform.position) < minimumDistance) {
							yield return null;
						}
						// Stop attacking

						GetComponent<EnemyAttack> ().isAttacking = false;
						StopCoroutine (newAttack);
						anim.SetTrigger ("Walk");

					}
				}
				yield return null;
			}

			// Wait until Enemy has approached last known player position
			agent.isStopped = false;
			agent.SetDestination (lastKnowPlayerPosition);
			yield return new WaitForSeconds(playerSearchTime);

			// If player is still not found, wander around 'searching' for the player
			// Moves the enemy in a random direction when it loses track of the player
			while (!playerFound) {
				agent.SetDestination (RandomDirection ());
				yield return new WaitForSeconds(wanderDirectionTime);
			}
		}
	}

	public void StopMovement() {
		isActivated = false;
		StopAllCoroutines ();
	}

	private Vector3 RandomDirection() {
		Vector3 randomDirection = Random.insideUnitSphere * wanderDistance;

		randomDirection += transform.position;
		NavMeshHit navHit;
		NavMesh.SamplePosition (randomDirection, out navHit, wanderDistance, -1);
		//print (navHit.position);
		return navHit.position;
	}

	// Rotates this object's transform towards the position of the target
	private IEnumerator RotateTowardsPlayer () {
		while (true) {
			Vector3 direction = (player.transform.position - transform.position).normalized;
			Quaternion lookRotation = Quaternion.LookRotation (new Vector3 (direction.x, 0, direction.z));
			transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, Time.deltaTime * 10f);
			yield return null;
		}
	}

	public IEnumerator EnemyPushed() {
		StopCoroutine ("Movement");
		agent.enabled = false;
		rb.isKinematic = false;
		rb.useGravity = true;

		GetComponent<EnemyAttack> ().isAttacking = false;

		//rb.AddExplosionForce (5f, transform.position, 3f);
		rb.AddForce(-transform.forward * 750f);

		yield return new WaitForSeconds (2f);

		rb.isKinematic = true;
		rb.useGravity = false;
		//transform.rotation = Quaternion.identity;
		rb.velocity = Vector3.zero;

		agent.enabled = true;
		StartCoroutine ("Movement");
	}

	public IEnumerator EnemyPulled() {
		StopCoroutine ("Movement");
		agent.enabled = false;
		rb.isKinematic = false;
		rb.useGravity = true;

		GetComponent<EnemyAttack> ().isAttacking = false;

		//rb.AddExplosionForce (5f, transform.position, 3f);
		rb.AddForce(transform.forward * 750f);

		yield return new WaitForSeconds (2f);

		rb.isKinematic = true;
		rb.useGravity = false;
		//transform.rotation = Quaternion.identity;
		rb.velocity = Vector3.zero;

		agent.enabled = true;
		StartCoroutine ("Movement");
	}

}