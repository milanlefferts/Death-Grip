using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public GameObject imgObj, textObj,damageOverlayObj, deathText;
	private Text text;
	//private Image img;

	void Start () {
		text = textObj.GetComponent<Text> ();

		EventManager.HealthChangeEvent += SetHealthText;

		EventManager.PlayerDamageUIEvent += DamageTakenEffectWrapper;

		EventManager.PlayerDeathEvent += Death;
	}

	private void SetHealthText() {
		text.text = Player.Instance.life + "";
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
	}
}
