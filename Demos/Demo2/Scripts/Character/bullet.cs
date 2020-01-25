using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

	[HideInInspector]
	public bool isBack = false;

	public bool isStop = false;

	[HideInInspector]
	public Vector2 target;

	GameObject player;

	GameObject energy;

	Vector2 vel;

	// Use this for initialization
	void Start () {
		// player = this.transform.parent.gameObject;
		player = GameObject.FindGameObjectWithTag("Player");
		energy = GameObject.FindGameObjectWithTag("Energy");
	}
	
	// Update is called once per frame
	void Update () {

		
		if(!isStop)
		{		
			if(isBack)
			{	
				vel = player.transform.position - this.transform.position;
				this.transform.position += new Vector3(vel.normalized.x,vel.normalized.y,0) * 20 * Time.deltaTime;

				if(Distance(vel) <= 0.3)
				{
					isBack = false;
					this.gameObject.SetActive(false);
				}
			}
			else
			{
				vel = target - (Vector2)this.transform.position;
				this.transform.position += new Vector3(vel.normalized.x,vel.normalized.y,0) * 20 * Time.deltaTime;

				if(Distance(vel) <= 0.3)
				{
					isBack = true;
				}
			}
		}
		else
		{
			if(energy.GetComponent<Energy>().energy < 100)
			{
				energy.GetComponent<Energy>().energy += 10 * Time.deltaTime;
			}
			else 
			{
				energy.GetComponent<Energy>().energy = 100;
			}

			if(!player.GetComponent<SpringJoint2D>().isActiveAndEnabled)
			{
				this.gameObject.SetActive(false);
			}
		}
	}

	float Distance(Vector2 v)
	{
		float result = 0;
		result = Mathf.Sqrt(v.x*v.x+v.y*v.y);
		return result;
	}

	void OnTriggerStay2D(Collider2D other)
	{
		// if(other.tag == "Floor" && isBack)
		// {
		// 	this.transform.position = other.transform.position;
		// }

		if((other.tag == "Floor" || other.tag == "Enemy") && isBack)
		{
			// this.transform.position = other.transform.position;
			player.GetComponent<player>().isTrapped = true;
			player.GetComponent<SpringJoint2D>().enabled = true;
			isStop = true;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.GetComponent<enemy3>())
		{
			Debug.Log("enemy3");
		}

		if(isBack && (other.tag == "Floor" || other.tag == "Enemy" || other.tag == "Item"))
		{
			// this.transform.position = other.transform.position;
			player.GetComponent<player>().isTrapped = true;
			player.GetComponent<SpringJoint2D>().enabled = true;
			isStop = true;
			
			if(energy.GetComponent<Energy>().energy > 100)
			{
				energy.GetComponent<Energy>().energy = 100;
			}
			else 
			{
				energy.GetComponent<Energy>().energy += 20;
			}

		}
	}
}
