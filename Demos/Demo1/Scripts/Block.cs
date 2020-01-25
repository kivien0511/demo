using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

	Collider2D collider; 
	enum BlockType
	{
		BOX,
		CIRCLE
	}

	BlockType type = BlockType.BOX;

	// Use this for initialization
	void Start () {
		string name = this.gameObject.name;

		if(name.Contains("Box"))
		{
			collider = this.gameObject.GetComponent<BoxCollider2D>();
			type = BlockType.BOX;
			// Debug.Log("BOX");
		}
		else if(name.Contains("Circle"))
		{
			collider = this.gameObject.GetComponent<CircleCollider2D>();
			type = BlockType.CIRCLE;
			// Debug.Log("CIRCLE");
		}		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Bullet" || other.tag == "Player")
		{			
			switch(type)
			{
			case BlockType.BOX:
				BoxRebound(other);
				break;
			case BlockType.CIRCLE:
				CircleRebound(other);
				break;
			}
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.tag == "Bullet" || other.tag == "Player" || other.tag == "Enemy")
		{
			other.GetComponent<Collider2D>().isTrigger = false;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Bullet" || other.tag == "Enemy")
		{
			if(other.gameObject !=null)
			{
				if(other.gameObject.GetComponent<Bullet>().coroutine != null)
				{
					StopCoroutine(other.GetComponent<Bullet>().coroutine);
					StartCoroutine(other.GetComponent<Bullet>().Back());
				}
			}			
		}
	}

	void BoxRebound(Collider2D col)
	{
		// Debug.Log("rebound:"+col.name);
		Vector2 vel = col.GetComponent<Rigidbody2D>().velocity;
		Vector2 move = Vector2.zero;

		Vector2 vp = col.transform.position - this.transform.position;

		float x = Mathf.Abs(vp.x);
		float y = Mathf.Abs(vp.y);

		if(x > y )
		{
			move = new Vector2(-vel.x ,vel.y);			
		}
		else
		{
			move = new Vector2(vel.x ,-vel.y);			
		}

		col.GetComponent<Rigidbody2D>().velocity = move;
	}

	void CircleRebound(Collider2D col)
	{
		Vector2 vel = col.GetComponent<Rigidbody2D>().velocity;		

		Vector2 vp = col.transform.position - this.transform.position;
		Vector2 m = new Vector2(-vp.y,vp.x);

		float v = Vector2.Dot(vel,m);

		if(v < 0)
		{
			m = -m;
		}

		m = m.normalized;

		float r = Mathf.Sqrt(vel.x*vel.x+vel.y*vel.y);

		v = Vector2.Dot(vel,m);

		float st = 90f-Vector2.Angle(vel,m);

		float cos = Mathf.Cos(st*Mathf.Deg2Rad);
		float sin = Mathf.Sin(st*Mathf.Deg2Rad);

		Vector2 x = m*r;
		Vector2 y = vp*r*sin;
		
		Vector2 move = x+y;

		col.GetComponent<Rigidbody2D>().velocity = move;
	}
}
