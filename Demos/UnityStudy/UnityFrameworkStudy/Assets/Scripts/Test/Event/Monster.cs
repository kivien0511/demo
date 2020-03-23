﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

	public int type = 1;

	public string name = "123123";

	// Use this for initialization
	void Start () {
		Dead();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// 死亡方法
	/// </summary>
	void Dead()
	{
		Debug.Log("怪物死亡");
		// //其他对象想在怪物死亡时做点什么
		// //比如
		// //1.玩家获得奖励
		// GameObject.Find("Player").GetComponent<Player>().MonsterDeadDo();
		// //2.任务记录
		// GameObject.Find("Task").GetComponent<Task>().TaskWaitMonsterDeadDo();
		// //3.其他（比如成就记录，比如副本继续创建怪物 等等等）
		// GameObject.Find("Other").GetComponent<Other>().OtherWaitMonsterDeadDo();
		// //加N个处理逻辑

		//触发事件
		EventCenter.GetInstance().EventTrigger<Monster>("MonsterDead",this);
	
		EventCenter.GetInstance().EventTrigger("OtherSTH");
	}
}
