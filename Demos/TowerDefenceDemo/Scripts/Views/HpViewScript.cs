using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpViewScript : MonoBehaviour {

	Slider HPStrip;    //添加血条Slider的引用
    public int HP;
    void Start () 
	{
		HPStrip = this.transform.GetComponent<Slider>();
		HP = this.transform.parent.transform.parent.GetComponent<EnemyScript>().hp;
        HPStrip.value = HPStrip.maxValue = HP;    //初始化血条
		this.transform.parent.transform.rotation = Camera.main.transform.rotation;
    }
    public void OnHit(int damage)
	{
        HP -= damage;
        HPStrip.value=HP;    //适当的时候对血条执行操作
    }
}
