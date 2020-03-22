using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour {

	void Start()
	{
		EventCenter.GetInstance().AddEventLinster("MonsterDead",TaskWaitMonsterDeadDo);
	}

	/// <summary>
	/// 怪物死亡时要做些什么
	/// </summary>
	/// <param name="info"></param>
	public void TaskWaitMonsterDeadDo(object info)
	{
		Debug.Log("任务记录");
	}

	void OnDestroy()
	{
		EventCenter.GetInstance().RemoveEventLinster("MonsterDead",TaskWaitMonsterDeadDo);
	}
}
