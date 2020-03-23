using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel {

	// Use this for initialization
	void Start () {
		GetControl<Button>("StartButton").onClick.AddListener(ClickStart);
		GetControl<Button>("ExitButton").onClick.AddListener(ClickExit);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//点击开始按钮的处理
	public void ClickStart()
	{
		Debug.Log("StartButton");
	}

	//点击退出按钮的处理
	public void ClickExit()
	{
		Debug.Log("ExitButton");
	}
}
