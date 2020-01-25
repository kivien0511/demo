using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy8 : enemy {

	//乌鸦 Enemy8
 

	public int distance = 3;

	float t = 0;

	// Use this for initialization
	void Start () {
		hp = 1;
		player = GameObject.FindGameObjectWithTag("Player");
		// bullet = GameObject.FindGameObjectWithTag("Bullet");
	}
	
	// Update is called once per frame
	void Update () {
		this.Movement();
	}

	
	public override void Movement()
	{
		if(isStoped)
		{
			this.transform.position += new Vector3(0,8* Time.deltaTime,0);

			bullet.transform.position += new Vector3(0,1*Time.deltaTime,0);
			player.GetComponent<SpringJoint2D>().connectedAnchor = bullet.transform.position;

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
		Debug.Log("乌鸦");//乌鸦 Enemy8

		
		this.transform.position += new Vector3(0,5 * Time.deltaTime,0);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		base.AttechBullet(other);

		base.AttechPlayer(other);
	}
}
