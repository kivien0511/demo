using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;

public class Enemy : MonoBehaviour {

[HideInInspector]
	public bool isCold = false;

	Coroutine coroutine;

	[HideInInspector]
	public GameObject player;

	public int target = 3;

	[HideInInspector]
	public int cold = 1;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		StartCoroutine(SampleAI());
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.rotation = Quaternion.identity;

		if(isCold && coroutine == null)
		{
			StopCoroutine(SampleAI());
			coroutine = StartCoroutine(Back());
			
		}
	}

	IEnumerator SampleAI()
	{
		Vector2 v = this.transform.position - player.transform.position;
	  while(!isCold)
		{
			if(player.GetComponent<PlayerActivity>().count <= target)
			{
				//玩家与分身位置向量获得
        v = this.transform.position - player.transform.position;
          
        //分身朝玩家位置移动
        transform.Translate(-v*Time.deltaTime/2/cold);
				yield return 0;
			}
			else
			{
				v = this.transform.position - player.transform.position;
				if(GetV(v) <= 2)
				{          
					//分身朝玩家位置移动
					transform.Translate(v*Time.deltaTime/2/cold);
					yield return 0;
				}
			}
			yield return 0;
		}
		
	}

	IEnumerator Back()
  {
    //玩家与分身初始位置向量获得
      Vector2 v = this.transform.position - player.transform.position;

      while(GetV(v) >= 0.5f)
      {
				if(TCKInput.GetTouchPhase("Joystick") == ETouchPhase.NoTouch)
        {
        	yield return 0;
         	continue;
        }
          
				//玩家与分身位置向量获得
        v = this.transform.position - player.transform.position;
          
        //分身朝玩家位置移动
        transform.Translate(-v*Time.deltaTime*2);
        yield return 0;            
      }        

		  //分身收回
      player.GetComponent<PlayerActivity>().count += 1;

			//吃掉敌人回复精力值
			player.GetComponent<PlayerActivity>().Energy.GetComponent<Energy>().time = 3;
			
      //消灭分身object
      GameObject.Destroy(this.gameObject);
  }

  	//计算玩家与分身位置向量的距离
    float GetV(Vector2 v)
    {
        float f = 100;
        f = Mathf.Sqrt(v.x * v.x + v.y * v.y);
        return f;
    }

    void OnTriggerStay2D(Collider2D other)
    {
			if(other.tag == "Bullet" )
			{
				other.GetComponent<Bullet>().attechEnemy = true;
				// player = other.GetComponent<Bullet>().player;
				//isCold = other.GetComponent<Bullet>().isTouchDown;
			}
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			if(!isCold)
			{
				Vector2 v = this.transform.position - player.transform.position;
				player.GetComponent<Rigidbody2D>().velocity = -v;
				player.GetComponent<PlayerActivity>().bullet.GetComponent<Bullet>().isDamaged = true;

				float x = Random.Range(0,11)/10;
				float y = (Random.Range(50,61)-50)/10;
				Vector2 move = new Vector2(x,y);
				player.GetComponent<PlayerActivity>().Shoot(move);
			}
			else
			{
				//分身收回
				player.GetComponent<PlayerActivity>().count += 1;

				//吃掉敌人回复精力值
				player.GetComponent<PlayerActivity>().Energy.GetComponent<Energy>().time = 3;
				
				//消灭分身object
				GameObject.Destroy(this.gameObject);
			}

			
		}
		if(other.tag == "Bullet" && !isCold)
		{
			// player = other.GetComponent<Bullet>().player;
			player.GetComponent<PlayerActivity>().Energy.GetComponent<Energy>().time = 3;
			isCold = true;
		}

			player.GetComponent<PlayerActivity>().bullet.GetComponent<Bullet>().isDamaged = false;
	}

}
