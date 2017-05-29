using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	// Allows remote access for unique instance
	public static Player Instance {
		get{
			return instance;
		}
	}
	private static Player instance;

	public int life;
	private int weaponDamage;

	public bool isMoving;

	[SerializeField]
	private bool isOpeningDoor;

	public GameObject weapons, leftWeapon, rightWeapon;
	private Animator weaponsAnim, leftWeaponAnim, rightWeaponAnim;
	public AudioSource audio;

	private KeyCode activateKey;

	void Start () {
		instance = this;

		// Weapons
		weaponsAnim = weapons.GetComponent<Animator> ();
		leftWeaponAnim = leftWeapon.GetComponent<Animator> ();
		rightWeaponAnim = rightWeapon.GetComponent<Animator> ();

		activateKey = KeyCode.E;

		audio = GetComponent<AudioSource> ();

		life = 100;
		weaponDamage = 1;
		isOpeningDoor = false;

		EventManager.HealthPickupEvent += ChangeHealth;
	}

	void ChangeHealth(int healthChange) {
		life += healthChange;
		EventManager.Instance.HealthChange();
	}
		
	void Update () {
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

	// Weapon Damage getter
	public int GetWeaponDamage() {
		return weaponDamage;
	}

	public IEnumerator DoorOpener() {
		if (!isOpeningDoor) {
			
			//print ("is opening door");

			isOpeningDoor = true;

			while (!Input.GetKeyDown(activateKey) || isOpeningDoor == false) {
				yield return null;
			}

			isOpeningDoor = false;

			EventManager.Instance.OpenDoor ();

			//print ("gedrukt");


		}
	}

	public void CancelDoorOpener() {
		isOpeningDoor = false;
	}

}