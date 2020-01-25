using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : enemy {

	[HideInInspector]
	public Vector3 move;

	Vector3 vel;

	float t = 0;
	float a = 0;

	// Use this for initialization
	void Start () {
		hp = 1;
	}
	
	// Update is called once per frame
	void Update () {

		this.Movement();
	}

	public override void Movement()
	{
		if(isStoped)
		{
			if(hp <= 0)
			{
				player.GetComponent<player>().isTrapped = false;
				player.GetComponent<SpringJoint2D>().enabled = false;
				// bullet.GetComponent<bullet>().isStop = false;
				// bullet.GetComponent<bullet>().isBack = true;
				bullet.transform.position = Vector3.zero;
				bullet.SetActive(false);
				isStoped = false;

				this.transform.position += vel.normalized * 5 * Time.deltaTime;

				//问题 ：被定住没有碰到玩家本体情况下的后续处理

				a += Time.deltaTime;
				if(a >= 3)
				{
					Destroy(this.gameObject);
				}
			}
		}
		else
		{
			Action();
		}
	}

	public override void Action()
	{
		Debug.Log("种子草子弹");//EnemyBullet

		this.transform.position += move.normalized * 5 * Time.deltaTime;

		t += Time.deltaTime;

		if(t >= 10)
		{
			Destroy(this.gameObject);
		}
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		this.AttechBullet(other);
		base.AttechPlayer(other);
	}

	public override void AttechBullet(Collider2D other)
	{
		if(other.tag == "Bullet")
		{
			bool isBack = other.GetComponent<bullet>().isBack;

			if(isBack)
			{
				bullet = other.gameObject;
				vel = this.transform.position - player.transform.position;
				isStoped = true;
			}
		}		
	}


}
