using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy
{

    private Collider2D _collider;
    private Rigidbody2D _mRig;

    private Animator _animator;

    public Transform leftPos;

    public Transform rightPos;

    private float letfX, rightX;

    public float mSpeed,jumpForce;

    private bool _isOrientationLeft = true;
    //获取到地面
    public LayerMask ground;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        _mRig = GetComponent<Rigidbody2D>();
        letfX = leftPos.position.x;
        rightX = rightPos.position.x;
        transform.DetachChildren();
        Destroy(rightPos.gameObject);
        Destroy(leftPos.gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        SwitchAnim();   
    }

    void FrogMovement()
    {
        //青蛙的简单来回移动
        if (_isOrientationLeft)
        {
            ////跳跃动作
            //朝向左，则向左位移
            if (_collider.IsTouchingLayers(ground))
            {
                _animator.SetBool("jump",true);
                _mRig.velocity = new Vector2(-mSpeed ,jumpForce);
            }
            
            //当青蛙位置达到左右两点时，重新赋值朝向；
            if (transform.position.x <= letfX)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                _isOrientationLeft = false;
            }
        }
        else
        {
            //朝向右，则向右位移
            if (_collider.IsTouchingLayers(ground))
            {
                _animator.SetBool("jump",true);
                _mRig.velocity = new Vector2(mSpeed ,jumpForce);
            }
            if (transform.position.x > rightX)
            {
                transform.localScale = new Vector3(1, 1, 1);
                _isOrientationLeft = true;
            }
        }
        
    }

    void SwitchAnim()
    {
        //切换下落动画
        if (_animator.GetBool("jump"))
        {
            if (_mRig.velocity.y < 0.1f)
            {
                _animator.SetBool("jump",false);
                _animator.SetBool("down",true);
            }
        }

        //切换普通动画
        if (_collider.IsTouchingLayers(ground) && _animator.GetBool("down"))
        {
            _animator.SetBool("down",false);
            _animator.SetBool("jump",false);
        }
    }
}
