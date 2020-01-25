using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	float Distance(Vector2 v)
	{
		float result = 0;
		result = Mathf.Sqrt(v.x*v.x+v.y*v.y);
		return result;
	}

	void BoxRebound(Collider2D col)
	{
		Vector2 vel = col.GetComponent<Rigidbody2D>().velocity;
		Vector2 move = Vector2.zero;

		Vector2 vp = col.transform.position - this.transform.position;

		// float x = Mathf.Abs(vp.x);
		// float y = Mathf.Abs(vp.y);

		float x = vp.x;
		float y = vp.y;

		float xboxScale = this.transform.localScale.x;
		float yboxScale = this.transform.localScale.y;

		float xcolScale = col.transform.localScale.x;
		float ycolScale = col.transform.localScale.y;

		int direction = 0;

		//不等于四个角的情况 （衝突判定　四角頂点ではない場合）
		if(!(Mathf.Abs(x) == xboxScale + xcolScale && Mathf.Abs(y) == yboxScale + ycolScale))
		{
			if(x > 0 && y > 0)
			{
				if(Mathf.Abs(x) < xboxScale + xcolScale)
				{
					Debug.Log("右");//Right
					direction = 0;
				}
				else if(Mathf.Abs(y) < yboxScale + ycolScale)
				{
					Debug.Log("上");//Top
					direction = 1;
				}				
			}
			if(x < 0 && y > 0)
			{
				if(Mathf.Abs(x) < xboxScale + xcolScale)
				{
					Debug.Log("左");//Left
					direction = 0;
				}
				else if(Mathf.Abs(y) < yboxScale + ycolScale)
				{
					Debug.Log("上");//Top
					direction = 1;
				}				
			}
			if(x < 0 && y < 0)
			{
				if(Mathf.Abs(x) < xboxScale + xcolScale)
				{
					Debug.Log("左");//Left
					direction = 0;
				}
				else if(Mathf.Abs(y) < yboxScale + ycolScale)
				{
					Debug.Log("下");//Buttom
					direction = 1;
				}				
			}
			if(x > 0 && y < 0)
			{
				if(Mathf.Abs(x) < xboxScale + xcolScale)
				{
					Debug.Log("右");//Right
					direction = 0;
				}
				else if(Mathf.Abs(y) < yboxScale + ycolScale)
				{
					Debug.Log("下");//Buttom
					direction = 1;
				}				
			}
		}

		switch(direction)
		{
			case 0 :
				move = new Vector2(-vel.x,vel.y);
				break;
			case 1 :
				move = new Vector2(vel.x,-vel.y);
				break;
		}

		col.GetComponent<Rigidbody2D>().velocity = move;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			BoxRebound(other);
		}

		if(other.tag == "Enemy")
		{
			if(other.GetComponent<enemy4>())
			{
				if(other.GetComponent<enemy4>().mode == enemy4.ActiveMode.Dash )
				{
					other.GetComponent<enemy4>().mode = enemy4.ActiveMode.StandBy;
					other.GetComponent<enemy4>().dashOver = false;
				}
			}
		}	
	}

}
