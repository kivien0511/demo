using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy2 : enemy {

	//甲虫 Enemy2

	public int distance = 3;

	float t = 0;

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
		Debug.Log("甲虫");//甲虫 Enemy2

		t += Time.deltaTime;
		this.transform.position = new Vector3(this.transform.position.x + distance * Mathf.Sin(t) / 100f,this.transform.position.y,0);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		base.AttechBullet(other);

		base.AttechPlayer(other);
	}
}
