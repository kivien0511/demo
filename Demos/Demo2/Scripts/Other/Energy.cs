using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour {

	public float energy = 100;

	[HideInInspector]
	Text text;

	// Use this for initialization
	void Start () {
		text = this.gameObject.GetComponent<Text>();
		
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "Energy：" + (int)energy;
	}
}
