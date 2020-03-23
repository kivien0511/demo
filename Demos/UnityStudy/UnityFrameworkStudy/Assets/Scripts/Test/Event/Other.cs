using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Other : MonoBehaviour {

	void Start()
	{
		EventCenter.GetInstance().AddEventLinster<Monster>("MonsterDead",OtherWaitMonsterDeadDo);

		EventCenter.GetInstance().AddEventLinster("OtherSTH",OtherSomeThing);
	}

	/// <summary>
	/// 怪物死亡时要做些什么
	/// </summary>
	/// <param name="info"></param>
	public void OtherWaitMonsterDeadDo(Monster info)
	{
		Debug.Log("其他各个对象要做的事");
	}

	public void OtherSomeThing()
	{
		Debug.Log("其他处理123");
	}

	void OnDestroy()
	{
		EventCenter.GetInstance().RemoveEventLinster<Monster>("MonsterDead",OtherWaitMonsterDeadDo);

		EventCenter.GetInstance().RemoveEventLinster("OtherSTH",OtherSomeThing);

	}
}
