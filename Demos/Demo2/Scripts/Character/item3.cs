using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item3 : item {

	//护罩 Item3

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(isUse)
		{		
			player.GetComponent<player>().isSheild = true;

			player.GetComponent<player>().isTrapped = false;
			player.GetComponent<SpringJoint2D>().enabled = false;
			// bullet.GetComponent<bullet>().isStop = false;
			// bullet.GetComponent<bullet>().isBack = true;
			bullet.transform.position = Vector3.zero;
			bullet.SetActive(false);

			Debug.Log("护盾"+player.GetComponent<player>().isSheild);//护罩 Item3

			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		base.AttechBullet(other);
		base.AttechPlayer(other);
	}
}
