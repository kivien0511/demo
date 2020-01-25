using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GotchaScript : MonoBehaviour {

	MessageScript message;

	enum gotLevel{
		Basic = 100,
		High = 400

	}

	// Use this for initialization
	void Start () {
		message = MessageScript.message.getMessageInstance();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void gotCha(int money)
	{
		if(Settings.Money - money >= 0)
		{
			Settings.Money -= money;
			//抽选奖池
			switch(money)
			{
				case (int)gotLevel.Basic:
					//抽选100
					// Debug.Log("-100");
					message.AddMessage("-100");
					break;
				case (int)gotLevel.High:
					//抽选400
					// Debug.Log("-400");
					message.AddMessage("-400");
					break;
			}
		}
		else
		{
			//显示信息 金钱不足
			// Debug.Log("金钱不足:"+money);
			message.AddMessage("金钱不足"+money);
		}
	}

}
