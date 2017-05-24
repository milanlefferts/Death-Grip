using UnityEngine;
using System.Collections;

public class LoadGame : MonoBehaviour {
	
	
	void Update () {
		if (Input.anyKeyDown) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				Application.Quit();
			} else {
			Application.LoadLevel("Game");
			}
		}
	}
	
}
