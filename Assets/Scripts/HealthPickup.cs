using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

	[HideInInspector]
	protected AudioSource audio;
	[HideInInspector]
	protected SpriteRenderer sprite;

	public int healthGain;

	protected bool pickedup;

	protected virtual void Start () {
		audio = GetComponent<AudioSource> ();
		sprite = GetComponent<SpriteRenderer> ();
	}

	protected void OnTriggerEnter(Collider other) {
		if (other.tag == "Player" && !pickedup) {
			StartCoroutine(Pickedup());
		}
	}

	protected IEnumerator Pickedup() {
		EventManager.Instance.HealthPickup(healthGain);
		pickedup = true;
		audio.Play ();
		sprite.enabled = false;
		yield return new WaitUntil (() => !audio.isPlaying);

		Destroy (this.gameObject);
	}
}
