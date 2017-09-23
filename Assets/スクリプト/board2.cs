using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class board2 : MonoBehaviour {
	public GameObject boardup;
	public GameObject text;

	private Animator anim;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}


	


	void OnTriggerEnter2D(Collider2D collider) { 
		Debug.Log (collider.gameObject.tag);
		if (collider.gameObject.tag == "Player") {
			boardup.GetComponent<Animator> ().SetBool ("board", true);
		
			/*
			T_hero.GetComponent<Animator> ().SetTrigger ("attack");
			attackEF.SetActive (true);
			attackEF.GetComponent<Animator> ().SetTrigger ("attack");
			Invoke ("ATKEFDL", 0.7f); //0.25f後にアタックエフェクトを消す
			//attackEF.SetActive (false);
			*/
			} 
	}
	void OnTriggerExit2D(Collider2D collider) {
		if (collider.gameObject.tag == "Player") {
			boardup.GetComponent<Animator> ().SetBool ("board", false);

		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
