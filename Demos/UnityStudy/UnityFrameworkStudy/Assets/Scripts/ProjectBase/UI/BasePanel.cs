using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 面板基类
/// 找到所有自己面板下的控件对象
/// 他应该提供显示或者隐藏的接口函数
/// 
/// 帮助我们通过代码快速找到所有的子控件
/// 方便我们在子类中处理逻辑
/// 节约找控件的工作量
/// </summary>
public class BasePanel : MonoBehaviour {

	private Dictionary<string, List<UIBehaviour>> controlDic = new Dictionary<string, List<UIBehaviour>>();

	// Use this for initialization
	void Awake() {
		FindChildrenControl<Button>();
		FindChildrenControl<Image>();
		FindChildrenControl<Text>();
		FindChildrenControl<Toggle>();
		FindChildrenControl<Slider>();
		FindChildrenControl<ScrollRect>();
	}
	
	/// <summary>
	/// 显示自己
	/// </summary>
	public virtual void ShowMe()
	{
		
	}

	/// <summary>
	/// 隐藏自己
	/// </summary>
	public virtual void HideMe()
	{

	}

	/// <summary>
	/// 得到对应名字的对应控件脚本
	/// </summary>
	/// <param name="controlName"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	protected T GetControl<T>(string controlName) where T : UIBehaviour
	{
		if(controlDic.ContainsKey(controlName))
		{
			for(int i = 0; i < controlDic[controlName].Count; i++)
			{
				if(controlDic[controlName][i] is T)
					return controlDic[controlName][i] as T;
			}
		}
		return null;
	}

	/// <summary>
	/// 找到子对象的对应控件
	/// </summary>
	/// <typeparam name="T"></typeparam>
	private void FindChildrenControl<T>() where T : UIBehaviour
	{
		T[] controls = this.GetComponentsInChildren<T>();
		
		for(int i = 0; i<controls.Length; i++)
		{
			string objName = controls[i].transform.name;
			if(controlDic.ContainsKey(objName))
				controlDic[objName].Add(controls[i]);
			else
				controlDic.Add(objName,new List<UIBehaviour>(){controls[i]});
		}
	}

}
