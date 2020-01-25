using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyTowerControlScript : MonoBehaviour {
	
	float time = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Settings.Wave > 10 && Settings.Wave < 20)
		{
			this.transform.GetChild(2).gameObject.SetActive(true);
		}
		else if(Settings.Wave > 20)
		{
			this.transform.GetChild(3).gameObject.SetActive(true);
		}
	}


}
