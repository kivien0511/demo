using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item4 : item {

	//雨伞 Item4

	void Start () {
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(isUse)
		{		
			player.GetComponent<player>().isSlowDrop = true;

			player.GetComponent<player>().isTrapped = false;
			player.GetComponent<SpringJoint2D>().enabled = false;
			// bullet.GetComponent<bullet>().isStop = false;
			// bullet.GetComponent<bullet>().isBack = true;
			bullet.transform.position = Vector3.zero;
			bullet.SetActive(false);

			Debug.Log("雨伞"+player.GetComponent<player>().isSlowDrop);//雨伞 Item4

			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		base.AttechBullet(other);
		base.AttechPlayer(other);
	}
}
