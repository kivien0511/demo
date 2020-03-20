using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1.C#中泛型的知识
/// 2.设计模式中 单例模式的知识
/// </summary>
public class BaseManager<T> where T:new() {

	private static T instance;

	public static T GetInstance()
	{
		if( instance == null)
			instance = new T();
		return instance;
	}
}



