using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HPgauge : MonoBehaviour {

	public Text hpLabel;
	public Image hpSprite;
	public Image moveSprite;
	public GameObject actionLabel;

	private int maxHp;
	private int nowHp;
	private int moveHp;
	private Vector4 damageColor;
	private Vector4 healColor;

	void Awake()
	{
		damageColor = new Vector4(0.5f, 0, 0, 1);
		healColor = new Vector4(0.3f, 1, 0.5f, 1);
		maxHp = 1000;
		moveHp = 1000;
		nowHp = maxHp;
		hpSprite.fillAmount = (float)nowHp / (float)maxHp;
		moveSprite.fillAmount = (float)moveHp / (float)maxHp;
		hpLabel.text = nowHp + "/" + maxHp;
	}

	public void OnDamageClick()
	{
		int damage = Random.Range(20,600);
		moveHp -= damage;
		if(moveHp < 0) moveHp = 0;
		actionLabel.GetComponent<Text>().color = damageColor;
		actionLabel.GetComponent<Text>().text = damage + "ダメージ";
	}

	public void OnHealClick()
	{
		int heal = Random.Range(20,600);
		moveHp += heal;
		if(moveHp > maxHp) moveHp = maxHp;

		actionLabel.GetComponent<Text>().color = healColor;
		actionLabel.GetComponent<Text>().text = heal + "回復";

	}

	void Start () {
	}

	void Update () {
		if(nowHp != moveHp)
		{
			if(nowHp > moveHp)
			{
				// damage
				nowHp -= Mathf.FloorToInt(maxHp * Time.deltaTime * 0.3f);
				if(nowHp < moveHp) nowHp = moveHp;
				moveSprite.color = damageColor;
				hpSprite.fillAmount = (float)moveHp / (float)maxHp;
				moveSprite.fillAmount = (float)nowHp / (float)maxHp;
			}
			else
			{
				// heal
				nowHp += Mathf.FloorToInt(maxHp * Time.deltaTime * 0.3f);
				if(nowHp > moveHp) nowHp = moveHp;

				moveSprite.color = healColor;
				hpSprite.fillAmount = (float)nowHp / (float)maxHp;
				moveSprite.fillAmount = (float)moveHp / (float)maxHp;

			}
			hpLabel.text = nowHp + "/" + maxHp;
		}
	}
}