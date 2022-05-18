using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator animator;

    protected AudioSource _audioSource;
    // Start is called before the first frame update
     protected  virtual  void Start()
     {
         animator = GetComponent<Animator>();
         _audioSource = GetComponent<AudioSource>();
     }

     public void Death()
     {
         //将碰撞体屏蔽了
         GetComponent<Collider2D>().enabled = false;
         Destroy(gameObject);
     }
     
     public void BeenKilled()
     {
         animator.SetTrigger("death");
         //play the clip of death
         _audioSource.Play();
     } 
}
