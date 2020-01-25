using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	public float speed = 5f;

	public int attack;

	public Transform target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Move();
	}

	void Move()
	{
		if(target == null)
		{
			Destroy(this.gameObject);
		}
		else
		{
			Vector3 nowPos = transform.position;
			Vector3 targetPos = target.position;

			float dis = Vector3.Distance(nowPos,targetPos);

			Vector3 move = targetPos - nowPos;
			move = move.normalized;

			transform.Translate(move * speed);
		}
		
	}
}
