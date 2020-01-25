using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy6 : enemy {

	//蝴蝶 Enemy6

	public int distance = 20;

	float t = 0;
	float a = 0;

	bool isAttack = false;
	
	public enum ActiveMode
	{
		StandBy,
		Follow
	}

	public ActiveMode mode = ActiveMode.StandBy;

	// Use this for initialization
	void Start () {
		hp = 1;
		player = GameObject.FindGameObjectWithTag("Player");
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
			this.Action();
		}
	}

	public override void Action()
	{
		Debug.Log("蝴蝶");//蝴蝶 Enemy6

		Check();

		if(mode == ActiveMode.StandBy)
		{
			t +=  Time.deltaTime;
			a += 4 * Time.deltaTime;
			this.transform.position += new Vector3(distance * Mathf.Sin(t) / 100f,distance * Mathf.Sin(a) / 100f,0);
		
			if(isAttack)
			{
				mode = ActiveMode.Follow;
			}
		}

		if(mode == ActiveMode.Follow)
		{
			Vector2 vel = player.transform.position - this.transform.position;
			this.transform.position += new Vector3(vel.normalized.x,vel.normalized.y,0) * 5 * Time.deltaTime;
		}

		
	}

	
	public void Check()
	{
		Vector2 dis = player.transform.position - this.gameObject.transform.position;
		if(Distance(dis) <= distance)
		{
			isAttack = true;
		}
		else
		{
			isAttack = false;
			mode = ActiveMode.StandBy;
		}
	}

	float Distance(Vector2 v)
	{
		float result = 0;
		result = Mathf.Sqrt(v.x*v.x+v.y*v.y);
		return result;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		base.AttechBullet(other);

		base.AttechPlayer(other);
	}
}
