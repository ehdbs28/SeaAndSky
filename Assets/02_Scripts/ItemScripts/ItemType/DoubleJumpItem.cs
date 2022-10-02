using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpItem : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(true);  
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            DoubleJump();
        }
    }

    void DoubleJump()
    {
        GameObject.Find("Player").GetComponent<Player>().JumpCount++;
        gameObject.SetActive(false);
    }
}
