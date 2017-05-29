using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayer : MonoBehaviour {
	Renderer renderer;
	// Use this for initialization
	void Start () {
		renderer = GetComponent<MeshRenderer> ();
		renderer.sortingLayerName = "Character";
		renderer.sortingOrder = 1;
	}
	

}
