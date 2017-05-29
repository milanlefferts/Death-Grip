using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public GameObject imgObj, textObj;
	private Text text;
	private Image img;

	void Start () {
		text = textObj.GetComponent<Text> ();
		img = imgObj.GetComponent<Image> ();

		EventManager.HealthChangeEvent += SetHealthText;

	}

	void SetHealthText() {
		text.text = Player.Instance.life + "";
	}


}
