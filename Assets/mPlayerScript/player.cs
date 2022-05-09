using System;
using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMovement();
        switchAnim();
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
        }
        
        if (faceDircetion != 0)
        {
            transform.localScale = new Vector3(faceDircetion, 1, 1);
            animator.SetFloat("running",Mathf.Abs(faceDircetion));
        }

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jump * Time.deltaTime);
            animator.SetBool("jumping",true);
        }
    }

    void switchAnim()
    {
        // animator.SetBool("idle",false);
        if (animator.GetBool("jumping"))
        {
            //在跳跃，仍需判断何时降落
            if (rb.velocity.y < 0)
            {
                animator.SetBool("jumping",false);
                animator.SetBool("falling",true);
                
            }
        }else if (playerCollider2D.IsTouchingLayers(ground))
        {
            //碰撞到地面了
            animator.SetBool("falling",false);
            animator.SetBool("idle",true);
        }
    }
    
    // 物品碰撞检测
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.CompareTag("goods"))
        {
            //销毁物品
            Destroy(col.gameObject);
            Debug.Log("destroy the goods");
            return;
        }
    }
}
