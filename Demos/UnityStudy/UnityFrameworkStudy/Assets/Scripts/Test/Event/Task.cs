using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour {

	void Start()
	{
		EventCenter.GetInstance().AddEventLinster<Monster>("MonsterDead",TaskWaitMonsterDeadDo);
	}

	/// <summary>
	/// 怪物死亡时要做些什么
	/// </summary>
	/// <param name="info"></param>
	public void TaskWaitMonsterDeadDo(Monster info)
	{
		Debug.Log("任务记录");
	}

	void OnDestroy()
	{
		EventCenter.GetInstance().RemoveEventLinster<Monster>("MonsterDead",TaskWaitMonsterDeadDo);
	}
}
