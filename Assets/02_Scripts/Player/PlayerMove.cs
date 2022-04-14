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
    private bool isJump = false;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        Bounds bounds = capsuleCollider2D.bounds;
        footPosition = new Vector2(bounds.center.x, bounds.min.y);
        isGround = Physics2D.OverlapCircle(footPosition, 0.1f, groundLayer);
        float h = Input.GetAxisRaw("Horizontal");
        if(isGround)
        {
            isJump = false;
            if (h != 0)
            {
                anim.Play("PlayerMove");
                if (h < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }
            else
            {
                if (isJump)
                    anim.Play("PlayerJump");
                else
                    anim.Play("PlayerIdle");
            }
        }

     

        transform.Translate(new Vector2(h, 0) * speed * Time.deltaTime);


        if (isGround &&Input.GetButton("Jump"))
        {
            isJump = true;
            rigid.velocity = Vector2.zero;
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.Play("PlayerJump");
        }
    }

    
}
