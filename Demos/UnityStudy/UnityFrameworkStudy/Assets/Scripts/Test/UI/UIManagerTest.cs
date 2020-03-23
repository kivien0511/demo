using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerTest : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		UIManager.GetInstance().ShowPanel<LoginPanel>("LoginPanel",E_UI_Layer.Mid,ShowPanelOver);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void ShowPanelOver(LoginPanel panel)
	{
		Debug.Log("LoginPanel Loaded");
		Invoke("DelayHide",3);
	}

	private void DelayHide()
	{
		UIManager.GetInstance().HidePanel("LoginPanel");
	}
}
