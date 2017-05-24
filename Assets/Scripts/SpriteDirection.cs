using UnityEngine;
using System.Collections;

// This script calculates the angle between the Player and the Object
// in order to determine the appropriate Sprite to display for
// for the correct viewing perspective, simulating the DOOM engine's
// way for handling its pseudo-3D images visually. 

public class SpriteDirection : MonoBehaviour {

	public GameObject player;
	public Sprite[] objectPositionSprites;

	private Vector3 playerPosition;
	private Vector3 spritePosition;
	private Vector3 playerToSpriteDistance;
	
	private float angle;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");

	}
	
	// Update is called once per frame
	void Update () {


		// Determines player position and sprite position and subtracts them,
		// making the angle calculated independent of player position
		Vector3 playerPosition = player.transform.position;
		Vector3 spritePosition = transform.position;
		Vector3 playerToSpriteDistance = new Vector2(spritePosition.x, spritePosition.z) - new Vector2(playerPosition.x, playerPosition.z);

		// Calculates angle between player and sprite
		angle = Vector2.Angle(new Vector2(transform.forward.x, transform.forward.z),
		                      playerToSpriteDistance);

		// Takes Cross Product of the Vectors used in the angle to convert 
		// the 0-180 angle to a full 0-360 degrees
		Vector3 cross = Vector3.Cross (new Vector2 (transform.forward.x, transform.forward.z),
		                               playerToSpriteDistance);

		if (cross.z > 0f) {
			angle = 360.0f - angle;
		}

		// if for any reason the angle goes below 0, it is returned to the appropriate value
		if (angle < 0.0f) {
			angle += 360.0f;
		}

		//print (angle);

		// Divide the angle by the amount of Sprite indexes wanted (in this case 8, so 360 / 8 = 45)
		int index = Mathf.RoundToInt (angle / 45.0f);
		//print (index);

		// Select the appropriate Sprite associated with the index
		GetComponentInChildren<SpriteRenderer> ().sprite = objectPositionSprites [index];
	}
}

/*
	Erroneous method 1.
-----------------------

	private Vector3 playerPosition;
	private Vector3 spritePosition;
	private Vector2 spriteFacing;
	private Vector2 playerToSpriteDistance;
	private Vector3 direction;

		playerPosition = player.transform.position;
		spritePosition = transform.position;

		spriteFacing = new Vector2 (transform.forward.x, transform.forward.z);
		//print (transform.forward.x);
		playerToSpriteDistance = new Vector2(spritePosition.x, spritePosition.z) - new Vector2(playerPosition.x, playerPosition.z);

		direction = playerToSpriteDistance - spriteFacing;

		angle = Mathf.Atan2 (direction.z, direction.x) * Mathf.Rad2Deg;
		*/

/*	Erroneous method 2.
-----------------------
		playerPosition = player.transform.position;
		spritePosition = transform.position;

		spriteFacing = transform.forward;

		playerToSpriteDistance = spritePosition - playerPosition;

		direction = playerToSpriteDistance - spriteFacing;

		angle = Mathf.Atan2 (direction.z, direction.x) * Mathf.Rad2Deg;
*/
/*
 * 	Erroneous method 3 (best).
-----------------------
 * 
 * 
		angle = Vector2.Angle( 
		                      new Vector2(transform.forward.x, transform.forward.z),
		                      new Vector2(player.transform.position.x, player.transform.position.z));

		Vector3 cross = Vector3.Cross (
		                               new Vector2 (transform.forward.x, transform.forward.z),
		                               new Vector2 (player.transform.position.x, player.transform.position.z));

	if (cross.z > 0f) {
			angle = 360.0f - angle;
		}

		if (angle < 0.0f) {
			angle += 360.0f;
		}

		int index = Mathf.RoundToInt (angle / 45.0f);
		*/


