using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour {
	public GameObject newgame;
	public GameObject gallery;
	public GameObject blackout;
	private float InputTime=0; //入力のクールタイム

	public bool mode = true; //trueだとnewgame
	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
		if(blackout.activeSelf == false){
		if (mode) {
			newgame.GetComponent<Animator> ().SetBool ("newgame",true);
			gallery.GetComponent<Animator> ().SetBool ("gallery",false);
		}else if(mode == false){
			newgame.GetComponent<Animator> ().SetBool ("newgame",false);
			gallery.GetComponent<Animator> ().SetBool ("gallery",true);
		}

			if (InputTime == 0) {
				if (Input.GetKeyDown ("down") || -1 == Input.GetAxisRaw ("Vertical")) {
					if (mode) {
						mode = false;
						InputTime = 100;
						GetComponents<AudioSource> ()[0].Play ();

					} else {
						mode = true;
						InputTime = 100;
						GetComponents<AudioSource> ()[0].Play ();

					}
				}
				if (Input.GetKeyDown ("up") || 1 == Input.GetAxisRaw ("Vertical")) {
					if (mode == false) {
						mode = true;
						InputTime = 100;
						GetComponents<AudioSource> ()[0].Play ();

					} else {
						mode = false;
						InputTime = 100;
						GetComponents<AudioSource> ()[0].Play ();


					}
				}
			}
			if (InputTime > 0) {
				if (Input.GetAxisRaw ("Vertical") == 0 && Input.GetAxisRaw ("Horizontal") == 0) {
					InputTime = 0;
				} else {
					InputTime -= 5;
				}
			}

		if (Input.GetKey ("a")||Input.GetButtonDown ("Attack")) {
				blackout.SetActive (true);
				blackout.GetComponent<blackout> ().fedein = true ;
				GetComponents<AudioSource> ()[1].Play ();

				
		}
	}
}
}
