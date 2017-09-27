using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gallely : MonoBehaviour {
	//大枠のセット
	public GameObject  Character;
	public GameObject  manu;
	public GameObject  Credit;
	public GameObject  Story;

	public GameObject Pen;
	public GameObject Pen2;//ストーリーの項目用
	public GameObject shortstory;

	//キャラ
	public GameObject  hero;
	public GameObject  kakashi;
	public GameObject  tamashibi;
	public GameObject  pino;
	public GameObject  boss;
	//ui文字
	public GameObject  title;
	public GameObject  charaname;
	public GameObject  main;
	public GameObject  from;
	//public GameObject  ;


	private int action_type = 0;//アクションのランダム用
	private int select_type = 0; //モードセレクト用
	private int select_chapter = 0; //story内章選択用
	private float InputTime=0; //入力のクールタイム

	//キャラクター内のモード
	enum MovingMode{
		Hero,
		Kakashi,
		Tamashibi,
		Pino,
		Boss,
	};
	//ギャラリー内大枠のモード
	enum MovingSelect{
		MainManu,
		Character,
		Movie,
		Story,
		Credit,
	};

	MovingMode mode = MovingMode.Hero ;
	MovingSelect  select = MovingSelect.MainManu;
	// Use this for initialization
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {
		//デバッグ用後で消して
	
		if (Input.GetButtonDown ("Start")) {
			Application.LoadLevel ("Title");

		}
		 //Debug.Log (Input.GetAxis("Vertical"));

		//街頭キャラのモードになった時そのキャラが表示されるモード切り替えはupdate○○のinput参照
		switch (select) {
		case MovingSelect.Character:
			Character.SetActive (true);
			manu.SetActive (false);
			if (Input.GetKeyDown ("space") || Input.GetButtonDown ("Jump")) {
				select = MovingSelect.MainManu ;
				GetComponents<AudioSource> ()[1].Play ();
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
			case MovingMode.Boss:
				updateBoss ();
				break;
			}
			break;
		case MovingSelect.MainManu:
			Character.SetActive (false);
			Story.SetActive (false);
			Credit.SetActive (false);
			manu.SetActive (true);
			updateMainManu ();
			break;

		case MovingSelect.Story:
			if (Input.GetKeyDown ("space") || Input.GetButtonDown ("Jump")) {
				select = MovingSelect.MainManu ;
				GetComponents<AudioSource> ()[1].Play ();
			}
			Story.SetActive (true);
			manu.SetActive (false);
			updateStory ();
			break;
		case MovingSelect.Movie:
			Character.SetActive (false);
			manu.SetActive (false);
			updateMovie ();
			break;
		case MovingSelect.Credit:
			if (Input.GetKeyDown ("space") || Input.GetButtonDown ("Jump")) {
				select = MovingSelect.MainManu ;
				GetComponents<AudioSource> ()[1].Play ();
			}
			manu.SetActive (false);
			Credit.SetActive (true);
			updateCredit ();
			break;


		}
	}

	void updateMainManu(){
			//上下で羽ペンが移動する選択
		if (InputTime == 0) {
			
			if (Input.GetKeyDown ("up") || 1 == Input.GetAxisRaw ("Vertical")) {
				select_type--;
				if (select_type <= -1) {
					select_type = 3;
				}
			switch (select_type) {
				case 0:
					Pen.transform.position = new Vector2 (1.78f, 3.55f);
					InputTime = 100;
					GetComponents<AudioSource> () [0].Play ();
					break;
				case 1:
					Pen.transform.position = new Vector2 (1.78f, 2.15f);
					InputTime = 100;
					GetComponents<AudioSource> () [0].Play ();
					break;
				case 2:
					Pen.transform.position = new Vector2 (1.78f, 0.42f);
					InputTime = 100;
					GetComponents<AudioSource> () [0].Play ();
					break;
				case 3:
					Pen.transform.position = new Vector2 (1.78f, -3.64f);
					InputTime = 100;
					GetComponents<AudioSource> () [0].Play ();
					break;
				}
			}
		
			if (Input.GetKeyDown ("down") || -1 == Input.GetAxisRaw ("Vertical")) {
				select_type++;
				if (select_type >= 4) {
					select_type = 0;
				}
				switch (select_type) {
				case 0:
					Pen.transform.position = new Vector2 (1.78f, 3.55f);
					InputTime = 100;
					GetComponents<AudioSource> () [0].Play ();
					break;
				case 1:
					Pen.transform.position = new Vector2 (1.78f, 2.15f);
					InputTime = 100;
					GetComponents<AudioSource> () [0].Play ();
					break;
				case 2:
					Pen.transform.position = new Vector2 (1.78f, 0.42f);
					InputTime = 100;
					GetComponents<AudioSource> () [0].Play ();
					break;
				case 3:
					Pen.transform.position = new Vector2 (1.78f, -3.64f);
					InputTime = 100;
					GetComponents<AudioSource> () [0].Play ();
					break;
				}
			}
		}
		//連続入力できないようにクールタイムを設けている
		if (InputTime > 0) {
			if (Input.GetAxisRaw ("Vertical") == 0 && Input.GetAxisRaw ("Horizontal") == 0) {
				InputTime = 0;
			} else {
				InputTime -= 5;
			}
		}

		if (Input.GetKeyDown ("a")||Input.GetButtonDown ("Attack")) {
			switch (select_type) {
			case 0:
				select = MovingSelect.Story ;
				GetComponents<AudioSource> ()[1].Play ();
				break;
			case 1:
				select = MovingSelect.Character ;
				GetComponents<AudioSource> ()[1].Play ();
				break;
			case 2:
				//select = MovingSelect.Movie ;
				GetComponents<AudioSource> ()[1].Play ();
				break;
			case 3:
				select = MovingSelect.Credit ;
				GetComponents<AudioSource> ()[1].Play ();
				break;
			}

	
		}
	}

	void updateStory(){
		switch (select_chapter) {
		case 0:
			shortstory.GetComponent <Text> ().text = "冷たい雨が窓を打ち付ける\n長い夜が始まるようだ\n\nねぇあなた御本を読んでくれないかしら？\n\n……夜は怖い悪魔が来るのです\n早く眠りについてください\n朝日が貴女を祝福してくれるはずです\n\nつまらない騎士様ね\n私を寝かせたいのならあくびが出るくらい\n退屈な話でもしてくださいな";
			shortstory.GetComponent<Text>().fontSize= 43;
			break;
		case 1:
			shortstory.GetComponent <Text> ().text = "青い鳥-あらすじ-\nあるところに仲のいい兄妹がいました\n\r親もいなく決して豊かではありませんでしたが\n二人は自分たちが不幸だとは思っていませんでした\nそんな二人のところに吟遊詩人がやってきて言います\n君たちが心の底から幸せだと思っているのなら\n青い鳥がやってきて君たちの願いをなんでも1つ\n叶えてくれるだろう\nさぁ森へお行き。こんなところに篭っていては\n青い鳥もやっては来れないさ\n兄妹は森へ向います\n二人には叶えてもらいたい\n願いがあるのですから……\n";
			shortstory.GetComponent<Text>().fontSize= 33;
			break;
		case 2:

			break;
		case 3:

			break;
		case 4:
	
			break;

		}

		//上下で羽ペンが移動する選択
		if (InputTime == 0) {
			if (Input.GetKeyDown ("up") || 1 == Input.GetAxisRaw ("Vertical")) {
				select_chapter--;
				if (select_chapter <= -1) {
					select_chapter = 4;
				}
				switch (select_chapter) {
				case 0:
					Pen2.transform.position = new Vector2 (-8.6f, 2.99f);
					InputTime = 100;
					GetComponents<AudioSource> () [0].Play ();
					break;
				case 1:
					Pen2.transform.position = new Vector2 (-8.93f, 1.67f);
					InputTime = 100;
					GetComponents<AudioSource> () [0].Play ();
					break;
				case 2:
					Pen2.transform.position = new Vector2 (-8.93f, 0.22f);
					InputTime = 100;
					GetComponents<AudioSource> () [0].Play ();
					break;
				case 3:
					Pen2.transform.position = new Vector2 (-8.93f, -1.2f);
					InputTime = 100;
					GetComponents<AudioSource> () [0].Play ();
					break;
				case 4:
					Pen2.transform.position = new Vector2 (-8.93f, -2.53f);
					InputTime = 100;
					GetComponents<AudioSource> () [0].Play ();
					break;

				}
			}

			if (Input.GetKeyDown ("down") || -1 == Input.GetAxisRaw ("Vertical")) {
				select_chapter++;
				if (select_chapter >= 5) {
					select_chapter = 0;
				}
				switch (select_chapter) {
				case 0:
					Pen2.transform.position = new Vector2 (-8.93f, 2.99f);
					InputTime = 100;
					GetComponents<AudioSource> () [0].Play ();
					break;
				case 1:
					Pen2.transform.position = new Vector2 (-8.93f, 1.67f);
					InputTime = 100;
					GetComponents<AudioSource> () [0].Play ();
					break;
				case 2:
					Pen2.transform.position = new Vector2 (-8.93f, 0.22f);
					InputTime = 100;
					GetComponents<AudioSource> () [0].Play ();
					break;
				case 3:
					Pen2.transform.position = new Vector2 (-8.93f, -1.2f);
					InputTime = 100;
					GetComponents<AudioSource> () [0].Play ();
					break;
				case 4:
					Pen2.transform.position = new Vector2 (-8.93f, -2.53f);
					InputTime = 100;
					GetComponents<AudioSource> () [0].Play ();
					break;

				}
			}
		}
		//連続入力できないようにクールタイムを設けている
		if (InputTime > 0) {
			if (Input.GetAxisRaw ("Vertical") == 0 && Input.GetAxisRaw ("Horizontal") == 0) {
				InputTime = 0;
			} else {
				InputTime -= 5;
			}
		}
	}
	void updateMovie(){}
	void updateCredit(){}



	//以下はギャラリーキャラクターの該当キャラの動き
	void updateHero(){
		hero.SetActive (true);
		kakashi.SetActive (false);
		boss.SetActive (false);
		title.GetComponent <Text>().text = "Gallely:Character:Hero";
		charaname.GetComponent <Text>().text = "レヴナント";
		main.GetComponent <Text>().text = "この物語の主人公で記憶喪失の青年\n彼の求める記憶とは一体\nなんなのだろうか…";
		from.GetComponent <Text>().text = "出会えるところ：Childishi Knight";

		if (InputTime == 0) {
			
			if (Input.GetKeyDown ("p") || 1 == Input.GetAxisRaw ("Horizontal")) {
				mode = MovingMode.Kakashi;
				InputTime = 100;
				GetComponents<AudioSource> ()[0].Play ();
			} else if (Input.GetKeyDown ("q") || -1 == Input.GetAxisRaw ("Horizontal")) {
				mode = MovingMode.Boss;
				InputTime = 100;
				GetComponents<AudioSource> ()[0].Play ();
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
				GetComponents<AudioSource> ()[0].Play ();
			} else if (Input.GetKeyDown ("q") || -1 == Input.GetAxisRaw ("Horizontal")) {
				mode = MovingMode.Hero;
				InputTime = 100;
				GetComponents<AudioSource> ()[0].Play ();
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
				GetComponents<AudioSource> ()[0].Play ();
			} else if (Input.GetKeyDown ("q") || -1 == Input.GetAxisRaw ("Horizontal")) {
				mode = MovingMode.Kakashi;
				InputTime = 100;
				GetComponents<AudioSource> ()[0].Play ();
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
		boss.SetActive (false);
		title.GetComponent <Text>().text = "Gallely:Character:Enemy";
		charaname.GetComponent <Text>().text = "ピノキオ";
		main.GetComponent <Text>().text = "嘘を吐くと鼻が伸びるらしい存在\n嘘一つとて人を守るための嘘と\n苦しめる嘘がある\n彼らはきっとその嘘で\n苦しみを与えてしまったのだろう\n鼻の先が血に染まる\nかつて見たあの人の姿はもう見えない\n……別に鼻の先で人を刺すことは\nしないのでご安心を";
		from.GetComponent <Text>().text = "出会えるところ：Chapter1 森の中";
		if (InputTime == 0) {
			
			if (Input.GetKeyDown ("p") || 1 == Input.GetAxisRaw ("Horizontal")) {
				mode = MovingMode.Boss;
				InputTime = 100;
				GetComponents<AudioSource> ()[0].Play ();
			} else if (Input.GetKeyDown ("q") || -1 == Input.GetAxisRaw ("Horizontal")) {
				mode = MovingMode.Tamashibi;
				InputTime = 100;
				GetComponents<AudioSource> ()[0].Play ();
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
	void updateBoss(){
		boss.SetActive (true);
		pino.SetActive (false);
		hero.SetActive (false);
		title.GetComponent <Text>().text = "Gallely:Character:Boss";
		charaname.GetComponent <Text>().text = "青い鳥"; 
		main.GetComponent <Text>().text = "ある兄妹は一つの結論にたどり着いた\n幸福とは一人では得られない\n他者との比較が幸福の源であると\n『優越感』こそが幸福の正体であると\nだから兄は\n妹の世話をして過ごすことにした\n妹は\n兄の時間を奪い過ごすことにした\n青い鳥はその兄妹を\n紅に染まった瞳で見つめていた";
		from.GetComponent <Text>().text = "出会えるところ：Chapter1 森の最奥";
		if (InputTime == 0) {

			if (Input.GetKeyDown ("p") || 1 == Input.GetAxisRaw ("Horizontal")) {
				mode = MovingMode.Hero;
				InputTime = 100;
				GetComponents<AudioSource> ()[0].Play ();
			} else if (Input.GetKeyDown ("q") || -1 == Input.GetAxisRaw ("Horizontal")) {
				mode = MovingMode.Pino;
				InputTime = 100;
				GetComponents<AudioSource> ()[0].Play ();
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
				boss.GetComponent<Animator> ().SetTrigger ("attack1");
			} else if (action_type > 0) {
				boss.GetComponent<Animator> ().SetTrigger ("attack2");
			}
		}
	}
}
