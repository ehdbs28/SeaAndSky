using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    Rigidbody2D rigid;
    public float groundRayDistance = 1f;
    private bool isGround = false;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        transform.Translate(new Vector2(h, 0) * speed * Time.deltaTime);

        isGround = IsGround();
        Debug.Log(isGround);
        if (isGround &&Input.GetButton("Jump"))
        {
            rigid.velocity = Vector2.up * jumpPower;
        }
    }

    private bool IsGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundRayDistance, LayerMask.NameToLayer("Platform"));
        Debug.Log(hit.collider.name);
        Debug.DrawRay(rigid.position, Vector2.down, Color.red,groundRayDistance);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }
}
