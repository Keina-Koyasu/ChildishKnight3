using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kakashi : MonoBehaviour {
	public GameObject message;
	public GameObject hero;
	public GameObject text;
	public GameObject hiteffect;

	public float maxLife = 30;    //最大体力（readonlyは変数の変更ができなくなるらしい）
	public float Life = 30;    //現在体力

	public float xp = 0;


	public int textcount=0;

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}

	public void hit(float damage){
		Life -= damage; //体力を減らす
		hiteffect.SetActive (true);
		Invoke ("hitef",0.8f);
		if (Life > 0) {
			//gameObject.layer = LayerMask.NameToLayer ("PlayerDamage");
			if(this.gameObject.name=="BossKakashi"){
				textcount ++;
				switch(textcount){
				case 1:
						text.GetComponent<TextMesh> ().text = "幸福ってどこにあると思う？\n知らないことって幸せ？\n分からなくなってきたよ\n";	
						break;
				case 2:
					text.GetComponent<TextMesh> ().text = "幸福の象徴たる青い鳥\n彼らは何を以て\n幸福を判断したんだろうね";
						break;
				case 3:
					text.GetComponent<TextMesh> ().text = "君は今、ボクを傷つけている\n無抵抗な案内人を傷つけて\n言の葉のその先を知ろうとしてる";
						break;
				case 4:
					text.GetComponent<TextMesh> ().text = "でもボクは何も知らないよ\nボクは名もなきカカシだからね\n解のない謎掛けをして\n君を惑わせているのさ";
					break;
				case 5:
					text.GetComponent<TextMesh> ().text = "アハハハハハハ\nさぁ先に行こうか";
					break;
				case 6:
					text.GetComponent<TextMesh> ().text = "………………………";
					break;
				}
			}
			//Invoke ("muteki", 2.5f);
		} else {
			if (Life < 0) {
				anim.SetTrigger ("die");
				Invoke ("dead", 0.8f);
				GetComponent<AudioSource> ().Play ();
			}
		}
	}
	void hitef(){
		hiteffect.SetActive (false);
		//レイヤーをCharacterに戻す
		//gameObject.layer = LayerMask.NameToLayer("Enemy");
		//isGrounded = true;
	}
		
	void dead(){
		GetComponent<AudioSource> ().Stop ();
		Destroy(this.gameObject);
		hero.gameObject.SendMessage("EXPin", xp); 

	}
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "Player") {
			//StartCoroutine ("messagein");

		}
	}
	//IEnumerator messagein(){
		
	//}
	// Update is called once per frame
	void Update () {
		
	}
}

