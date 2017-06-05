using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

using UnityStandardAssets.Characters.FirstPerson;

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
	private bool isAlive;

	public bool isMoving;

	[SerializeField]
	private bool isOpeningDoor;

	public GameObject weaponHolder;
	public Animator weaponsAnim;
	public AudioSource audioSource;
	private BoxCollider boxcollider;

	public AudioClip playerDamageSound;

	private KeyCode activateKey;

	void Start () {
		instance = this;
		boxcollider = GetComponent<BoxCollider> ();

		// Weapons
		weaponsAnim = weaponHolder.GetComponent<Animator> ();

		activateKey = KeyCode.E;

		audioSource = GetComponent<AudioSource> ();

		life = 100;
		isAlive = true;

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
		} else if (life < 1 && isAlive) {
			StartCoroutine (Die ());
		}
	}

	void TakeDamage () {
		EventManager.Instance.PlayerDamageUI();
		audioSource.PlayOneShot(playerDamageSound, 0.3f);
	}

	IEnumerator Die () {
		isAlive = false;
		boxcollider.enabled = false;
		GetComponent<CharacterController> ().enabled = false;
		GetComponent<FirstPersonController> ().enabled = false;
		EventManager.Instance.PlayerDeath ();

		// Change
	
		print ("YOU ARE DEAD");

		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene (0);
	}
		
	void Update () {
		CheckForMovement ();
	}

	void CheckForMovement () {
		if (isAlive) {
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

	void OnDestroy() {
		EventManager.HealthPickupEvent -= ChangeHealth;
		EventManager.PlayerDamageEvent -= ChangeHealth;
	}

}