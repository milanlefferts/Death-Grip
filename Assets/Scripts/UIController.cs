using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public GameObject lifeImgObj, lifeTextObj, damageOverlayObj, deathText, weaponImgObj, weaponTextObj;
	private Text lifeText, weaponText;
	//private Image img;

	void Start () {
		lifeText = lifeTextObj.GetComponent<Text> ();
		weaponText = weaponTextObj.GetComponent<Text> ();

		EventManager.HealthChangeEvent += SetHealthText;
		EventManager.PlayerDamageUIEvent += DamageTakenEffectWrapper;
		EventManager.PlayerDeathEvent += Death;
		EventManager.WeaponSwitchEvent += WeaponSwitch;
	}

	private void WeaponSwitch() {
		weaponText.text = WeaponController.Instance.weaponName;
	}

	private void SetHealthText() {
		lifeText.text = Player.Instance.life + "";
	}

	private void DamageTakenEffectWrapper() {
		StartCoroutine (DamageTakenEffect ());	}

	private IEnumerator DamageTakenEffect() {
		damageOverlayObj.SetActive (true);
		yield return new WaitForSeconds (0.05f);
		damageOverlayObj.SetActive (false);
	}

	private void Death() {
		EventManager.HealthChangeEvent -= SetHealthText;
		EventManager.PlayerDamageUIEvent -= DamageTakenEffectWrapper;

		deathText.SetActive (true);
		damageOverlayObj.SetActive (true);
	}

	void OnDestroy () {
		EventManager.HealthChangeEvent -= SetHealthText;
		EventManager.PlayerDamageUIEvent -= DamageTakenEffectWrapper;
		EventManager.PlayerDeathEvent -= Death;
		EventManager.WeaponSwitchEvent -= WeaponSwitch;
	}
}
