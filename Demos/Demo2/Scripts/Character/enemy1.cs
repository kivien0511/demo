using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy1 : enemy {

	//蜗牛 Enemy1

	// Use this for initialization
	void Start () {
		hp = 1;
		player = GameObject.FindGameObjectWithTag("Player");
		// bullet = GameObject.FindGameObjectWithTag("Bullet");
	}
	
	// Update is called once per frame
	void Update () 
	{
		base.Movement();
	}

	public override void Action()
	{
		Debug.Log("蜗牛");//蜗牛 Enemy1
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		base.AttechBullet(other);

		base.AttechPlayer(other);
	}
}
