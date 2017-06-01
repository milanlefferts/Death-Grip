using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Weapon {
	public string weaponName;
	public int weaponDamage;
	public float weaponFireRate;
	public float weaponAbilityCooldown;
	public Action weaponAbility;
	public GameObject weaponProjectile;
	public AudioClip weaponFireSound;
	public Sprite weaponSpriteLeft;
	public Sprite weaponSpriteRight;

	public Weapon (string wName, int wDamage, float wFireRate, float wAbilityCooldown, Action wAbility, GameObject wProjectile, AudioClip wFireSound, Sprite wSpriteLeft, Sprite wSpriteRight) {
		weaponName = wName;
		weaponDamage = wDamage;
		weaponFireRate = wFireRate;
		weaponAbilityCooldown = wAbilityCooldown;
		weaponAbility = wAbility;
		weaponProjectile = wProjectile;
		weaponFireSound = wFireSound;
		weaponSpriteLeft = wSpriteLeft;
		weaponSpriteRight = wSpriteRight;
	}
}

public class WeaponController : MonoBehaviour {

	// Weapon Switching
	public List<Weapon> weaponList = new List<Weapon>();
	public int currentWeaponNr;
	public List<GameObject> weaponObjectList;

	private float weaponSwitchTimer = 1f;
	private float nextWeapon;

	public GameObject weaponHolder, leftWeapon, rightWeapon;
	private Animator weaponsAnim, leftWeaponAnim, rightWeaponAnim;
	private SpriteRenderer leftWeaponSprite, rightWeaponSprite;

	[Header("Weapon Attributes")]
	// Weapon
	private Weapon weapon;
	//public GameObject weaponObject;
	[SerializeField]
	private string weaponName;
	private Action Ability;

	public int damage;

	public float fireRate;
	private float nextFire;

	private float nextAbility;
	public float abilityCooldown;

	private GameObject projectile;
	private AudioClip projectileFireSound;

	[Header("Weapon Objects (assign manually")]
	public Transform projectileSpawner;
	// Handgun
	public GameObject handgunProjectile;
	public AudioClip handgunSound;
	public Sprite handgunSpriteLeft;
	public Sprite handgunSpriteRight;
	// Shotgun
	public GameObject shotgunProjectile;
	public AudioClip shotgunSound;
	public Sprite shotgunSpriteLeft;
	public Sprite shotgunSpriteRight;
	// GrenadeLauncher
	public GameObject grenadeLauncherProjectile;
	public AudioClip grenadeLauncherSound;
	public Sprite grenadeLauncherSpriteLeft;
	public Sprite grenadeLauncherSpriteRight;

	void Start () {


		leftWeaponSprite = leftWeapon.GetComponent<SpriteRenderer> ();
		rightWeaponSprite = rightWeapon.GetComponent<SpriteRenderer> ();

		leftWeaponAnim = leftWeapon.GetComponent<Animator> ();
		rightWeaponAnim = rightWeapon.GetComponent<Animator> ();
		weaponsAnim = weaponHolder.GetComponent<Animator> ();

		SetupWeapons ();

	}

	void Update () {
		// Primary Fire
		if (Input.GetKeyDown (KeyCode.Mouse0) && Time.time > nextFire) {
			leftWeaponAnim.SetTrigger ("Attack");
			nextFire = Time.time + fireRate;
			Shoot ();
		}

		// Alt Fire
		if (Input.GetKeyDown (KeyCode.Mouse1) && Time.time > nextAbility) {
			rightWeaponAnim.SetTrigger ("Attack");
			nextAbility = Time.time + abilityCooldown;
			Ability ();
		}

		// Scroll up
		if (Input.GetAxis ("Mouse ScrollWheel") > 0f && Time.time > nextWeapon) {
			nextWeapon = Time.time + weaponSwitchTimer;
			NextWeapon ();
		}

		// Scroll down
		if (Input.GetAxis ("Mouse ScrollWheel") < 0f && Time.time > nextWeapon) {
			nextWeapon = Time.time + weaponSwitchTimer;
			PreviousWeapon ();
		}
	}

	void NextWeapon () {
		print ("NextWeapon");
		weaponsAnim.SetTrigger ("Switch");

		if (currentWeaponNr < weaponList.Count - 1) {
			currentWeaponNr += 1;

		} else {
			currentWeaponNr = 0;
		}
		SetWeapon(weaponList[currentWeaponNr]);
	}

	void PreviousWeapon () {
		print ("PreviousWeapon");
		weaponsAnim.SetTrigger ("Switch");

		if (currentWeaponNr != 0) {
			currentWeaponNr -= 1;
		} else {
			currentWeaponNr = weaponList.Count - 1;
		}
		SetWeapon(weaponList[currentWeaponNr]);
	}

	void SetWeapon (Weapon weap) {
		print ("SetWeapon");
		weapon = weap;
		weaponName = weapon.weaponName;
		damage = weapon.weaponDamage;
		Ability = weapon.weaponAbility;
		fireRate = weapon.weaponFireRate;
		abilityCooldown = weapon.weaponAbilityCooldown;
		projectile = weapon.weaponProjectile;
		projectileFireSound = weapon.weaponFireSound;
		leftWeaponAnim.SetTrigger (weaponName);
		leftWeaponSprite.sprite = weapon.weaponSpriteLeft;
		rightWeaponAnim.SetTrigger (weaponName);
		rightWeaponSprite.sprite = weapon.weaponSpriteRight;
	}

	void SetupWeapons() {
		weaponList.Add(new Weapon("Handgun", 1, 0.2f, 0.2f, FreezeAbility, handgunProjectile, handgunSound, handgunSpriteLeft, handgunSpriteRight));
		weaponList.Add(new Weapon("Shotgun", 2, 0.8f, 0.2f, PullAbility, shotgunProjectile, shotgunSound, shotgunSpriteLeft, shotgunSpriteRight));
		weaponList.Add(new Weapon("GrenadeLauncher", 4, 1.2f, 0.2f, PushAbility, grenadeLauncherProjectile, grenadeLauncherSound, grenadeLauncherSpriteLeft, grenadeLauncherSpriteRight));

		currentWeaponNr = 0;
		SetWeapon (weaponList [currentWeaponNr]);
	}

	void PushAbility () {
		StartCoroutine(ScreenShake.Instance.ScreenShaker (0.01f, 0.1f));
		print ("Push");
	}

	void PullAbility () {
		StartCoroutine(ScreenShake.Instance.ScreenShaker (0.01f, 0.1f));
		print ("Pull");
	}

	void FreezeAbility () {
		StartCoroutine(ScreenShake.Instance.ScreenShaker (0.01f, 0.1f));
		print ("Freeze");
	}

	void Shoot () {
		// Spawn projectile
		GameObject newProjectile = Instantiate(projectile, projectileSpawner.position, projectileSpawner.rotation);
		newProjectile.GetComponent<ProjectileCollision> ().damage = damage;
		newProjectile.GetComponent<ProjectileCollision> ().owner = this.gameObject.tag;

		// Shake screen
		StartCoroutine(ScreenShake.Instance.ScreenShaker (0.01f, 0.1f));

		// Play gunshot audio
		Player.Instance.audioSource.PlayOneShot (projectileFireSound);
	}
}
