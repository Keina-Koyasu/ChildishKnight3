using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour {
	public GameObject newgame;
	public GameObject gallery;
	public GameObject blackout;


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

			/* ギャラリーモード完成したら使えるようにして
			if (Input.GetKeyDown ("down")||-1 == Input.GetAxisRaw ("Vertical") ) {
			if (mode) {
				mode = false;
			} else {
				mode = true;
			}
		}
			if(Input.GetKeyDown("up")||1 == Input.GetAxisRaw ("Vertical") ){
			if (mode == false) {
				mode = true;
			} else {
				mode = false;
			}
		}
			*/
			if (Input.GetKey ("a")||Input.GetButtonDown ("Attack")) {
			if (mode) {
				blackout.SetActive (true);
				blackout.GetComponent<blackout> ().fedein = true ;

				//Application.LoadLevel ("scene1");
			}		
		}
	}
}
}
