using UnityEngine;
using System.Collections;

public class mob_AI : MonoBehaviour {

	public GameObject firePrefab;
	public GameObject ATJ;
	public GameObject hero;
	public GameObject hiteffect;

	private int move_type = 0;
	private Vector3 forward;

	private bool isIdle = true;
	//private bool isAttack = false;
	private bool isWalk = false;
	private bool isJump = false;
	private Animator anim;


	public float maxLife = 30;    //最大体力（readonlyは変数の変更ができなくなるらしい）
	public float Life = 30;    //現在体力


	// JumpParams
	private float jumpPowor;
	public float jumpPoworConst = 0.8f;
	public float jumpGrvity = 0.05f;

	public float xp = 10;

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
		anim.SetTrigger ("die");
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
		anim.SetTrigger ("die");
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
		/*
		Vector3 worldPos = this.transform.position;
		Camera camera = Camera.main;
		Vector3 viewportPos = GetComponent<Camera>().WorldToViewportPoint (worldPos);
		if (viewportPos.x < -1 || 2 < viewportPos.x) {
			Destroy (this.gameObject);
		}
		*/
		//アイドル状態
		if(isIdle){
			// ランダムでモーション種類基準となる番号を取得
			move_type = Random.Range(0, 4);

			// 歩くフラグが立っていたら
		}else if(isWalk){

			//playerオブジェクトをを取得して向きを決定
			GameObject player2 = GameObject.Find("hero");
			Vector3 forward = player2.transform.position - transform.position;

			// 向きに対してモーションを行なう&向きも変える
			if(forward.x > 0){
				transform.Translate(Vector3.right * 0.1f);
				transform.localScale = new Vector3(- 1.5f, 1.5f, 1.5f);
			}else{
				transform.Translate(Vector3.left * 0.1f);
				transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
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

		//Attack
		if(move_type == 1){
			isIdle = false;
			//isAttack = true;
			anim.SetBool("attack", true);
			Invoke ("ATJA", 0.3f);
			//ATJ.SetActive (true);
			ATJ.GetComponent<Enemyattack> ();
			/*
			//攻撃オブジェクトを生成
			GameObject fire = Instantiate(firePrefab, new Vector3(transform.position.x, transform.position.y - 0.5f, 1), Quaternion.identity) as GameObject;

			// 攻撃オブジェクトのタグを変える
			fire.gameObject.tag = "mob";

			// 攻撃向きと飛ばす方向を決める
			GameObject player2 = GameObject.Find("player");
			Vector3 forward = player2.transform.position - transform.position;

			if(forward.x > 0){
				fire.gameObject.SendMessage("setDirection", true);
				transform.localScale = new Vector3(-2, 2, 2);
			}else{
				fire.gameObject.SendMessage("setDirection", false);
				transform.localScale = new Vector3(2, 2, 2);
			}
			*/
			//スリープ
			StartCoroutine("WaitFotAttack");
		}

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

	}

	IEnumerator WaitFotAttack()
	{
		yield return new WaitForSeconds(1.0f);
		isIdle = true;
		//isAttack = false;
		anim.SetBool("attack", false);
		ATJ.SetActive (false);
	}

	IEnumerator WaitFotWalk()
	{
		yield return new WaitForSeconds(0.5f);
		isIdle = true;
		isWalk = false;
	}
}