using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	public int hp;

	float speed = 3f;

	bool isMagicAttack = false;

	bool isSpeedDown = false;

	bool isAttackSpeedUp = false;

	int speedDownLevel = 0;

	int speedUpLevel = 0;

	int money;

	public List<Vector3> wayPoints = new List<Vector3>();

	int wayIndex = 0;

	public void Move()
	{
		if(wayIndex == wayPoints.Count)
		{
			wayIndex = 0;
		}

		Vector3 nowPos = transform.position;
		Vector3 targetPos = wayPoints[wayIndex];

		float dis = Vector3.Distance(nowPos,targetPos);
		
		if(dis < 0.1F)
		{
			wayIndex += 1;
		}

		Vector3 move = targetPos - nowPos;
		move = move.normalized;

		transform.Translate(move * speed * Time.deltaTime);
	}

	public void CheckHp()
	{
		if(hp <= 0)
		{
			Settings.PlayerHP += 1;
			int ran = Random.Range(10,31);
			money = Settings.Wave + 10 +ran;
			Settings.Money += money;
			Destroy(this.gameObject);			
		}
	}

	// Use this for initialization
	void Start () {
		Settings.PlayerHP -= 1;
	}
	
	// Update is called once per frame
	void Update () {
		if(isMagicAttack)
		{

		}

		if(isSpeedDown)
		{

		}

		if(isAttackSpeedUp)
		{

		}

		Move();
	}

	void Damage(int attack)
	{
		hp -= attack;
		CheckHp();
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Bullet")
		{
			int attack = other.GetComponent<BulletScript>().attack;
			Damage(attack);
			transform.GetChild(0).GetChild(0).GetComponent<HpViewScript>().OnHit(attack);
			Destroy(other.gameObject);
		}

		if(other.tag == "SpeedDown" )
		{
			isSpeedDown = true;
			// int level = other.GetComponent<SpeedDownTower>().speedDownLevel;

			// if (speedDownLevel == 0 || level > speedDownLevel)
			// {
			// 	speedDownLevel = level;
			// }
		}

		if(other.tag == "AttackSpeedUp")
		{
			isAttackSpeedUp = true;
		}

	}

	void OnTriggerStay(Collider other)
	{
		
		if(other.tag == "MagicAttack")
		{
			isMagicAttack = true;
		}

		if(other.tag == "SpeedDown" )
		{
			// int level = other.GetComponent<SpeedDownTower>().speedDownLevel;

			// if (speedDownLevel == 0 || level > speedDownLevel)
			// {
			// 	speedDownLevel = level;
			// }
		}

		if(other.tag == "AttackSpeedUp")
		{
			isAttackSpeedUp = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "MagicAttack")
		{
			isMagicAttack = false;
		}

		if(other.tag == "SpeedDown" )
		{
			isSpeedDown = false;
			speedDownLevel = 0;
		}

		if(other.tag == "AttackSpeedUp")
		{
			isAttackSpeedUp = false;
		}
	}
}
