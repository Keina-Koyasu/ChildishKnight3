using UnityEngine;
using System.Collections;

public class boss_AI : MonoBehaviour {

	public GameObject firePrefab;
	public GameObject ATJ;
	public GameObject ATJ2;
	public GameObject hero;
	public GameObject hiteffect;


	private int shot_count=0; //何個弾を打ったか
	private int action_type = 0;
	private Vector3 forward;

	private bool isIdle = true;
	//private bool isAttack = false;
	private bool isWalk = false;
	private bool isJump = false;

	private Animator anim;


	public float maxLife = 500;    //最大体力（readonlyは変数の変更ができなくなるらしい）
	public float Life = 500;    //現在体力


	// JumpParams
	private float jumpPowor;
	public float jumpPoworConst = 0.8f;
	public float jumpGrvity = 0.05f;

	public float xp = 10;

	private float action_time = 0;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();


	}
	void ATJA(){
		ATJ.SetActive (true);
	}

	//attack_judge2からsendmasagemで呼ばれる
	public void hit(float damage){
		Life -= damage; //体力を減らす
		if (Life > 0) {
			StartCoroutine("hitef");
		} else {
			if (Life < 0) {
				anim.SetTrigger ("die");
				StartCoroutine("Dead");

			}
		}
	}
	IEnumerator hitef(){
		//anim.SetTrigger ("die");
		hiteffect.SetActive (true);
		yield return null;
		yield return new WaitForSeconds(0.7f);
		hiteffect.SetActive (false);
	}
	IEnumerator Dead(){
		//GetComponent<AudioSource> ().Play ();
		ATJ.GetComponent<BoxCollider2D>().enabled = false;
		GetComponent<ParticleSource> ().particle ();
		hiteffect.SetActive (true);
		//anim.SetTrigger ("die");
		yield return null;
		yield return new WaitForSeconds(0.4f);
		GetComponent<AudioSource> ().Play ();
		GetComponent<SpriteRenderer>().enabled = false;
		hiteffect.SetActive (false);
		yield return null;
		yield return new WaitForSeconds(1.00f);
		hero.gameObject.SendMessage("EXPin", xp); 
		GetComponent<AudioSource> ().Stop ();

		Destroy(this.gameObject);


	}

	// Update is called once per frame
	void Update () {

		//アイドル状態
		if(isIdle){
		//アクティブタイムバトル風に時間で行動させる
			action_time += Time.deltaTime;
			if(action_time>3){

				Vector3 Apos = hero.transform.position;
				Vector3 Bpos = this.transform.position;
				float dis = Vector3.Distance(Apos,Bpos);
				Debug.Log("Distance : " + dis);
				action_time = 0;
				if (dis < 19 && dis > 9) {
					action_type = 1;
				} else if (dis > 18) {
					action_type = 3;
				}
			//ランダムでモーション種類基準となる番号を取得
				//action_type = Random.Range(1, 2);


			}
			//Debug.Log(action_time);
			// 歩くフラグが立っていたら
		
		}
		/*
		else if(isWalk){

			//playerオブジェクトをを取得して向きを決定
			GameObject player2 = GameObject.Find("hero");
			Vector3 forward = player2.transform.position - transform.position;

			// 向きに対してモーションを行なう&向きも変える
			if(forward.x > 0){
				transform.Translate(Vector3.right * 0.1f);
				transform.localScale = new Vector3(- 3f, 3f, 3f);
			}else{
				transform.Translate(Vector3.left * 0.1f);
				transform.localScale = new Vector3(3f, 3f, 3f);
			}
			return;


			// ジャンプフラグが立っていたら
		}else if(isJump){
			//ジャンプ力を計算
			jumpPowor = jumpPowor - jumpGrvity;
			transform.Translate(Vector3.up * jumpPowor);
			//地面に着地したら処理処理終了
			if(jumpPowor < 0 && transform.position.y <= 1){
				isIdle = true;
				isJump = false;
			}
			return;
		}else{
			return;
		}
		*/

		//Attack
		if(action_type == 1){
			isIdle = false;
			//isAttack = true;
			anim.SetBool("attack1", true);
			Invoke ("ATJA", 0.3f); //攻撃判定を遅らせる

			ATJ.GetComponent<Enemyattack> ();

			//スリープ
			StartCoroutine("WaitFotAttack");
		}
		if(action_type ==  2){
			isIdle = false;
			//isAttack = true;
			anim.SetBool("attack2", true);
			Invoke ("ATJA", 0.3f); //攻撃判定を遅らせる

			ATJ2.GetComponent<Enemyattack> ();
	
			//スリープ
			StartCoroutine("WaitFotAttack2");
		}
		if(action_type == 3){
			isIdle = false;

			//isAttack = true;
			StartCoroutine("shotting");

			anim.SetBool("attack3", true);
	
			/*

			if(forward.x > 0){
				//fire.gameObject.SendMessage("setDirection", true);
				transform.localScale = new Vector3(-3, 3, 3);
			}else{
				//fire.gameObject.SendMessage("setDirection", false);
				transform.localScale = new Vector3(3, 3, 3);

			}
*/

			//スリープ
			StartCoroutine("WaitFotAttack");

			//action_type = 0;
		}
		/*
		// Walk
		if(move_type == 2){
			isIdle = false;
			isWalk = true;
			StartCoroutine("WaitFotWalk");
		}


		// Jump
		if(move_type == 3){
			isIdle = false;
			isJump = true;
			jumpPowor = jumpPoworConst;
		}
		*/

	}
	IEnumerator shotting()
	{
		while(shot_count<3){
			yield return new WaitForSeconds(0.5f);
	GameObject fire = Instantiate(firePrefab, new Vector3(transform.position.x, transform.position.y - 0.5f, 1), 
		Quaternion.identity) as GameObject ;
	// 攻撃オブジェクトのタグを変える
	fire.gameObject.tag = "shot";
	// 攻撃向きと飛ばす方向を決める
	Vector3 forward = hero.transform.position - transform.position;
			shot_count++;
		}
	}

	IEnumerator WaitFotAttack()
	{
		yield return new WaitForSeconds(0.5f);
		isIdle = true;

		//isAttack = false;
		anim.SetBool("attack1", false);
		anim.SetBool("attack3", false);
		ATJ.SetActive (false);
		action_type = 0;
		shot_count = 0;

	}
	IEnumerator WaitFotAttack2()
	{
		yield return new WaitForSeconds(1.0f);
		isIdle = true;

		//isAttack = false;
		anim.SetBool("attack2", false);
		anim.SetBool("attack3", false);
		ATJ.SetActive (false);
		action_type = 0;
	}

	IEnumerator WaitFotWalk()
	{
		yield return new WaitForSeconds(0.5f);
		isIdle = true;
		isWalk = false;
	}
}