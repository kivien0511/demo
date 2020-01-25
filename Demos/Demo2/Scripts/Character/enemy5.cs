using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy5 : enemy {

	//种子草 Enemy5

	public GameObject enemyBullet;

	public int distance = 10;

	float t = 0;

	bool isAttack = false;

	public enum ActiveMode
	{
		StandBy,
		Shoot
	}

	public ActiveMode mode = ActiveMode.StandBy;

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
		Debug.Log("种子草");//种子草 Enemy5

		Check();

		if(isAttack && mode == ActiveMode.StandBy)
		{
			Debug.Log("开火");//Attack
			mode = ActiveMode.Shoot;
		}

		if(mode == ActiveMode.Shoot)
		{
			t += Time.deltaTime;
			if(t >= 2)
			{
				Debug.Log("Shoot");
				Shoot();
				t = 0;
			}
		}
	}

	void Shoot()
	{
		enemyBullet.GetComponent<enemyBullet>().move = player.transform.position - this.transform.position;
		enemyBullet.GetComponent<enemyBullet>().player = player;
		Instantiate(enemyBullet,this.transform.position,Quaternion.identity);
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
