using UnityEngine;

public class Camera : MonoBehaviour {
	Animator anim;
	void Start () {
		anim = GetComponent<Animator> ();
		EventManager.PlayerDeathEvent += PlayerDeath;
	}

	void PlayerDeath () {
		anim.SetTrigger ("Death");
	}

}
