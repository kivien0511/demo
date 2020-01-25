using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy7 : enemy {

	//蜜蜂 Enemy7

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
		Debug.Log("蜜蜂");//蜜蜂 Enemy7

		t += 2 * Time.deltaTime;
		this.transform.position += new Vector3(0,distance * Mathf.Sin(t) / 10f,0);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		base.AttechBullet(other);

		base.AttechPlayer(other);
	}
}
