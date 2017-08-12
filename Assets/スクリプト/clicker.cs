using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Cameraオブジェクトに張り付けて使って下さい.
/// ……別にカメラ取得するなら、何に張り付けてもいいんですけどね.（ﾎﾞｿｯ
/// </summary>
public class clicker : MonoBehaviour {

	public GameObject addTextObj;
	public GameObject Prompt;
	public GameObject Ignis;
	public GameObject Gradio;
	public GameObject Ardyn;
	public GameObject Ardyntext; // 吹き出し
	public GameObject Shoptext; //為替レート
	public GameObject Army;
	public GameObject ArmyCountText;


	private float timeleft; //魔導兵が勝手にクリックを増やすのに必要
	private float Armytime =0.5f; //魔導兵が勝手にクリックを増やすのに必要

	public float clickrate = 0.5f;


	int click = 0;
	int money =0;
	int Army_table =1;
	int army =0;

	int PromptPower = 1;
	int GradioPower = 1;
	int IgnisPower = 1;
	int ArmyPower	=5;
	Text countText, messageText;
	Text moneyText, mText;
	GameObject cookie;
	GameObject canvas;

	void Start () {
		click = 0;//デバッグ用.
		money =0;
		//click = PlayerPrefs.GetInt("ClickCount");//コメントアウトしてありますが、これが無いとセーブデータが読み込めません！
		countText = GameObject.Find("CountText").GetComponent<Text>();
		moneyText = GameObject.Find("MoneyText").GetComponent<Text>();
		cookie = GameObject.Find("Cookie");
		canvas = GameObject.Find("Canvas");
		countText.text = "Count:"+click;
		moneyText.text = "あ:"+money;
	}

	void Update () {
		Debug.Log (army);
		//デバック用コマンド
		if(Input.GetKeyDown("q")){
			money -= 50;
		}
		if(Input.GetKeyDown("w")){
			click += 50;
			money+=50;
		}

		countText.text = "Count:" + click;//クリックUI更新.
		moneyText.text = "あ:" + money;//クリックUI更新.

		//クリックが150以上でアーデンがショップのモーション
		if(click>150){
			Ardyn.GetComponent<Animator> ().SetTrigger ("shop");
			Ardyntext.SetActive (true);
		}
		//クリックしたとき
		if (Input.GetMouseButtonUp(0)) {
			Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);
			if (hit) {
				Debug.Log("Hit!" + hit.transform.gameObject.tag);
				if (hit.transform.tag == "Cristal") {//tagがクッキーのオブジェクトをクリックしたとき.
					Click();
				}else if(hit.transform.tag == "Ardyn"&& click>150 ){
					Shop ();
				}
			}
		}

		//魔導兵が勝手にクリックを増やす。およそ0.5fごとにArmypowerで増やす
		if(Army.activeSelf==true){
			timeleft -= Time.deltaTime;
			if (timeleft <= 0.0) {
				timeleft = Armytime;
				//ここに処理
				click += ArmyPower;
				money += ArmyPower;

			}
			//StartCoroutine("ArmyAttack");//"ArmyAttack"のコルーチンを呼ぶ.
			
		}

		//クッキーの数でメッセージが変化(定番ですね).
		/*

		if (click > 50) {
			messageText.text = "あなたのクッキーは異世界でも評判がいい。";
		}
		else if (click > 40) {
			messageText.text = "この世界であなたのクッキーを知らない生物はいない。";
		}
		else if (click > 30) {
			messageText.text = "あなたのクッキーは今や空前の大ブームとなっている。";
		}
		else if (click > 20) {
			messageText.text = "あなたのクッキーは町の人が並んで買うほど人気がある。";
		}
		else if (click > 10) {
			messageText.text = "あなたのクッキーを家族が食べてくれた。";
		}
		else if (click <= 10) {
			messageText.text = "あなたのクッキーからは生ごみの味がする。";
		}
		*/
	}

	/// <summary>
	/// クリック処理.
	/// </summary>
	void Click() {
		click +=PromptPower; //総クリスタル叩いた回数
		money += PromptPower; //クリスタル叩けば増えるけど買い物すれば減る
		GetComponents<AudioSource> ()[0].Play ();
		/*
		GameObject obj = Instantiate(addTextObj);
		obj.transform.position = Input.mousePosition;//objの座標をマウス位置にする.
		obj.transform.SetParent(canvas.transform);//親をCanvasに指定する.
		obj.AddComponent<ObjectDestroy>();//ObjectDestroyコンポーネントをobjに加える.
		*/

		//moneyText.text = "あ:" + money;//クリックUI更新.
		if (click > 100) {
			Ignis.GetComponent<Animator> ().SetTrigger ("attack");
			Prompt.GetComponent<Animator> ().SetTrigger ("attack");
			Gradio.GetComponent<Animator>().SetTrigger("attack");
			click+=GradioPower;
			click+=IgnisPower;
			money+=GradioPower;
			money+=IgnisPower;
			GetComponents<AudioSource> ()[1].Play ();
			GetComponents<AudioSource> ()[2].Play ();
		} else if (click > 30) {
			click+=GradioPower;
			money += GradioPower;
			GetComponents<AudioSource> ()[1].Play ();
			Prompt.GetComponent<Animator> ().SetTrigger ("attack");
			Gradio.GetComponent<Animator>().SetTrigger("attack");
		}else if (click > 0) {
			Prompt.GetComponent<Animator> ().SetTrigger ("attack");
			
		//StartCoroutine("ClickAnim");//"ClickAnim"のコルーチンを呼ぶ.
	}
	}
	//ショップでは魔導兵増産が行える
	void Shop(){
		switch(Army_table){
		case(1):
			if(money>150){
				Army.SetActive (true);
				army++; //兵士の数を増やす
				money -= 150; //お金を減らす
				ArmyCountText.GetComponent<Text> ().text = "あ×" + army; //兵士の上のアイコンの数値を増やす
				Army_table++; //次のレベルアップテーブルに移行する
				Shoptext.GetComponent<Text> ().text = "500"; //次必要なお金を描く
				GetComponents<AudioSource> ()[3].Play (); //ちゃりんの音を鳴らす
				ArmyPower=5; //現在の兵士の力
			}
			break;
		case(2):
			if(money>500){
				Army.SetActive (true);
				army++;
				money -= 500;
				ArmyCountText.GetComponent<Text> ().text = "あ×" + army;
				Army_table++;
				Shoptext.GetComponent<Text> ().text = "2000";
				GetComponents<AudioSource> ()[3].Play ();
				ArmyPower = 10;
			}
			break;
		case(3):
			if(money>2000){
				Army.SetActive (true);
				army++;
				money -= 2000;
				ArmyCountText.GetComponent<Text> ().text = "あ×" + army;
				Army_table++;
				Shoptext.GetComponent<Text> ().text = "未定";
				GetComponents<AudioSource> ()[3].Play ();
				ArmyPower = 50;
				Armytime = 0.3f;
			}
			break;
		}
		

	}
	/// <summary>
	/// クッキーが微妙に大きくなるような演出をするメソッド.
	/// </summary>
	/// <returns></returns>

	/*
	IEnumerator ArmyAttack() {
		while (true) {
			yield return null;
			click += ArmyPower;


		}
	
		float time = 0;//アニメーションの時間を記録.
		float scale = 1;//クッキーサイズ.
		while (time < clickrate/2) {//アニメーションの半分の時間を拡大に使う.
			time += Time.fixedDeltaTime;//時間をフレームの増分足す.
			scale = 1 + time/clickrate/8;
			cookie.transform.localScale = new Vector2(scale,scale);//クッキーのサイズを変える.
			yield return new WaitForSeconds(Time.deltaTime);//フレームの終わりまで待つ.
		}
		//上の逆バージョン（今度は縮小処理）.
		while (time < clickrate/2) {
			time += Time.fixedDeltaTime;
			scale -= time/clickrate/8;
			cookie.transform.localScale = new Vector2(scale,scale);
			yield return new WaitForSeconds(Time.deltaTime);
		}
		cookie.transform.localScale = new Vector2(1,1);//スケールに誤差があるかもしれないので、ここで(1,1)に戻す.

	}
*/
	/// <summary>
	/// アプリケーション終了時に呼ばれる.
	/// </summary>
	private void OnApplicationQuit() {
		PlayerPrefs.SetInt("ClickCount", click);
	}
}

/// <summary>
/// クッキーをクリックしたときの『+1』を
/// 時間が経つと消してくれるクラス.
/// </summary>
public class ObjectDestroy : MonoBehaviour{

	float destroyTime = 0.5f;
	float timer;

	private void Update() {
		timer += Time.deltaTime;
		if (destroyTime <= timer) {
			Destroy(gameObject);
		}
	}
}