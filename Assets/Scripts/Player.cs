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

	public bool isMoving;

	[SerializeField]
	private bool isOpeningDoor;

	public GameObject weaponHolder;
	public Animator weaponsAnim;
	public AudioSource audioSource;

	public AudioClip playerDamageSound;

	private KeyCode activateKey;

	void Start () {
		instance = this;

		// Weapons
		weaponsAnim = weaponHolder.GetComponent<Animator> ();

		activateKey = KeyCode.E;

		audioSource = GetComponent<AudioSource> ();

		life = 100;

		isOpeningDoor = false;

		EventManager.HealthPickupEvent += ChangeHealth;
		EventManager.PlayerDamageEvent += ChangeHealth;
	}

	void ChangeHealth(int healthChange) {
		if (healthChange < 0) {
			
			TakeDamage ();
		}

		life += healthChange;
		EventManager.Instance.HealthChange();

		if (life > 100) {
			life = 100;
		} else if (life < 1) {
			Die ();
		}
	}

	void TakeDamage () {
		EventManager.Instance.PlayerDamageUI();

		audioSource.PlayOneShot(playerDamageSound, 0.3f);
	}

	void Die () {
		print ("YOU ARE DEAD");
	}
		
	void Update () {
		CheckForMovement ();

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

	public IEnumerator DoorOpener() {
		if (!isOpeningDoor) {
			isOpeningDoor = true;

			while (!Input.GetKeyDown(activateKey) || isOpeningDoor == false) {
				yield return null;
			}

			isOpeningDoor = false;

			EventManager.Instance.OpenDoor ();
		}
	}

	public void CancelDoorOpener() {
		isOpeningDoor = false;
	}

}