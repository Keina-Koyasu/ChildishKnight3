using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gallely : MonoBehaviour {
	//大枠のセット
	public GameObject  Character;
	public GameObject  manu;

	public GameObject Pen;

	//キャラ
	public GameObject  hero;
	public GameObject  kakashi;
	public GameObject  tamashibi;
	public GameObject  pino;
	//ui文字
	public GameObject  title;
	public GameObject  charaname;
	public GameObject  main;
	public GameObject  from;
	//public GameObject  ;
	bool CharacterSelect = true;
//	bool InputCooltime = false;
	bool once = false;

	private int action_type = 0;
	private float InputTime=0;

	//キャラクター内のモード
	enum MovingMode{
		Hero,
		Kakashi,
		Tamashibi,
		Pino,
	};
	//ギャラリー内大枠のモード
	enum MovingSelect{
		MainManu,
		Character,
		Movie,
	};

	MovingMode mode = MovingMode.Hero ;
	MovingSelect  select = MovingSelect.MainManu;
	// Use this for initialization
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {
		//デバッグ用後で消して
		if(Input.GetKeyDown("l")){
			select = MovingSelect.Character ;
		}
		if(Input.GetKeyDown("k")){
			select = MovingSelect.MainManu ;
		}

		Debug.Log (Input.GetAxis("Vertical"));

		//街頭キャラのモードになった時そのキャラが表示されるモード切り替えはupdate○○のinput参照
		switch (select) {
		case MovingSelect.Character:
			Character.SetActive (true);
			manu.SetActive (false);
			if (Input.GetKeyDown ("space") || Input.GetButtonDown ("Jump")) {
				select = MovingSelect.MainManu ;
			}

			switch (mode) {
			case MovingMode.Hero:
				updateHero ();
				break;
			case MovingMode.Kakashi:
				updateKakashi ();
				break;
			case MovingMode.Tamashibi:
				updateTamashibi ();
				break;
			case MovingMode.Pino:
				updatePino ();
				break;
			}
			break;
		case MovingSelect.MainManu:
			Character.SetActive (false);
			manu.SetActive (true);
			updateMainManu ();
			break;
		}
	}

	void updateMainManu(){
			//上下で羽ペンが移動する選択

		if (InputTime == 0) {
			
			if (Input.GetKeyDown ("up") || 1 == Input.GetAxisRaw ("Vertical")) {
				if (CharacterSelect) {
					Pen.transform.position = new Vector2 (1.44f, -0.95f);
					CharacterSelect = false;
					InputTime = 100;

				} else if (CharacterSelect == false) {
					Pen.transform.position = new Vector2 (1.44f, 2.22f);
					CharacterSelect = true;
					InputTime = 100;
				}
			}
			if (Input.GetKeyDown ("down") || -1 == Input.GetAxisRaw ("Vertical")) {
				if (CharacterSelect) {
					Pen.transform.position = new Vector2 (1.44f, -0.95f);
					CharacterSelect = false;
					InputTime = 100;
				} else if (CharacterSelect == false) {
					Pen.transform.position = new Vector2 (1.44f, 2.22f);
					CharacterSelect = true;
					InputTime = 100;
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

		if (Input.GetKeyDown ("a")||Input.GetButtonDown ("Attack")) {
			if(CharacterSelect){
				select = MovingSelect.Character ;

			}else if(CharacterSelect==false){
				select = MovingSelect.Movie ;
			}
		}
	}


	//以下はギャラリーキャラクターの該当キャラの動き
	void updateHero(){
		hero.SetActive (true);
		kakashi.SetActive (false);
		pino.SetActive (false);
		title.GetComponent <Text>().text = "Gallely:Character:Hero";
		charaname.GetComponent <Text>().text = "レヴナント";
		main.GetComponent <Text>().text = "この物語の主人公で記憶喪失の青年\n彼の求める記憶とは一体\nなんなのだろうか…";
		from.GetComponent <Text>().text = "出会えるところ：Childishi Knight";

		if (InputTime == 0) {
			
			if (Input.GetKeyDown ("p") || 1 == Input.GetAxisRaw ("Horizontal")) {
				mode = MovingMode.Kakashi;
				InputTime = 100;
			} else if (Input.GetKeyDown ("q") || -1 == Input.GetAxisRaw ("Horizontal")) {
				mode = MovingMode.Pino;
				InputTime = 100;
			}
		}
		if (InputTime > 0) {
			if (Input.GetAxisRaw ("Vertical") == 0 && Input.GetAxisRaw ("Horizontal") == 0) {
				InputTime = 0;
			} else {
				InputTime -= 5;
			}
		}
		if (Input.GetKeyDown ("a")||Input.GetButtonDown ("Attack")) {
			action_type = Random.Range(0,7);
			if (action_type > 5) {
				hero.GetComponent<Animator> ().SetTrigger ("attack");
			} else if (action_type > 4) {
				hero.GetComponent<Animator> ().SetTrigger ("attack2");
			} else if (action_type > 3) {
			hero.GetComponent<Animator> ().SetTrigger ("jump");
			}else if (action_type > 2) {
				hero.GetComponent<Animator> ().SetTrigger ("Dodge");
			}else if (action_type > 1) {
				hero.GetComponent<Animator> ().SetTrigger ("Dead");
			}else if (action_type > 0) {
				hero.GetComponent<Animator> ().SetTrigger ("walk");
			}
		}

	}
	void updateKakashi(){
		kakashi.SetActive (true);
		hero.SetActive (false);
		tamashibi.SetActive (false);
		title.GetComponent <Text>().text = "Gallely:Character:Enemy";
		charaname.GetComponent <Text>().text = "カカシくん";
		main.GetComponent <Text>().text = "森の案内人。看板は彼が持参したもの\n攻撃のやり方を教えてくれるが\nその攻撃に巻き込まれ\n命を落とすこともしばしば\n棒部分の先端で敵を刺すことが\n出来るとか出来ないとか……\n物事を教えることが善意なのか\n悪意なのかは彼のみぞ知る\n";
		from.GetComponent <Text>().text = "出会えるところ：Chapter1 森の入り口";
		if (InputTime == 0) {

			if (Input.GetKeyDown ("p") || 1 == Input.GetAxisRaw ("Horizontal")) {
				mode = MovingMode.Tamashibi;
				InputTime = 100;
			} else if (Input.GetKeyDown ("q") || -1 == Input.GetAxisRaw ("Horizontal")) {
				mode = MovingMode.Hero;
				InputTime = 100;
			}
		}
		if (InputTime > 0) {
			if (Input.GetAxisRaw ("Vertical") == 0 && Input.GetAxisRaw ("Horizontal") == 0) {
				InputTime = 0;
			} else {
				InputTime -= 5;
			}
		}

		if (Input.GetKeyDown ("a") || Input.GetButtonDown ("Attack")) {
			kakashi.GetComponent<Animator> ().SetTrigger ("die");
		}

	}
	void updateTamashibi(){
		tamashibi.SetActive (true);
		kakashi.SetActive (false);
		pino.SetActive (false);
		title.GetComponent <Text>().text = "Gallely:Character:Enemy";
		charaname.GetComponent <Text>().text = "タマシビ";
		main.GetComponent <Text>().text = "彷徨える魂の片鱗\nおどろおどろしい喜びの顔と\n悲しみの顔を見せ、襲いかかってくる\n彷徨える魂は在るべきところへ\n安らかに眠らせてあげよう";
		from.GetComponent <Text>().text = "出会えるところ：Chapter1 森の中";
		if (InputTime == 0) {
			if (Input.GetKeyDown ("p") || 1 == Input.GetAxisRaw ("Horizontal")) {
				mode = MovingMode.Pino;
				InputTime = 100;
			} else if (Input.GetKeyDown ("q") || -1 == Input.GetAxisRaw ("Horizontal")) {
				mode = MovingMode.Kakashi;
				InputTime = 100;
			}
		}
		if (InputTime > 0) {
			if (Input.GetAxisRaw ("Vertical") == 0 && Input.GetAxisRaw ("Horizontal") == 0) {
				InputTime = 0;
			} else {
				InputTime -= 5;
			}
		}

		if (Input.GetKeyDown ("a")||Input.GetButtonDown ("Attack")) {
			tamashibi.GetComponent<Animator> ().SetTrigger ("attack");
		}

	}
	void updatePino(){
		pino.SetActive (true);
		tamashibi.SetActive (false);
		hero.SetActive (false);
		title.GetComponent <Text>().text = "Gallely:Character:Enemy";
		charaname.GetComponent <Text>().text = "ピノキオ";
		main.GetComponent <Text>().text = "嘘を吐くと鼻が伸びるらしい存在\n嘘一つとて人を守るための嘘と\n苦しめる嘘がある\n彼らはきっとその嘘で\n苦しみを与えてしまったのだろう\n鼻の先が血に染まる\nかつて見たあの人の姿はもう見えない\n……別に鼻の先で人を刺すことは\nしないのでご安心を";
		from.GetComponent <Text>().text = "出会えるところ：Chapter1 森の中";
		if (InputTime == 0) {
			
			if (Input.GetKeyDown ("p") || 1 == Input.GetAxisRaw ("Horizontal")) {
				mode = MovingMode.Hero;
				InputTime = 100;
			} else if (Input.GetKeyDown ("q") || -1 == Input.GetAxisRaw ("Horizontal")) {
				mode = MovingMode.Tamashibi;
				InputTime = 100;
			}
		}
		if (InputTime > 0) {
			if (Input.GetAxisRaw ("Vertical") == 0 && Input.GetAxisRaw ("Horizontal") == 0) {
				InputTime = 0;
			} else {
				InputTime -= 5;
			}
		}

		if (Input.GetKeyDown ("a")||Input.GetButtonDown ("Attack")) {
			action_type = Random.Range(0,4);
			if (action_type > 2) {
				pino.GetComponent<Animator> ().SetTrigger ("attack");
			} else if (action_type > 0) {
				pino.GetComponent<Animator> ().SetTrigger ("die");
			}
		}
	}

}
