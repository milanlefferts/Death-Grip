using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {
	AudioSource audio;
	SpriteRenderer sprite;
	private bool pickedup;
	void Start () {
		audio = GetComponent<AudioSource> ();
		sprite = GetComponent<SpriteRenderer> ();
	}
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player" && !pickedup) {
			StartCoroutine(Pickedup());

		}
	}

	private IEnumerator Pickedup() {
		EventManager.Instance.HealthPickup();
		pickedup = true;
		audio.Play ();
		sprite.enabled = false;
		yield return new WaitUntil (() => !audio.isPlaying);

		Destroy (this.gameObject);
	}

}
