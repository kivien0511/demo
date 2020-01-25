using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;

public class item2 : item {

	//铁环 Item2

	float t;

	bool isPress = false;

	bool isShoot = false;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(isUse)
		{
			t += Time.deltaTime;
			player.GetComponent<Rigidbody2D>().velocity += new Vector2(Mathf.Cos(t*5)/2,Mathf.Sin(t*5)/2);

			if(isShoot)
			{
				Debug.Log("铁环放手");//铁环放手 Item2Used
				Destroy(this.gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		base.AttechBullet(other);
		base.AttechPlayer(other);
	}

	public void rotate()
	{
		if(isUse)
		{
			isPress = true;
		}
	}

	public void shoot()
	{
		if(isPress)
		{
			isShoot = true;
		}
	}
}
