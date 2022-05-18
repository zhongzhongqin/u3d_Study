using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class Cherry : MonoBehaviour
{
    private Collider2D _col;
    // Start is called before the first frame update
    void Start()
    {
        _col = GetComponent<Collider2D>();
    }

    //play attain animation
    public void HasGot()
    {
        FindObjectOfType<player>().CherryCount();
        _col.enabled = false;
        Destroy(gameObject);
    }
    
    
}
