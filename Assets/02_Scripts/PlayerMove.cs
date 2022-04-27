using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
            Debug.Log("df");
            rigidbody.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
        }
    }

    
}
