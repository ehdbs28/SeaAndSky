using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ConstantManager;

public class PlayerMove : MonoBehaviour
{
    public float speed = 10f;
    public float jump = 5f;

    private Rigidbody2D rigidbody;


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Moving();
        Jump();
    }

    void Moving()
    {
        float h = Input.GetAxisRaw("Horizontal");

        transform.Translate(h * speed * Time.deltaTime, 0, 0);
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(GameManager.Instance.playerJumpCount > 0) { 
                rigidbody.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
                GameManager.Instance.playerJumpCount--;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "DoubleJump")
        {
            EventManager.TriggerEvent(DOUBLE_JUMP);
        }
        else if (collision.tag == "CreateObject")
        {
            EventManager.TriggerEvent(SPAWN_OBJECT);
        }
        else if (collision.tag == "Teleport")
        {
            EventManager.TriggerEvent(TELEPORTATION);
        }
    }

}


