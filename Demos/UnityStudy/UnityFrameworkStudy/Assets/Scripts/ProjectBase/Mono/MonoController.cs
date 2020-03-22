using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Mono的管理者
/// 1.生命周期函数
/// 2.事件 委托
/// 3.协程
/// </summary>

public class MonoController : MonoBehaviour {

	private event UnityAction updateEvent;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if(updateEvent != null)
		{
			updateEvent();
		}
	}

	/// <summary>
	/// 给外部提供的添加帧更新事件的函数
	/// </summary>
	/// <param name="func"></param>
	public void AddUpdateLinster(UnityAction func)
	{
		updateEvent += func;
	}

	/// <summary>
	/// 提供给外部用于移除帧更新事件函数
	/// </summary>
	/// <param name="func"></param>
	public void RemoveUpdateLinster(UnityAction func)
	{
		updateEvent -= func;
	}
}
