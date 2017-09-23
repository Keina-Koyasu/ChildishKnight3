using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public float attack = 20f;  //攻撃力
	public float speed = 1.0f;
	//public GameObject hero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GameObject hero = GameObject.Find("hero");

		float step = Time.deltaTime * speed;
		transform.position = Vector3.MoveTowards(transform.position, hero.transform.position, step);
	}

	void OnTriggerEnter2D(Collider2D collider) { 
		//Debug.Log (collider.gameObject.tag);
		string layerName = LayerMask.LayerToName(collider.gameObject.layer);

		if (collider.gameObject.tag == "Player") {
			if( layerName == "PlayerDamage"){
				Destroy(this.gameObject);
			}else if( layerName == "Character"){
			//collider.gameObject.GetComponent<Enemy> ().hit ();
			collider.gameObject.SendMessage("hit", attack);   //相手の"Damage"関数を呼び出す
			Destroy(this.gameObject);
			//hit.gameObject.SendMessage("Damage", attack);   //相手の"Damage"関数を呼び出す
		}
		}
	}
}
