using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    Rigidbody2D rigid;
    public float groundRayDistance = 1f;
    public LayerMask groundLayer;
    private bool isGround = false;
    private Vector3 footPosition;
    private CapsuleCollider2D capsuleCollider2D;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    
    void Update()
    {
        Bounds bounds = capsuleCollider2D.bounds;
        footPosition = new Vector2(bounds.center.x, bounds.min.y);
        isGround = Physics2D.OverlapCircle(footPosition, 0.1f, groundLayer);
        float h = Input.GetAxisRaw("Horizontal");
        transform.Translate(new Vector2(h, 0) * speed * Time.deltaTime);

        
        Debug.Log(isGround);
        if (isGround &&Input.GetButton("Jump"))
        {
            rigid.velocity = Vector2.up * jumpPower;
        }
    }

    
}
