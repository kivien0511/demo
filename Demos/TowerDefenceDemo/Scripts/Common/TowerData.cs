using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerData  {

	public int index{get;set;}
	public int TowerType{get;set;}

	public int TowerLevel{get;set;}

	public Vector3 TowerPostion{get;set;}

	public bool isSelected{get;set;}

	public int price{get;set;}

	public bool isDelected{get;set;}

	public TowerData(int index,int type,int level,Vector3 position,int price,bool isSelected = false,bool isDelected = false)
	{
		this.index = index;
		this.TowerType = type;
		this.TowerLevel = level;
		this.TowerPostion = position;
		this.price = price;
		this.isSelected = isSelected;		
		this.isDelected = isDelected;
	}

}
