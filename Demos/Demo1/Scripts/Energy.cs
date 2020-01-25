using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour {

	[HideInInspector]
	public float time;

	Slider slider;

	// Use this for initialization
	void Start () {
		time = 3f;
		slider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
		slider.value = time / 3f;
	}
}
