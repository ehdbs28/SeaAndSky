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
    private Animator anim = null;
    private SpriteRenderer sprite = null;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        Bounds bounds = capsuleCollider2D.bounds;
        footPosition = new Vector2(bounds.center.x, bounds.min.y);
        isGround = Physics2D.OverlapCircle(footPosition, 0.1f, groundLayer);
        float h = Input.GetAxisRaw("Horizontal");
        if(h != 0)
        {
            anim.Play("PlayerMove");
        }else
        {
            anim.Play("PlayerIdle");
        }
        transform.Translate(new Vector2(h, 0) * speed * Time.deltaTime);

        //반대방향 돌리기 예정

        if (isGround &&Input.GetButton("Jump"))
        {
            rigid.velocity = Vector2.up * jumpPower;
        }
    }

    
}
