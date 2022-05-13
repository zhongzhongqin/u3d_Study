using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator animator;
    // Start is called before the first frame update
     protected  virtual  void Start()
     {
         animator = GetComponent<Animator>();
     }

     public void Death()
     {
         Destroy(gameObject);
     }
     
     public void BeenKilled()
     {
         animator.SetTrigger("death");
     } 
}
