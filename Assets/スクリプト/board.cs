using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class board : MonoBehaviour {
	public GameObject boardup;
	public GameObject T_hero;
	public GameObject attackEF;

	private Animator anim;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}


	
	void ATKEFDL (){
		attackEF.SetActive (false);
	}
	IEnumerator attackhero(){
		while (true) {
			attackEF.SetActive (true);
			T_hero.GetComponent<Animator> ().SetTrigger ("attack");
			attackEF.GetComponent<Animator> ().SetTrigger ("attack");
			yield return null;
			yield return new WaitForSeconds(0.6f);
			//attackEF.SetActive (false);
			yield return new WaitForSeconds(1.0f);
			yield return null;
		}
	
	}
	void OnTriggerEnter2D(Collider2D collider) { 
		Debug.Log (collider.gameObject.tag);
		if (collider.gameObject.tag == "Player") {
			boardup.GetComponent<Animator> ().SetBool ("board", true);
			StartCoroutine ("attackhero");
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
			StopCoroutine("attackhero");
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
