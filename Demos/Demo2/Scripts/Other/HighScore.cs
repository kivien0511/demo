using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {

	GameObject player;

	[HideInInspector]
	public float num = 0;

	Text txt;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		txt = this.gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if(player.transform.position.y > num)
		{
			num = player.transform.position.y;
		}

		txt.text = num.ToString();
	}
}
