using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{		
		if((other.tag == "Bullet"&& !other.GetComponent<Bullet>().isBack) || other.tag == "Player")
		{			
			other.isTrigger = true;			
		}
	}



	void OnTriggerExit2D(Collider2D other)
	{
		if( other.tag == "Player" || other.tag == "Bullet")
		{			
			other.isTrigger = false;			
		}

		if(other.tag == "Enemy")
		{
			other.isTrigger = true;		
		}
	}
}
