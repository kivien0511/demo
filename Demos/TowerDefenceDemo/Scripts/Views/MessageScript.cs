using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class MessageScript : MonoBehaviour {

	[HideInInspector]
	public　static MessageScript message;

	private int count = 0;

	private const int countMax = 5;

	// private string[] msg = new string[countMax];

	// [HideInInspector]
	// public List<GameObject> msgList = new List<GameObject>();

	public Text[] msgList;

	private float time;

	void Awake()
	{
		message = this;	
	}

	// Use this for initialization
	void Start () {
		// for (int i = 0; i < countMax; i++)
		// {
		// 	msgList.Insert(i,this.transform.GetChild(i).gameObject);
		// }
	}

	public MessageScript getMessageInstance()
	{
		return message;
	}

	public void AddMessage(string str)
	{
		if(count >= countMax)
		{
			sortMsg(str);
		}
		else
		{
			msgList[count].text = str;
			count++;
		}
	}

	private void sortMsg(string str)
	{
		this.msgList[0].text = "";
		for(int i = 0; i < countMax-1 ; i++)
		{
			this.msgList[i].text = this.msgList[i+1].text;
		}
		this.msgList[countMax-1].text = str;
	}

	private void sortMsgList()
	{
		this.msgList[0].text = "";
		for(int i = 0; i < countMax-1 ; i++)
		{
			this.msgList[i].text = this.msgList[i+1].text;
		}
		this.msgList[countMax-1].text = "";
		count --;
	}
	
	// Update is called once per frame
	void Update () {
		if(count > 0)
		{
			time += Time.deltaTime;
		}

		if(time >= (float)countMax)
		{
			sortMsgList();
			time = 0;
		}
	}
}
