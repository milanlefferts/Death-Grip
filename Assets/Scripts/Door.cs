using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	[SerializeField]
	private bool isOpened;
	private Animator anim;
	private AudioSource audioSource;
	public AudioClip openDoorSound;
	private BoxCollider otherCollider;

	void Start () {
		isOpened = false;
		anim = GetComponentInChildren<Animator> ();
		audioSource = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player" && !isOpened) {
			StartCoroutine (Player.Instance.DoorOpener ());
			EventManager.OpenDoorEvent += OpenDoorWrapper;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			EventManager.OpenDoorEvent -= OpenDoorWrapper;

			// Cancels opening the door yield if leaving the trigger
			Player.Instance.CancelDoorOpener ();

			// Close door if it is opened
			if (isOpened) {
				StartCoroutine (CloseDoor ());
			}
		}
	}

	void OpenDoorWrapper() {
		if (isOpened == false) {
			isOpened = true;
			anim.SetTrigger ("Open");
			audioSource.PlayOneShot (openDoorSound);
		}
	}
		
	IEnumerator CloseDoor() {
		isOpened = false;

		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("ClosingDoor")) {
			yield break;
		}
		yield return new WaitForSeconds (2f);
		anim.SetTrigger ("Close");
		audioSource.PlayOneShot (openDoorSound);
	}
}
