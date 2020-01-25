using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item1 : item {

	//气球 Item1

	float t = 0;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(isUse)
		{
			this.transform.position += new Vector3(0,1*Time.deltaTime,0);
			bullet.transform.position += new Vector3(0,1*Time.deltaTime,0);
			player.GetComponent<SpringJoint2D>().connectedAnchor = bullet.transform.position;

			t += Time.deltaTime;

			if(t >= 5)
			{
				player.GetComponent<player>().isTrapped = false;
				player.GetComponent<SpringJoint2D>().enabled = false;
				// bullet.GetComponent<bullet>().isStop = false;
				// bullet.GetComponent<bullet>().isBack = true;
				bullet.transform.position = Vector3.zero;
				bullet.SetActive(false);

				Destroy(this.gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		base.AttechBullet(other);
		base.AttechPlayer(other);
	}



	
}
