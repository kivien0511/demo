using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;
using UnityEngine.UI;

public class PlayerActivity : MonoBehaviour {

    public GameObject bullet;

    public GameObject Energy;

    public GameObject joystick;

    // Vector2 position;
    Transform myTransform;

    Rigidbody2D rigid;

    float chargeTime;


    bool isTouchUp = false;
    [HideInInspector]
    public bool isTouchDown = false;

    Coroutine coroutine;

    // ETouchPhase touchPhase;

    Vector2 move;

    public GameObject text;

    [HideInInspector]
    public float count = 5;

    [HideInInspector]
    public bool isOverTime = false;

    float pressTime = 3f;

    float bulletTime = 1f;

    bool isCancelShoot = false;

    float speedown = 0;

    void Awake()
    {
        myTransform = this.transform;
        rigid = this.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        this.transform.rotation = Quaternion.identity;


        //UI显示用（当前可射出分身数量）
        text.GetComponent<Text>().text = count.ToString();

        this.gameObject.transform.localScale = new Vector2(count/5,count/5);

        if(TCKInput.GetTouchPhase("Joystick") == ETouchPhase.Stationary || TCKInput.GetTouchPhase("Joystick") == ETouchPhase.Moved)
        {

            if(Energy.GetComponent<Energy>().time > 0)
            {
                Energy.GetComponent<Energy>().time -= Time.deltaTime;

                //子弹时间开启    
                Time.timeScale = 0.3f;
            }            

            if(pressTime >= 0)
            {
                pressTime -= Time.deltaTime;
            }            
            
            if(Energy.GetComponent<Energy>().time <= 0)
            {
                //子弹时间关闭            
                Time.timeScale = 1.0f;
            }

            if(pressTime <= 0)
            { 
                joystick.GetComponent<TCKJoystick>().touchDown = false;               
                joystick.GetComponent<TCKJoystick>().touchPhase = ETouchPhase.NoTouch;

                isOverTime = true;
                isTouchUp = true;
                isCancelShoot = false;
            }

            //获取移动方向     
            move = TCKInput.GetAxis("Joystick");

            if(Mathf.Abs(move.x) <= 0.3f && Mathf.Abs(move.y) <= 0.3f)
            {
                //取消发射判定
                isCancelShoot = true;
            }
            else
            {
                isCancelShoot = false;
            }
           
        }       
        else
        {
            Time.timeScale = 1.0f;

            if(Energy.GetComponent<Energy>().time <= 3)
            {
                Energy.GetComponent<Energy>().time += 0.05f*Time.deltaTime;
            }            
        }

        //移动（生成分身）
        if(isTouchUp && count > 1 && !isCancelShoot)
        {
            // Debug.Log("Shoot");
            // rigid.velocity += move;

            Shoot(move);

            //如果已经有移动线程 先关闭
            if(coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }

            //移动线程
            coroutine = StartCoroutine(Move(move.normalized*2));
        }

        // //重置开火状态    
        isTouchUp = false;      
    }

    public void Shoot(Vector2 move)
    {
        //可射出分身数量-1
        count --;
        
        //子弹移动
        bullet.GetComponent<Bullet>().vel = -move*VelocityCal(move)*0.6f;
        bullet.GetComponent<Bullet>().player = this.gameObject;

        //子弹生成
        Instantiate(bullet, transform.position , Quaternion.identity);
    }

    //移动处理
    IEnumerator Move(Vector2 m)
    {
        rigid.velocity += m;
        yield return 0;
        while(VelocityCal(rigid.velocity) > 0)
        {
            SpeedDown();
            yield return 0;
        }
        
    }


    void TouchEvent()
    {
    //    Debug.Log(TCKInput.GetTouchPhase("leftJoystick").ToString() == "Stationary");
       Debug.Log("jsk"+TCKInput.GetTouchPhase("Joystick"));
    }

    public void TouchUp()
    {    
        if(!isOverTime && !isCancelShoot)
        {
            isTouchUp = true; 
        }
        
        pressTime = 3f;
        isOverTime = false;        
        isTouchDown = false;
        
    }

    public void TouchDown()
    {
        isTouchDown = true;        
    }

    //计算当前本体速度
    float VelocityCal(Vector2 vec)
    {
        float v = (vec.x) * (vec.x) + (vec.y) * (vec.y);
        v = Mathf.Sqrt(v);
        return v;
    }

    //阻力
    void SpeedDown()
    {
        if (VelocityCal(rigid.velocity)>0)
        {
            // rigid.velocity -= rigid.velocity / 50f;

            speedown += 3*Time.deltaTime;
            rigid.velocity -= rigid.velocity / (40f+speedown);  

            if(VelocityCal(rigid.velocity)<=0)
            {
                speedown = 0;
            }
        }
    }    

}
