﻿using UnityEngine;
using System.Collections;

public class attack_judg2 : MonoBehaviour {
	public float attack = 10f;  //攻撃力
	public GameObject Player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D collider) {
       // Debug.Log(collider.gameObject.tag);
		switch(Player.GetComponent<hero2>().level){
		case 1:
			if (collider.gameObject.tag == "pino") {
				//collider.gameObject.GetComponent<Enemy> ().hit ();
				collider.gameObject.SendMessage("hit", attack);   //相手の"Damage"関数を呼び出す

			}
			if (collider.gameObject.tag == "tamashibi") {
				collider.gameObject.GetComponent<Tamashibi_AI3> ().hit ();
                }
			if (collider.gameObject.tag == "Kakashi") {
				collider.gameObject.SendMessage("hit", attack);   //相手の"Damage"関数を呼び出す

			}
			if (collider.gameObject.name == "boss") {
				//collider.gameObject.GetComponent<Enemy> ().hit ();
				collider.gameObject.SendMessage("hit", attack);   //相手の"Damage"関数を呼び出す

			}
            if (collider.gameObject.tag == "shot") { //相手の弾をはじき返す為の試作。
                 //collider.gameObject.SendMessage("hit", attack);   //相手の"Damage"関数を呼び出す
				Destroy(collider.gameObject);
                }
                break;
		case 2:
			if (collider.gameObject.tag == "pino") {
				//collider.gameObject.GetComponent<Enemy> ().hit ();
				collider.gameObject.SendMessage("hit", attack);   //相手の"Damage"関数を呼び出す
				Player.GetComponent<hero2> ().Attack = true;
				Player.GetComponent<hero2> ().attackCount++;
				if (Player.GetComponent<hero2> ().SecondAttack == false) {
					Player.GetComponent<hero2> ().Attack = true;
				}

			}
			if (collider.gameObject.tag == "tamashibi") {
				collider.gameObject.GetComponent<Tamashibi_AI3> ().hit ();
				Player.GetComponent<hero2> ().Attack = true;
				Player.GetComponent<hero2> ().attackCount++;
				if (Player.GetComponent<hero2> ().SecondAttack == false) {
					Player.GetComponent<hero2> ().Attack = true;
				}

			}
			if (collider.gameObject.tag == "Kakashi") {
				collider.gameObject.SendMessage("hit", attack);   //相手の"Damage"関数を呼び出す
				if (Player.GetComponent<hero2> ().SecondAttack == false) {
					Player.GetComponent<hero2> ().Attack = true;
					Player.GetComponent<hero2> ().attackCount++;
				} else if(Player.GetComponent<hero2> ().SecondAttack == true){
					Player.GetComponent<hero2> ().attackCount++;
					Player.GetComponent<hero2> ().Attack = false;
				}
		
			}
			if (collider.gameObject.name == "boss") {
				//collider.gameObject.GetComponent<Enemy> ().hit ();
				collider.gameObject.SendMessage("hit", attack);   //相手の"Damage"関数を呼び出す
				Player.GetComponent<hero2> ().Attack = true;
				Player.GetComponent<hero2> ().attackCount++;
				if (Player.GetComponent<hero2> ().SecondAttack == false) {
					Player.GetComponent<hero2> ().Attack = true;
				}

			}
            if (collider.gameObject.tag == "shot")
                { //相手の弾をはじき返す為の試作。
                     //collider.gameObject.SendMessage("hit", attack);   //相手の"Damage"関数を呼び出す
				Destroy(collider.gameObject);
                }
                break;
		}
	}
}
