using System;
using UnityEngine;

public class house : MonoBehaviour
{
    public GameObject entryDialog;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("has been collided");
            entryDialog.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("left the col");
        entryDialog.SetActive(false);
    }
}
