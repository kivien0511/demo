using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerLevelScript : MonoBehaviour {

	int level;

	Text text ;

	// Use this for initialization
	void Start () {
		level = this.transform.parent.transform.parent.GetComponent<TowerTest>().level;
		text = this.gameObject.GetComponent<Text>();
		this.transform.parent.transform.rotation = Camera.main.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "Lv"+level;
	}



}
