using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item5 : item {

	//救生圈 Item5

	void Start () {
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(isUse)
		{		
			player.GetComponent<player>().isOtherLife = true;

			player.GetComponent<player>().isTrapped = false;
			player.GetComponent<SpringJoint2D>().enabled = false;
			// bullet.GetComponent<bullet>().isStop = false;
			// bullet.GetComponent<bullet>().isBack = true;
			bullet.transform.position = Vector3.zero;
			bullet.SetActive(false);

			Debug.Log("救生圈"+player.GetComponent<player>().isOtherLife);//救生圈 Item5

			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		base.AttechBullet(other);
		base.AttechPlayer(other);
	}
}
