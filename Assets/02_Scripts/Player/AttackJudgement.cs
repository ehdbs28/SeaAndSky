using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackJudgement : MonoBehaviour
{
    
    public float speed;
    public float jumpPower;

    private void Start()
    {

    }

    private void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //피격시 튕기게
        if (collision.CompareTag("Enemy"))
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                GameObject playerObject = GameObject.Find("Player");
                Rigidbody2D py = playerObject.GetComponent<Rigidbody2D>();

                py.velocity = Vector3.up * jumpPower;
            }
            else if(PlayerMove.h >= 0)
            {
                GameObject playerObject = GameObject.Find("Player");
                Rigidbody2D py = playerObject.GetComponent<Rigidbody2D>();

                py.velocity = Vector3.left * speed;
            }
            
            else if(PlayerMove.h < 0)
            {
                GameObject playerObject = GameObject.Find("Player");
                Rigidbody2D py = playerObject.GetComponent<Rigidbody2D>();

                py.velocity = Vector3.right * speed;
            }
        }
    }
}
