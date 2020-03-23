using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// UI层级
/// </summary>
public enum E_UI_Layer
{
	Bot,
	Mid,
	Top,
	System,
}

/// <summary>
/// UI管理器
/// 1.管理所有显示的面板
/// 2.提供给外部显示和隐藏等等接口
/// </summary>
public class UIManager : BaseManager<UIManager> 
{
	public Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

	private Transform bot;
	private Transform mid;
	private Transform top;
	private Transform system;

	public UIManager()
	{
		//创建Canvas 让其过场景的时候不被移除
		GameObject obj = ResourcesManager.GetInstance().Load<GameObject>("UI/Canvas");
		Transform canvas = obj.transform;
		GameObject.DontDestroyOnLoad(obj);

		//找到各层
		bot = canvas.Find("Bot");
		mid = canvas.Find("Mid");
		top = canvas.Find("Top");
		system = canvas.Find("System");

		//创建EventSystem 让其过场景的时候不被移除
		obj = ResourcesManager.GetInstance().Load<GameObject>("UI/EventSystem");
		GameObject.DontDestroyOnLoad(obj);
	}

	/// <summary>
	/// 显示面板
	/// </summary>
	/// <param name="panelName">面板名</param>
	/// <param name="layer">显示在哪一层</param>
	/// <param name="callback">当面板预设体创建成功后 你想做的事</param>
	/// <typeparam name="T">显示面板类型</typeparam>
	public void ShowPanel<T>(string panelName,E_UI_Layer layer = E_UI_Layer.Mid,UnityAction<T> callback = null) where T : BasePanel
	{
		if(panelDic.ContainsKey(panelName))
		{
			//TODO：显示层级可能会出问题
			panelDic[panelName].ShowMe();
			//处理面板创建完成后的逻辑
			if(callback != null)
				callback(panelDic[panelName] as T);
			return;
		}

		ResourcesManager.GetInstance().LoadAsync<GameObject>("UI/"+panelName,(obj)=>{
			//把他作为Canvas的子对象
			//并且要设置它的相对位置
			//找到父对象 你到底显示在哪一层
			Transform father = bot;
			switch(layer)
			{
				case E_UI_Layer.Mid:
					father = mid;
					break;
				case E_UI_Layer.Top:
					father = top;
					break;
				case E_UI_Layer.System:
					father = system;
					break;
			}

			//设置父对象 设置相对位置和大小
			obj.transform.SetParent(father);

			obj.transform.localPosition = Vector3.zero; 
			obj.transform.localScale = Vector3.one;

			(obj.transform as RectTransform).offsetMax = Vector2.zero;
			(obj.transform as RectTransform).offsetMin = Vector2.zero;

			//得到预设体身上的面板脚本
			T panel = obj.GetComponent<T>();
			//处理面板创建完成后的逻辑
			if(callback != null)callback(panel);

			panel.ShowMe();
			//把面板存起来
			panelDic.Add(panelName,panel);
		});
	}

	/// <summary>
	/// 隐藏面板
	/// 
	/// 缺点：可能会卡
	/// </summary>
	/// <param name="panelName"></param>
	public void HidePanel(string panelName)
	{
		if(panelDic.ContainsKey(panelName))
		{	
			panelDic[panelName].HideMe();
			GameObject.Destroy(panelDic[panelName].gameObject);
			panelDic.Remove(panelName);
		}
	}
}
