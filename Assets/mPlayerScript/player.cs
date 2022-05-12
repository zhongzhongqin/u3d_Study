using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using  UnityEngine.UI;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class player : MonoBehaviour
{
    // Start is called before the first frame update

    //获取刚体
    private Rigidbody2D rb;
    //默认速度
    public float speed;
    //跳跃
    public float jump;
    //动画
    private Animator animator;
    //获取到地面
    public LayerMask ground;
    //获取碰撞体
    public Collider2D playerCollider2D;
    //获取的总物品数
    public int goodsCount = 0;
    //樱桃道具计数器
    public Text cherryCount;
    //受伤状态标记
    private bool hurtStat = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (!hurtStat)
        {
            switchAnim();
            PlayerMovement();
            
        }
        else
        {
            switchAnim();
            return;
        }
    }

    //角色移动
    void PlayerMovement(){
        //水平移动  //1 -1, 0
        float horizontalMove = Input.GetAxis("Horizontal");
       
        //方向
        float faceDircetion = Input.GetAxisRaw("Horizontal");
        
        if(horizontalMove != 0)
        {
            rb.velocity = new Vector2(horizontalMove * speed * Time.deltaTime,rb.velocity.y);
            animator.SetFloat("running",Mathf.Abs(faceDircetion));
            // animator.SetBool("squta",false);
            //当玩家播放受伤动画之后，移动了则恢复正常动画；
            animator.SetBool("hurt",false);
            hurtStat = false;
        }
        
        if (faceDircetion != 0)
        {
            if (!animator.GetBool("squta"))
            {
                //若处于蹲伏状态键入左右方向
                transform.localScale = new Vector3(faceDircetion, 1, 1);
                animator.SetFloat("running",Mathf.Abs(faceDircetion));
            }
            else
            {
                //蹲着位移
                animator.SetFloat("squta",Mathf.Abs(faceDircetion));
            }
        }

        if (Input.GetButtonDown("Jump") && playerCollider2D.IsTouchingLayers(ground))
        {
            Debug.Log("this is the stat"+hurtStat);
            if (!hurtStat)
            {
                //处于受伤状态键入jump
                rb.velocity = new Vector2(rb.velocity.x, jump * Time.deltaTime);
                animator.SetBool("jumping", true);
                animator.Play("jump");
                if (animator.GetBool("squta"))
                {
                    //若处于蹲伏状态键入jump
                    animator.SetBool("squta",false);
                }
            }
        }
        
        //蹲伏
        if (Input.GetButtonDown("Vertical"))
        {
            animator.SetBool("squta",true);
        }
    }

    //玩家跳跃/下落动画切换
    void switchAnim()
    {
        animator.SetBool("idle",false);
        if (animator.GetBool("jumping"))
        {
            //在跳跃，仍需判断何时降落
            if (rb.velocity.y < 0)
            {
                animator.SetBool("jumping",false);
                animator.SetBool("falling",true);
                
            }
        }
        else if (playerCollider2D.IsTouchingLayers(ground))
        {
            //碰撞到地面了
            animator.SetBool("falling",false);
            animator.SetBool("idle",true);
        }
        
        if(hurtStat)
        {  
             //
             if (Mathf.Abs(rb.velocity.x) < 0.1)
             {
                 hurtStat = false;
             }
        }
    }
    
    // 物品碰撞检测
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.tag);
        //隐式调用字符串比对，相较在if中显示比较性能高些
        if (col.gameObject.CompareTag("goods"))
        {
            //销毁物品
            Destroy(col.gameObject);
            goodsCount += 1;
            cherryCount.text = goodsCount.ToString();
            Debug.Log("destroy the goods");
            return;
        }
    }
    
    //enemy碰撞
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("enemy"))
        {
            //todo 销毁enemy 只有踩在敌人头上时，才会将其消灭
            
            var pos = col.gameObject.transform.position;
            
            if (animator.GetBool("falling"))
            {
                //已经起跳，处于下落状态
                Destroy(col.gameObject);
                Debug.Log("destroy the enemy");
                
                rb.velocity = new Vector2(0,6f);
            }
            else
            {
                if (animator.GetBool("squta"))
                {
                    //蹲着受伤的
                    this.animator.SetBool("squta",false);
                }
                //否则则是直接碰撞到敌人，播放受伤效果
                animator.SetBool("hurt",true);
                hurtStat = true;
 
                //播放完动画切换会正常动画状态；
                if (transform.position.x < pos.x)
                {
                    rb.velocity = new Vector2(-3f, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(3f, rb.velocity.y);
                }
            }
        }
    }
}
