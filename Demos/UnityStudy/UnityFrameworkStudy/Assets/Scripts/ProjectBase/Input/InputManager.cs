using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1.Input类
/// 2.事件中心模块
/// 3.公共Mono模块的使用
/// </summary>
public class InputManager : BaseManager<InputManager> 
{
	//按键输入检测列表
	private List<KeyCode> inputList;

	private bool isStart = false;

	/// <summary>
	/// 构造函数中 添加Update监听
	/// </summary>
	public InputManager()
	{
		inputList = new List<KeyCode>();
		MonoManager.GetInstance().AddUpdateLinster(InputUpdate);
	}

	public void SetKeyCode(KeyCode key)
	{
		inputList.Add(key);
	}

	/// <summary>
	/// 是否开启或关闭输入检测
	/// </summary>
	public void StartOrEndCheck(bool isOpen)
	{
		isStart = isOpen;
	}

	/// <summary>
	/// 用来检测按键抬起按下 分发事件
	/// </summary>
	/// <param name="key"></param>
	private void CheckKeyCode(KeyCode key)
	{
		//事件中心模块 分发按下抬起事件
		if(Input.GetKeyDown(key))
			EventCenter.GetInstance().EventTrigger<KeyCode>("KeyDown",key);
		//事件中心模块 分发按下抬起事件
		if(Input.GetKeyUp(key))
			EventCenter.GetInstance().EventTrigger<KeyCode>("KeyUp",key);
	}

	private void InputUpdate()
	{
		//没有开启输入检测 就不去检测 直接return
		if(!isStart)return;

		inputList.ForEach((x)=>{CheckKeyCode(x);});

		// for(int i = 0; i < inputList.Count; i++)
		// {
		// 	CheckKeyCode(inputList[i]);
		// }

		// CheckKeyCode(KeyCode.W);
		// CheckKeyCode(KeyCode.S);
		// CheckKeyCode(KeyCode.A);
		// CheckKeyCode(KeyCode.D);
	}
}
