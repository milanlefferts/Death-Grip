using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {

	public GameObject projectile;
	public Transform arrowSpawner;

	public float fireRate;
	private float nextFire;

	public AudioClip projectileFireSound;

	void Start () {
		fireRate = 0.2f;
	}

	void Update () {
		if (Input.GetButtonDown("Fire1") && Time.time > nextFire){
			nextFire = Time.time + fireRate;
			Shoot ();
		}
	}

	void Shoot () {
		// Spawn projectile
		GameObject newProjectile = Instantiate(projectile, arrowSpawner.position, arrowSpawner.rotation);
		newProjectile.GetComponent<ProjectileCollision>().damage = Player.Instance.GetWeaponDamage ();
		newProjectile.GetComponent<ProjectileCollision> ().owner = this.gameObject.tag;


		// Shake screen
		StartCoroutine(ScreenShake.Instance.ScreenShaker (0.01f, 0.1f));

		// Play gunshot audio
		Player.Instance.audioSource.PlayOneShot (projectileFireSound);
	}
}
