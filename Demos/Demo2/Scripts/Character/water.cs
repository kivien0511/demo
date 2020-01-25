using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class water : MonoBehaviour {

	public GameObject highScore;
	public GameObject result;

	GameObject player;

	bool isUp = false;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

		float distance = player.transform.position.y - this.transform.position.y - this.transform.localScale.y/2;

		if(distance > 50)
		{
			isUp = true;			
		}
		
		if(isUp)
		{
			this.transform.position += new Vector3(0,20,0);
			isUp = false;
		}

		//缓慢上升
		this.transform.position += new Vector3(0,1*Time.deltaTime,0);

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			if(other.GetComponent<player>().isOtherLife)
			{
				Debug.Log("复活");//Reborn

				other.GetComponent<Rigidbody2D>().velocity = new Vector2(0,10);
				other.GetComponent<player>().isOtherLife = false;
			}
			else
			{
				Debug.Log("死亡");//Dead
				result.SetActive(true);
				Destroy(other.gameObject.GetComponent<player>());
				other.GetComponent<BoxCollider2D>().isTrigger = false;
				float num = highScore.GetComponent<HighScore>().num;
				result.GetComponent<Text>().text = "Failed\nHighScore：" + num.ToString().Substring(0,4);
			}
		}
	}
}
