using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warning : MonoBehaviour {

	GameObject player;
	GameObject water;
	Text txt;

	float num = 0;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		water = GameObject.FindWithTag("Water");
		txt = this.gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		num = player.transform.position.y - water.transform.position.y - water.transform.localScale.y/2;
		// num = Mathf.Abs(num);
		txt.text = "Distance:" + num.ToString();
	}
}
