using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public GameObject imgObj, textObj,damageOverlayObj;
	private Text text;
	//private Image img;

	void Start () {
		text = textObj.GetComponent<Text> ();
		//img = imgObj.GetComponent<Image> ();

		EventManager.HealthChangeEvent += SetHealthText;

		EventManager.PlayerDamageUIEvent += DamageTakenEffectWrapper;

	}

	private void SetHealthText() {
		text.text = Player.Instance.life + "";
	}

	private void DamageTakenEffectWrapper() {
		StartCoroutine (DamageTakenEffect ());	}

	private IEnumerator DamageTakenEffect() {
		damageOverlayObj.SetActive (true);
		yield return new WaitForSeconds (0.1f);
		damageOverlayObj.SetActive (false);

	}

}
