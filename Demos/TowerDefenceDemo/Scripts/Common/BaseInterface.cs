using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInterface : MonoBehaviour {

	//设置射线在Plane上的目标点target
    private Vector3 target;

	enum MouseType{
		Idle,
		Stay
	}

	MouseType mouseType = MouseType.Idle;

	enum MouseEvent{
        LeftCLick,
        RightClick
    }

	// Use this for initialization
	void Start () {
		
	}

	void Update()
	{
		
	}

	protected void ClickEvent()
	{
		if (Input.GetMouseButtonUp((int)MouseEvent.RightClick)) //点击鼠标右键
        {
            object     ray = Camera.main.ScreenPointToRay(Input.mousePosition); //屏幕坐标转射线
            RaycastHit hit;                                                     //射线对象是：结构体类型（存储了相关信息）
            bool       isHit = Physics.Raycast((Ray) ray, out hit);             //发出射线检测到了碰撞   isHit返回的是 一个bool值
            if (isHit )
            {
                Debug.Log("坐标为：" + hit.point);
                target = hit.point; //检测到碰撞，就把检测到的点记录下来
				Debug.Log(hit.transform.gameObject.name);
            }
        }
	}

}
