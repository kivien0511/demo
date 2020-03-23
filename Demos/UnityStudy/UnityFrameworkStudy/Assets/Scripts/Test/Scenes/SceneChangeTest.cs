using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneChangeTest : MonoBehaviour {

	private int process = 0;

	// Use this for initialization
	void Start () {
		EventCenter.GetInstance().AddEventLinster<object>("Loading",loading);
		ScenesManager.GetInstance().LoadSceneAsyn("SampleScene",()=>{});
		Debug.Log("SceneChangeTest");
	}

	private void loading(object info)
	{
		Debug.Log("loading:"+ (info));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
