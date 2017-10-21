using UnityEngine;
using UnityEngine.UI;   // UIを使います。
using System.Collections.Generic;
using System.Collections;

public class hero2 : MonoBehaviour {
	public GameObject mainCamera;
	public GameObject attackEF; //攻撃エフェクト　トリガーの関係で出したり消したりする 
	public GameObject attackEF2; //攻撃エフェクト　トリガーの関係で出したり消したりする 
	public GameObject airattackEF;
	public GameObject DamegeEF; //ダメージ喰らったときのパーティクル
	public GameObject RevivalEF;//復活の際のエフェクト
	public GameObject text;  // UIのレベル表示

	//ボイス音関連
	AudioSource audioSource;  // ここコピペ
	public List<AudioClip> audioClip = new List<AudioClip>();  // ここコピペ
	private int voice=0;//ランダムで再生されるボイス用int Random.Range(0,2)などintは最大数が含まれない


	public float speed = 10f; //歩くスピード
	public float jumpPower = 200; //ジャンプ力
	public LayerMask groundLayer; //Linecastで判定するLayer
	public LayerMask slopLayer; //Linecastで判定するLayer

	//ダメージ処理用
	public float maxLife = 100;    //最大体力（readonlyは変数の変更ができなくなるらしい）
	public float Life = 100;    //現在体力
	private bool isdamage=false;

	//待機モーションとかよう
	private float idlelimit=10f; //十秒間操作がなかったらとか
	private bool ismoving=false;//動いている状態



	//レベルアップと攻撃カウント
	public int level = 1 ;
	public float EXP =0;
	public int attackCount = 0 ;//攻撃がヒットするとあがる　攻撃１段目と２段目の切り替え用
	public bool SecondAttack =false; //レベル2以降の２段目の攻撃を出したか否か

	private Rigidbody2D rigidbody2D;
	private Animator anim;

	//private Renderer renderer; //ダメージ処理のために仮で作っとく

	//public bool cooltime = true;
	private bool footmusic; //足音用falseで音がなっている状態
	private bool isGrounded; //着地判定

	public bool Attack = true; //攻撃可能
	public bool move = true;


	void Start () {
		//各コンポーネントをキャッシュしておく
		audioSource = gameObject.AddComponent<AudioSource>();  // ここコピペ
		anim = GetComponent<Animator>();
		rigidbody2D = GetComponent<Rigidbody2D>();
		rigidbody2D.fixedAngle = true;
		//renderer = GetComponent<Renderer>(); //ダメージ処理のために
		Life = maxLife; //体力を全回復させる
	}




	void attack1(){
		voice = Random.Range(0,2);
		if (voice == 0) {
		audioSource.PlayOneShot(audioClip[0]);  //ボイス
		}else{
			audioSource.PlayOneShot(audioClip[1]);  //ボイス
		}
		anim.SetBool ("wolk 0", false);
		anim.SetTrigger ("attack");
		attackEF.SetActive (true);
		attackEF.GetComponent<Animator> ().SetTrigger ("attack");
		attackEF.GetComponent<attack_judg2> ();
		Invoke ("ATKEFDL", 0.7f); //0.25f後にアタックエフェクトを消す
		Attack= false;
	}

	void attack2(){
		audioSource.PlayOneShot(audioClip[2]);  //ボイス
		anim.SetBool ("wolk 0", false);
		anim.SetTrigger ("attack2");
		attackEF2.SetActive (true);
		attackEF2.GetComponent<Animator> ().SetTrigger ("yari");
		attackEF2.GetComponent<attack_judg2> ();
		Invoke ("ATKEFDL2", 0.7f); //0.25f後にアタックエフェクトを消す
		Attack= false;
		SecondAttack = true;
	}

	void ATKEFDL (){ //animation終わり後に消す目的
		attackEF.SetActive (false);
		airattackEF.SetActive (false);
		Attack = true;
		attackCount = 0;
		ismoving = false;

	}
	void ATKEFDL2 (){ //animation終わり後に消す目的
		attackEF2.SetActive (false);
		Attack = true;
		SecondAttack = false;
		attackCount = 0;
		ismoving = false;
	}

	//ここは足音に関する項目足音の多重入力を阻止するため一瞬だけplayして止まったら音を消す。空中に行った時も音を消す。
	//foolmusicがfalseになっているときは音がなっている状態。こんなことするもの全てはパッドスティックのせい
	void Footmusic(){
		if(footmusic&&isGrounded){
			GetComponent<AudioSource> ().Play();
			footmusic = false;
		}
		if(footmusic==false&&isGrounded==false){
			GetComponent<AudioSource> ().Stop();
			footmusic = true;
		}
	}


	//public void Damage (float damage) {
	//}
	public void hit(float damage){

		isdamage = true;
		Life -= damage; //体力を減らす
		anim.SetTrigger ("damage");
		anim.SetBool ("airattack",false);
		// プレイヤーの位置を後ろに飛ばす

		float s = 5f * Time.deltaTime;
		transform.Translate(Vector3.up * s);

		// プレイヤーのlocalScaleでどちらを向いているのかを判定
		if(transform.localScale.x >= 0){
			transform.Translate(Vector3.left * s);
		}else{
			transform.Translate(Vector3.right * s);
		}

		 //ダメージを受けた時にライフが〜
		if (Life > 0) {
			if(Life>30){
			audioSource.PlayOneShot(audioClip[6]);  //ボイス
			}else if(Life<31){
				audioSource.PlayOneShot(audioClip[7]);  //ボイス
			}
			//StartCoroutine ("Damage");
			DamegeEF.SetActive (true);
			Invoke ("muteki", 2.5f);
			//レイヤーをPlayerDamageに変更
			gameObject.layer = LayerMask.NameToLayer ("PlayerDamage");
		} else {
			if (Life < 0) {
				voice = Random.Range(0,2);
				if (voice == 0) {
					audioSource.PlayOneShot(audioClip[10]);  //ボイス
				}else{
					audioSource.PlayOneShot(audioClip[11]);  //ボイス
				}

				StartCoroutine ("Dead");
			}
		}
	}
	public void EXPin(float xp){
		EXP += xp;
	}
	IEnumerator Dead(){
		Time.timeScale = 0.25f;
		anim.SetTrigger("damage");
		gameObject.layer = LayerMask.NameToLayer ("PlayerDamage");
		yield return new WaitForSeconds(0.01f);
		anim.SetTrigger ("Dead");
		move = false;
		yield return new WaitForSeconds(0.20f);
		Time.timeScale = 1.0f;
		yield return new WaitForSeconds(0.8f);
		RevivalEF.SetActive (true);
		yield return new WaitForSeconds(2.30f);
		RevivalEF.SetActive (false);
		anim.SetTrigger ("Revival");
		gameObject.layer = LayerMask.NameToLayer("Character");
		move = true;
		maxLife += 30f;
		Life = maxLife;
		isdamage = false;
		voice = Random.Range(0,2);
		if (voice == 0) {
			audioSource.PlayOneShot(audioClip[12]);  //ボイス
		}else{
			audioSource.PlayOneShot(audioClip[13]);  //ボイス
		}

	
	}
	/*
	IEnumerator Damage ()
	{
		
		//レイヤーをPlayerDamageに変更
		gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
		//while文を7回ループ
		int count = 7;
		while (count > 0){
			//透明にする
			renderer.material.color = new Color (1,1,1,0);
			//0.05秒待つ
			yield return new WaitForSeconds(0.05f);
			//元に戻す
			renderer.material.color = new Color (1,1,1,1);
			//0.05秒待つ
			yield return new WaitForSeconds(0.05f);
			count--;
		
		}



	}
	*/
	void muteki(){
		//レイヤーをCharacterに戻す
		gameObject.layer = LayerMask.NameToLayer("Character");
		Attack = true;
        move = true;
		DamegeEF.SetActive (false);
		isdamage = false;
		ismoving=false; //動いてない扱い
		//isGrounded = true;
	}
	void Update ()
	{
		//レベルアップ
		if (EXP > 99) {
            //level++;
            level = 2;
			text.GetComponent <Text>().text = "Ⅱ";
			EXP = 0;
			audioSource.PlayOneShot(audioClip[15]);  //ボイス
		}
		//デバッグ用コード
		if(Input.GetKeyDown("p")){
			EXP += 100f;
		}
		if(Input.GetKeyDown("o")){
			StartCoroutine ("Dead");
		}
		if(Input.GetKeyDown("l")){
		transform.position=new Vector3(378.4f,15f,0f);
		}

		if (Input.GetButtonDown ("Start")) {
			Application.LoadLevel ("Title");

		}

		if(isGrounded){//地上にいる時
			anim.SetBool ("airattack", false);
		}


		//何もしてない状態を作る
		if(ismoving==false){
			idlelimit -=Time.deltaTime;
		}else if(ismoving==true){
			idlelimit=10f;
		}
		if(idlelimit<1){
			audioSource.PlayOneShot(audioClip[14]);  //ボイス
			idlelimit=10f;
		}
		//Debug.Log (idlelimit);


		//moveがtrueのときに移動とか攻撃とかできる
		if(move){
		if (-1 == Input.GetAxisRaw ("Horizontal") || Input.GetKey ("left")) {
				ismoving=true;
			rigidbody2D.velocity = new Vector2 (-1 * speed, rigidbody2D.velocity.y);
			Vector2 temp = transform.localScale;
			temp.x = -1;
			transform.localScale = temp;
			if (isGrounded)
				anim.SetBool ("wolk 0", true);
				Footmusic ();
		} else if(1 == Input.GetAxisRaw ("Horizontal") || Input.GetKey ("right")){
				ismoving=true;
				rigidbody2D.velocity = new Vector2 (1 * speed, rigidbody2D.velocity.y);
			Vector2 temp = transform.localScale;
			temp.x = 1;
			transform.localScale = temp;
			if (isGrounded)
				anim.SetBool ("wolk 0", true);
				Footmusic ();
				
		} else {
			anim.SetBool ("wolk 0", false);
				GetComponent<AudioSource> ().Stop();
				footmusic = true;
				ismoving=false;

		}
		//回避
		if(Input.GetKeyDown("w")&&Attack||Input.GetButtonDown ("Douge")&&Attack){
				voice = Random.Range(0,2);
				ismoving=true;
				if (voice == 0) {
					audioSource.PlayOneShot(audioClip[8]);  //ボイス
				}else{
					audioSource.PlayOneShot(audioClip[9]);  //ボイス
				}

				//横にスライド
			// プレイヤーのlocalScaleでどちらを向いているのかを判定
				Attack = false;
                move = false;

				GetComponent<AudioSource> ().Stop();
				
            rigidbody2D.velocity = new Vector2(2.5f * speed * transform.localScale.x, rigidbody2D.velocity.y);
                    anim.SetTrigger("Dodge");
				gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
				Invoke ("muteki", 0.7f);
			}

		//Linecastで主人公の足元に地面があるか判定
		isGrounded = Physics2D.Linecast (
			transform.position + transform.up * 1,
			transform.position - transform.up * 3.75f,
			groundLayer);

         
		//スペースキーを押し、
		if (Input.GetKeyDown ("space")||Input.GetButtonDown ("Jump")) {
				ismoving=true;
			//着地していた時、
			if (isGrounded) {
					audioSource.PlayOneShot(audioClip[5]);  //ボイス
				//Dashアニメーションを止めて、Jumpアニメーションを実行
				anim.SetBool("wolk 0", false);
				anim.SetTrigger("jump");
				//着地判定をfalse
				isGrounded = false;
				//rigidbody2D.gravityScale = 0f;
				//Invoke ("jumping", 0.15f);
				//AddForceにて上方向へ力を加える
				rigidbody2D.AddForce (Vector2.up * jumpPower);
			}
		}
		//攻撃
		if (Input.GetKeyDown ("q")||Input.GetButtonDown ("Attack")) {
				ismoving = true;
				if (isGrounded && Attack&& SecondAttack==false) {
					
				switch( level ){
				case 1:
					attack1();
					break;
				case 2:
					if( attackCount % 2 == 0 ){
						attack1();
					}
					if( attackCount % 2 == 1 ){
						attack2();
					}
					//attackCount ++ ;
					break;
				case 3:
					if( attackCount % 3 == 0 ){
						attack1();
					}
					if( attackCount % 3 == 1 ){
						attack2();
					}
					if( attackCount % 3 == 3 ){
						attack2();
					}
					//attackCount ++ ;
					break;
				}	
			
				}else if(isGrounded==false&&Attack){
					
					voice = Random.Range(0,2);
					if (voice == 0) {
						audioSource.PlayOneShot(audioClip[3]);  //ボイス
					}else{
						audioSource.PlayOneShot(audioClip[4]);  //ボイス
					}

					anim.SetBool ("airattack",  true);
					anim.SetBool ("wolk 0", false);
					airattackEF.SetActive (true);
					airattackEF.GetComponent<Animator> ().SetTrigger ("attack");
					airattackEF.GetComponent<attack_judg2> ();
					Invoke ("ATKEFDL", 0.7f); //0.25f後にアタックエフェクトを消す
					Attack= false;
				}
		}
		//上下への移動速度を取得
		float velY = rigidbody2D.velocity.y;
		//移動速度が0.1より大きければ上昇
		bool isJumping = velY > 0.1f ? true:false;
		//移動速度が-0.1より小さければ下降
		bool isFalling = velY < -0.1f ? true:false;
		//結果をアニメータービューの変数へ反映する
		anim.SetBool("isjumping",isJumping);
		anim.SetBool("isfalling",isFalling);

	}
	//↑moveの中身
	}


	void FixedUpdate ()
	{}
}