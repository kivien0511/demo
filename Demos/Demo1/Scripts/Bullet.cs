using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;

public class Bullet : MonoBehaviour {

    //分身初始速度
	[HideInInspector]
	 public Vector2 vel;

	Rigidbody2D rigid;

    [HideInInspector]
    //分身收回flag
    public bool isTouchDown = false;
    
    //玩家object
    [HideInInspector]
    public GameObject player;

    [HideInInspector]
    public Coroutine coroutine;

    [HideInInspector]
    public bool attechEnemy = false;

    [HideInInspector]
    public bool isBack = false;

    bool isDestroy = false;

    public GameObject Paint;

    float time = 0;

    Vector3 position;

    public bool isDamaged = false;

    bool isOut = false;

	// Use this for initialization
	void Start () {
        // Debug.Log("b"+vel);
		rigid = GetComponent<Rigidbody2D>();        
		rigid.velocity += vel.normalized*10;
        position = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {	
        this.transform.rotation = Quaternion.identity;

    

        // {
        //     time += Time.deltaTime;
        //     if(time >= 0.03)
        //     {
        //         if(position != this.transform.position)
        //         {
        //             Instantiate(Paint, transform.position , Quaternion.Euler(Vector3.zero));
        //             position = this.transform.position;
        //         }
                
        //         time = 0;
        //     }
        // }

        //凃地
        // {
        //     Vector2 p = position - this.transform.position;
        //     if(GetV(p) >= 0.25)
        //     {
        //         if(position != this.transform.position)
        //         {
        //             Instantiate(Paint, transform.position , Quaternion.Euler(Vector3.zero));
        //             position = this.transform.position;
        //         }
        //     }
        // }

        //获取玩家的flag
        isTouchDown = player.GetComponent<PlayerActivity>().isTouchDown;
        
        bool isOverTime = player.GetComponent<PlayerActivity>().isOverTime;

        Vector2 v = this.transform.position - player.transform.position;

        if(GetV(v) <= player.transform.localScale.y/2+ 0.3f)
        {
            this.gameObject.GetComponent<Collider2D>().isTrigger = true;
        }

        //按下按钮并且没有触发返回线程时
        if(isTouchDown && coroutine == null && !isOverTime)
        {        
           

            isBack = true;
            this.GetComponent<Collider2D>().isTrigger = false; 
            
            if(isDamaged)
            {
                return;
            }
            // Debug.Log("bullet back");
            //开启返回线程   
            coroutine = StartCoroutine(Back());            
        }
        else
        {
            //减速
            SpeedDown();
            StopMove();
        }		

        if(isDestroy && this.gameObject.GetComponent<Collider2D>().isTrigger)
        {
            isDestroy = false;
            // StopCoroutine(coroutine);
            DestroyBullet();
        }
	}

    public IEnumerator Back()
    {
        this.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;


        //玩家与分身初始位置向量获得
        Vector2 v = this.transform.position - player.transform.position;

        rigid.velocity *= 0;

        while(GetV(v) >= 0.5f)
        {
            if(TCKInput.GetTouchPhase("Joystick") == ETouchPhase.NoTouch)
            {              
                if(isDestroy)
                {
                    isDestroy = false;
                    break;
                }
                yield return 0;
                continue;
            }

            if(isDestroy)
            {
                break;
            }

            //玩家与分身位置向量获得
            v = this.transform.position - player.transform.position;
            
            //分身朝玩家位置移动
            transform.Translate(-v*Time.deltaTime*2); 

            yield return 0;            
        }        

        isDestroy = true;
    }

    //计算玩家与分身位置向量的距离
    float GetV(Vector2 v)
    {
        float f = 100;
        f = Mathf.Sqrt(v.x * v.x + v.y * v.y);
        return f;
    }

    //获得分身当前速度
	float VelocityCal()
    {
        float v = (rigid.velocity.x) * (rigid.velocity.x) + (rigid.velocity.y) * (rigid.velocity.y);
        v = Mathf.Sqrt(v);
        return v;
    }

    //阻力
    void SpeedDown()
    {
        if (VelocityCal()>0)
        {
            rigid.velocity -= rigid.velocity / 30f;
        }
    }

    public void StopMove()
    {
        if(attechEnemy && !isDamaged)
        {
            rigid.velocity *= 0;
            
        }       

    }

    void DestroyBullet()
    {
        //分身收回
        player.GetComponent<PlayerActivity>().count += 1;

        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        //消灭分身object
        GameObject.Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(isBack)
            {
                isDestroy = true;
            }           
        }

        if(other.tag == "Enemy" && !isDamaged)
        {
            other.GetComponent<Enemy>().isCold = true;     
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(isOut)
            {
                isDestroy = true;
            }            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            this.GetComponent<Collider2D>().isTrigger = false;  
            isOut = true;
        }
    }


}
