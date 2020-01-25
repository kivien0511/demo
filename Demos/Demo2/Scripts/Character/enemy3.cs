using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy3 : enemy {

	//毛虫 Enemy3

	public int distance = 3;

	float t = 0;

	bool cantAttench = true;

	Color temp;

	// Use this for initialization
	void Start () {
		hp = 1;
		player = GameObject.FindGameObjectWithTag("Player");
		temp = this.gameObject.GetComponent<SpriteRenderer>().color;		
	}
	
	// Update is called once per frame
	void Update () {
		base.Movement();
	}

	public override void Action()
	{
		Debug.Log("毛虫");//毛虫 Enemy3

		t += Time.deltaTime;
		this.transform.position = new Vector3(this.transform.position.x + distance * Mathf.Sin(t) / 100f,this.transform.position.y,0);

		if(t % 4 > 2)
		{
			Debug.Log("有刺");//Cant Attack
			this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,0f,0f,1f);  
			cantAttench = true;
		}
		else
		{
			Debug.Log("没刺");//Can Attack
			this.gameObject.GetComponent<SpriteRenderer>().color = temp;
			cantAttench = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		AttechBullet(other);
		AttechPlayer(other);
	}
	
	public override void AttechBullet(Collider2D other)
	{
		if(other.tag == "Bullet" && !cantAttench)
		{
			bool isBack = other.GetComponent<bullet>().isBack;

			if(isBack)
			{
				bullet = other.gameObject;
				isStoped = true;
			}
		}		
	}

	public override void AttechPlayer(Collider2D other)
	{
		if(other.tag == "Player")
		{			
			if(isStoped)
			{
				Damage();
				Debug.Log("敌人受伤处理");//EnemyDamageAction
				if(energy.GetComponent<Energy>().energy >= 20)
				{
					energy.GetComponent<Energy>().energy -= 20;
				}
				else
				{
					energy.GetComponent<Energy>().energy = 0;
				}
			}
			else
			{
				if(cantAttench)
				{
					Debug.Log("玩家精力值减少");//PlayerEnergyDown
				}
				else
				{
					Debug.Log("玩家受伤处理 弹开");//PlayerDamageAction
				}				
			}
		}
	}
}
