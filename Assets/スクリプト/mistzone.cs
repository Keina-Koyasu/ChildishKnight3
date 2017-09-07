using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mistzone : MonoBehaviour {
	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "Player") {
			//StartCoroutine ("messagein");
			player.transform.position=new Vector3(167.5f,11.5f,0f);
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
