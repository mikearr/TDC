using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public City cityPrefab;
	private City cityInstance;

	private void BeginGame(){
		cityInstance = Instantiate (cityPrefab) as City; 
		StartCoroutine(cityInstance.Generate());
	}

	private void RestartGame(){
		StopAllCoroutines();
		Destroy (cityInstance.gameObject);
		BeginGame();
	}

	// Use this for initialization
	void Start () {
		BeginGame ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
