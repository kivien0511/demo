using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour {
    protected GameObject player;

    protected GameObject bullet;

    protected bool isUse = false;

	bool isBulletAttech = false;

    public virtual void AttechBullet(Collider2D other)
	{
		if(other.tag == "Bullet")
		{
			bool isBack = other.GetComponent<bullet>().isBack;

			if(isBack)
			{
				bullet = other.gameObject;
				isBulletAttech = true;
			}
		}		
	}

	public virtual void AttechPlayer(Collider2D other)
	{
		if(other.tag == "Player" && isBulletAttech)
		{			
            isUse = true;
		}
	}
}
