using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public bool isMoving;
	GameObject weapons, leftWeapon, rightWeapon;
	Animator weaponsAnim, leftWeaponAnim, rightWeaponAnim;

	void Start () {
		weapons = GameObject.FindGameObjectWithTag ("Weapons");
		weaponsAnim = weapons.GetComponent<Animator> ();



		leftWeapon = weapons.transform.Find ("LeftWeapon").gameObject;
		leftWeaponAnim = leftWeapon.GetComponent<Animator> ();

		rightWeapon = weapons.transform.Find ("RightWeapon").gameObject;
		rightWeaponAnim = rightWeapon.GetComponent<Animator> ();
	}

	void Update ()
	{
		CheckForMovement ();

		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			leftWeaponAnim.SetTrigger ("Punch");
		}

		if (Input.GetKeyDown (KeyCode.Mouse1)) {
			rightWeaponAnim.SetTrigger ("Punch");

		}

	}

	void CheckForMovement () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		if ((moveHorizontal != 0 || moveVertical != 0) && !isMoving) {
			isMoving = true;
			weaponsAnim.SetBool ("IsWalking", isMoving);
		} else if (moveHorizontal == 0 && moveVertical == 0 && isMoving) {
			isMoving = false;
			weaponsAnim.SetBool ("IsWalking", isMoving);
		}
	}



}