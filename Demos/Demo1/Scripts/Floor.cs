using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

	[HideInInspector]	
	public bool isPaint = false;

	SpriteRenderer spriteRenderer;

	Color paint = new Color(0.5f,0.5f,0,1);
	Color floor;

	// Use this for initialization
	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		floor = spriteRenderer.color;
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Enemy" && isPaint && !other.GetComponent<Enemy>().isCold)
		{
			//定身敌人
			other.GetComponent<Enemy>().cold = 3;
		}
		if(other.tag == "Bullet" && !other.GetComponent<Bullet>().isDamaged)
		{
			isPaint = true;
			spriteRenderer.color = paint;
		}
		if(other.tag == "Player")
		{
			isPaint = false;
			spriteRenderer.color = floor;
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			isPaint = false;
			spriteRenderer.color = floor;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Enemy" && isPaint && !other.GetComponent<Enemy>().isCold)
		{
			//定身敌人
			other.GetComponent<Enemy>().cold = 1;
		}
	}
}
