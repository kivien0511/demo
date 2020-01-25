using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy4 : enemy {

	//松鼠 Enemy4

	public int distance = 10;

	float t = 0;

	bool isAttack = false;

	public enum ActiveMode
	{
		StandBy,
		Charge,
		Dash
	}

	Vector2 move;

	public ActiveMode mode = ActiveMode.StandBy;

	[HideInInspector]
	public bool dashOver = false;

	// Use this for initialization
	void Start () {
		hp = 1;
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		base.Movement();
	}

	public override void Action()
	{
		Debug.Log("松鼠");//松鼠 Enemy4

		Check();

		if(isAttack && mode == ActiveMode.StandBy)
		{
			mode = ActiveMode.Charge;
		}

		if(mode == ActiveMode.Charge)
		{
			t += Time.deltaTime;
			if(t >= 2)
			{
				move = player.transform.position - this.transform.position;
				mode = ActiveMode.Dash;
				t = 0;
			}
		}

		if(mode == ActiveMode.Dash)
		{
			Dash();
		}

		
	}

	void Dash()
	{
		
		if(!dashOver)
		{
			Vector2 vel = player.transform.position - this.transform.position;

			this.transform.position += new Vector3(vel.normalized.x,vel.normalized.y,0) * 20 * Time.deltaTime;

			if(Distance(vel) <= 0.3)
			{
				Debug.Log("冲完了");//DashOver
				dashOver = true;
			}
		}
		else
		{
			if(move.x > 0)
			{
				//从左往右冲 Left2Right
				this.transform.position += new Vector3(1,0,0) * 20 * Time.deltaTime;
			}
			else if(move.x < 0)
			{
				//从右往左冲 Right2Left
				this.transform.position += new Vector3(-1,0,0) * 20 * Time.deltaTime;
			}
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
