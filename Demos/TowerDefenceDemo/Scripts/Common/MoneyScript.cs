using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyScript : MonoBehaviour {

	public Text moneyText;

	// Use this for initialization
	void Start () {
		Settings.Money = 1000;
	}
	
	// Update is called once per frame
	void Update () {
		moneyText.text = "Money:"+ Settings.Money;
	}
}
