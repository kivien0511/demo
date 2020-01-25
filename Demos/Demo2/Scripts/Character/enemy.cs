using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class enemy : MonoBehaviour {

	protected int hp;
	
	protected bool isStoped = false;

	public GameObject player;

	protected GameObject bullet;

	protected GameObject energy;

	void Start()
	{
		energy = GameObject.FindWithTag("Energy");
	}


	protected void Damage()
	{
		hp -= 1;
	}

	float Distance(Vector2 v)
	{
		float result = 0;
		result = Mathf.Sqrt(v.x*v.x+v.y*v.y);
		return result;
	}
 
	//敌人行为基础 EnemyBasicAction
	public virtual void Action()
	{
		Debug.Log("enemy");
	}

	//敌人 Enemy
	public virtual void Movement()
	{
		if(isStoped)
		{
			if(hp <= 0)
			{
				Debug.Log("hp<0");
				player.GetComponent<player>().isTrapped = false;
				player.GetComponent<SpringJoint2D>().enabled = false;
				// bullet.GetComponent<bullet>().isStop = false;
				// bullet.GetComponent<bullet>().isBack = true;
				bullet.transform.position = Vector3.zero;
				bullet.SetActive(false);
				isStoped = false;

				Destroy(this.gameObject);
			}			
		}
		else
		{
			Action();
		}
	}

	public virtual void AttechBullet(Collider2D other)
	{
		if(other.tag == "Bullet")
		{
			bool isBack = other.GetComponent<bullet>().isBack;

			if(isBack)
			{
				bullet = other.gameObject;
				isStoped = true;
			}
		}		
	}

	public virtual void AttechPlayer(Collider2D other)
	{
		if(other.tag == "Player")
		{			
			if(isStoped)
			{
				Damage();
				Debug.Log("敌人受伤处理");//EnemyDamage
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
				Debug.Log("玩家受伤处理 弹开");	//PlayerDamageAction
			}
		}
	}

}
