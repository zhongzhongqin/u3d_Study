using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : Enemy
{
    private Animator _animator;

    private Collider2D coll;

    private Rigidbody2D _rigidbody2D;

    public Transform upPos, downPos;

    private float _upY, _downY;
    
    public float mSpeed,jumpForce;
    
    private bool isUp = true;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _upY = upPos.position.y;
        _downY = downPos.position.y;
        transform.DetachChildren();
        Destroy(upPos.gameObject);
        Destroy(downPos.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        EagleMovement();
    }

    void EagleMovement()
    {
        if (isUp)
        {
            //向下运动
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.position.x, -mSpeed);
            if (transform.position.y <= _downY)
            {
                isUp = false;
            }
        }
        else
        {
            //向下运动
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.position.x, mSpeed);
            if (transform.position.y >= _upY)
            {
                isUp = true;
            }
        }
    }
    
}
