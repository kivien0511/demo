using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	void Start()
	{
		EventCenter.GetInstance().AddEventLinster<Monster>("MonsterDead",MonsterDeadDo);
	}

	/// <summary>
	/// 怪物死亡时要做些什么
	/// </summary>
	/// <param name="info"></param>
	public void MonsterDeadDo(Monster info)
	{
		Debug.Log("玩家获得奖励"+ info.name);
	}

	void OnDestroy()
	{
		EventCenter.GetInstance().RemoveEventLinster<Monster>("MonsterDead",MonsterDeadDo);
	}
}
