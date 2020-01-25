using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

	enum WallType
	{
		UP,DOWN,LEFT,RIGHT
	}

	WallType type = WallType.UP;

	// Use this for initialization
	void Start () {
		switch(name)
		{
			case "BoxWall1":
				break;
			case "BoxWall2":
				type = WallType.DOWN;
				break;
			case "BoxWall3":
				type = WallType.LEFT;
				break;
			case "BoxWall4":
				type = WallType.RIGHT;
				break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Bullet" || other.tag == "Player")
		{						
			BoxRebound(other);			
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.tag == "Bullet" || other.tag == "Player")
		{					
			Vector2 move = Vector2.zero;
			Vector2 vel = other.GetComponent<Rigidbody2D>().velocity;
	
			switch(type)
			{
				case WallType.UP:
					move = new Vector2(0,-1);
					break;
				case WallType.DOWN:
					move = new Vector2(0,1);
					break;
				case WallType.LEFT:
					move = new Vector2(1,0);
					break;
				case WallType.RIGHT:
					move = new Vector2(-1,0);
					break;
			}

			other.GetComponent<Rigidbody2D>().velocity = move;
		}

		if(other.tag == "Enemy")
		{
			other.GetComponent<Collider2D>().isTrigger = false;
		}
	}

	void BoxRebound(Collider2D col)
	{
		Vector2 vel = col.GetComponent<Rigidbody2D>().velocity;
		Vector2 move = Vector2.zero;

		switch(type)
		{
			case WallType.UP:
			case WallType.DOWN:
				move = new Vector2(vel.x ,-vel.y);
				break;
			case WallType.LEFT:
			case WallType.RIGHT:
				move = new Vector2(-vel.x ,vel.y);
				break;
		}

		col.GetComponent<Rigidbody2D>().velocity = move;
	}
}
