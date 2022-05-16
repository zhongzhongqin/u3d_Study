using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossum : Enemy
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
    private void Update()
    {
        OpossumMovement();
    }

    void OpossumMovement()
    {
        //负鼠的简单来回移动
        if (_isOrientationLeft)
        {
            ////跳跃动作
            //朝向左，则向左位移
            if (_collider.IsTouchingLayers(ground))
            {
                _mRig.velocity = new Vector2(-mSpeed ,jumpForce);
            }
            
            //当负鼠位置达到左右两点时，重新赋值朝向；
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
                _mRig.velocity = new Vector2(mSpeed ,jumpForce);
            }
            if (transform.position.x > rightX)
            {
                transform.localScale = new Vector3(1, 1, 1);
                _isOrientationLeft = true;
            }
        }
        
    }
}
