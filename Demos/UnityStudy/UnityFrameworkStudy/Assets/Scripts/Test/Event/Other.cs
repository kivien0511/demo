using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Other : MonoBehaviour {

	void Start()
	{
		EventCenter.GetInstance().AddEventLinster("MonsterDead",OtherWaitMonsterDeadDo);
	}

	/// <summary>
	/// 怪物死亡时要做些什么
	/// </summary>
	/// <param name="info"></param>
	public void OtherWaitMonsterDeadDo(object info)
	{
		Debug.Log("其他各个对象要做的事");
	}

	void OnDestroy()
	{
		EventCenter.GetInstance().RemoveEventLinster("MonsterDead",OtherWaitMonsterDeadDo);
	}
}
