﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 抽屉数据
/// 池子中的一列容器
/// </summary>
public class PoolData
{
	//抽屉中对象挂在的父节点
	public GameObject fatherObj;
	//对象的容器
	public List<GameObject> poolList;

	public PoolData(GameObject obj,GameObject poolObj)
	{
		//给我们的抽屉 创建一个父对象 并且把他作为我们pool(衣柜)对象的子物体
		fatherObj = new GameObject(obj.name);
		fatherObj.transform.parent = poolObj.transform;
		poolList = new List<GameObject>();
		PushObj(obj);
	}

	/// <summary>
	/// 往抽屉里面 压东西
	/// </summary>
	/// <param name="obj"></param>
	public void PushObj(GameObject obj)
	{
		//失活 让其隐藏
		obj.SetActive(false);
		//存起来
		poolList.Add(obj);
		//设置父对象
		obj.transform.parent = fatherObj.transform;
	}

	/// <summary>
	/// 从抽屉里面 取东西
	/// </summary>
	/// <returns></returns>
	public GameObject GetObj()
	{
		GameObject obj = null;
		//取出第一个
		obj = poolList[0];
		poolList.RemoveAt(0);
		//激活 让其显示
		obj.SetActive(true);
		//断开父子关系
		obj.transform.parent = null;

		return obj;
	}

}

/// <summary>
/// 缓存池魔块
/// 1.Dictionary List
/// 2.GameObject 和 Resource 两个公共类中的API
/// 
/// 减少GC次数
/// </summary>
public class PoolManager : BaseManager<PoolManager> {

	//缓存池容器（衣柜）
	public Dictionary<string,PoolData> poolDic = new Dictionary<string, PoolData>();

	private GameObject poolObj;

	/// <summary>
	/// 往外拿东西
	/// </summary>
	/// <param name="name"></param>
	/// <returns></returns>
	public GameObject GetObj(string name)
	{
		GameObject obj = null;
		//有抽屉 并且抽屉里有东西
		if(poolDic.ContainsKey(name) && poolDic[name].poolList.Count >0 )
		{
			obj = poolDic[name].GetObj();
		}
		else
		{
			obj = GameObject.Instantiate(Resources.Load<GameObject>(name));
			//把对象名字改的和池子名字一样
			obj.name = name;
		}
		return obj;
	}

	/// <summary>
	/// 把暂时不用的东西放进去
	/// </summary>
	/// <param name="name"></param>
	/// <param name="obj"></param>
	public void PushObj(string name,GameObject obj)
	{
		if(poolObj == null)
			poolObj = new GameObject("Pool");

		//里面有抽屉
		if(poolDic.ContainsKey(name))
		{
			poolDic[name].PushObj(obj);
		}
		//里面没有抽屉
		else
		{
			poolDic.Add(name,new PoolData(obj,poolObj));
		}
	}

	/// <summary>
	/// 清空缓存池 
	/// 主要用在切换场景时
	/// </summary>
	public void Clear()
	{
		poolDic.Clear();
		poolObj = null;
	}

}
