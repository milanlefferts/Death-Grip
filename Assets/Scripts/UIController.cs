using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public GameObject imgObj, textObj;
	private Text text;
	//private Image img;

	void Start () {
		text = textObj.GetComponent<Text> ();
		//img = imgObj.GetComponent<Image> ();

		EventManager.HealthChangeEvent += SetHealthText;

		EventManager.PlayerDamageEvent += SetHealthText;
		EventManager.PlayerDamageEvent += DamageTakenEffect;

	}

	void SetHealthText() {
		text.text = Player.Instance.life + "";
	}

	void DamageTakenEffect() {
		print ("BIG VISUAL FEEDBACK!");
	}

}
