using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item6 : item {

	//蒲公英 Item6

	GameObject gameManager;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		gameManager = GameObject.FindWithTag("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
		if(isUse)
		{		
			gameManager.GetComponent<money>().gameMoney += 10;

			player.GetComponent<player>().isTrapped = false;
			player.GetComponent<SpringJoint2D>().enabled = false;
			// bullet.GetComponent<bullet>().isStop = false;
			// bullet.GetComponent<bullet>().isBack = true;
			bullet.transform.position = Vector3.zero;
			bullet.SetActive(false);

			Debug.Log("蒲公英"+gameManager.GetComponent<money>().gameMoney);//蒲公英 Item6

			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		base.AttechBullet(other);
		base.AttechPlayer(other);
	}
}
