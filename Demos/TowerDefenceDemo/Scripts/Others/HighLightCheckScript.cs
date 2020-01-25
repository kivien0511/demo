using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLightCheckScript : MonoBehaviour {


    //我们今天使用Shader来直接改变模型的渲染效果，这样可以避免使用一个材质  

    public Shader RimLightShader;  

	
    public Color RimColor = new Color(0.2F,0.8F,10.6F,1);  

    //定义私有变量以存储模型的原始信息  

    private MeshRenderer mSkin;  

    private Color mColor;  

    private Shader mShader;  

  

    void Start ()   
    {  
		mSkin = this.gameObject.transform.GetComponent<MeshRenderer>();

        //获取默认颜色  
        mColor=mSkin.material.color;  

        //获取默认Shader  
        mShader=mSkin.material.shader;  
    }  

  

    void Update ()   
    {  

       //获取鼠标位置  
       Vector3 mPos=Input.mousePosition;  

       //向物体发射射线  
       Ray mRay=Camera.main.ScreenPointToRay(Input.mousePosition);  
       RaycastHit mHit;  
       //射线检验  
       if(Physics.Raycast(mRay,out mHit))  
       {  

          //Cube  

          if(mHit.collider.gameObject.name=="Cube")  
          {  


			mHit.collider.gameObject.GetComponent<MeshRenderer>().material.shader=RimLightShader;  

            mHit.collider.gameObject.GetComponent<MeshRenderer>().material.SetColor("_RimColor",RimColor);  

             //将模型恢复到初始状态  

             GameObject.Find("Sphere").GetComponent<MeshRenderer>().material.shader=mShader;  

             GameObject.Find("Sphere").GetComponent<MeshRenderer>().material.SetColor("_RimColor",mColor);  
          }  

          //Sphere  
          if(mHit.collider.gameObject.name=="Sphere")  
          {  

			 mHit.collider.gameObject.GetComponent<MeshRenderer>().material.shader=RimLightShader;  

             mHit.collider.gameObject.GetComponent<MeshRenderer>().material.SetColor("_RimColor",RimColor);  

             //将模型恢复到初始状态  

            GameObject.Find("Cube").GetComponent<MeshRenderer>().material.shader=mShader;  

            GameObject.Find("Cube").GetComponent<MeshRenderer>().material.SetColor("_RimColor",mColor);  

          }  

            
            




       }  

  

    }  

}  
