using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Test用

public class RayCheckScript : MonoBehaviour 
{

    public GameObject UiTest;

    enum MouseEvent{
        LeftCLick,
        RightClick
    }

    public Shader RimLightShader;  
	
    public Color RimColor = new Color(0.2F,0.8F,10.6F,1);  

    //定义私有变量以存储模型的原始信息  
    private MeshRenderer mSkin;  
    private Color mColor;  
    private Shader mShader;  

    MapChipScript mapChipScript;

    Coroutine coroutine;

    GameObject selectTower;

	// Use this for initialization
	void Start () {
        mapChipScript = MapChipScript.mapChipScript.GetMapChipScript();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown((int)MouseEvent.RightClick)) //点击鼠标右键
        {
            object ray = Camera.main.ScreenPointToRay(Input.mousePosition);//屏幕坐标转射线
            RaycastHit hit;//射线对象是：结构体类型（存储了相关信息）
            bool isHit = Physics.Raycast((Ray) ray, out hit);//发出射线检测到了碰撞   isHit返回的是 一个bool值
            // if (isHit &&  hit.transform.tag == "Tower" && type == MouseType.Idle)
            if (isHit &&  hit.transform.tag == "Tower")
            {
                // Debug.Log("坐标为：" + hit.point);
                // target = hit.point; //检测到碰撞，就把检测到的点记录下来
                // Debug.Log(hit.transform.gameObject.transform.position);

                mSkin = hit.transform.gameObject.GetComponent<MeshRenderer>();
                //获取默认颜色  
                mColor=mSkin.material.color;  
                //获取默认Shader  
                mShader=mSkin.material.shader;  

                if(selectTower != null && hit.transform.gameObject.transform.position != selectTower.transform.position)
                {
                    CancelSelect();
                }               
                else if(selectTower != null && hit.transform.gameObject.transform.position == selectTower.transform.position)
                {
                    CancelSelect();
                    return;
                }
                
                StartCoroutine( mapChipScript.SetMapShader(hit.transform.gameObject.GetComponent<TowerTest>().index,"_RimColor",RimColor,RimLightShader));

                mapChipScript.SetTowerSelected(hit.transform.gameObject.GetComponent<TowerTest>().index,true);

                selectTower = hit.transform.gameObject;
                selectTower.GetComponent<TowerTest>().isSelected = true;
                // Debug.Log("selectTower:"+selectTower.transform.position);

                UiTest.SetActive(true);
            }          
            
        }

	}

    public void CancelSelect()
    {
        UiTest.SetActive(false);
                
        StartCoroutine( mapChipScript.SetMapShader(selectTower.transform.gameObject.GetComponent<TowerTest>().index,"_RimColor",mColor,mShader));    
        mapChipScript.SetTowerSelected(selectTower.transform.gameObject.GetComponent<TowerTest>().index,false); 
        selectTower.GetComponent<TowerTest>().isSelected = false;         
        selectTower = null;
    }

}
