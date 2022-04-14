using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMove : MonoBehaviour
{
    
    public float speed;
    public float jumpPower;
    public float groundRayDistance = 1f;
    public LayerMask groundLayer;
    private bool isGround = false;
    private Vector3 footPosition;
    private CapsuleCollider2D capsuleCollider2D;
    private Animator anim = null;
    private Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        Move();
        Jump();
    }

    private void Jump()
    {
        if (isGround && Input.GetButton("Jump"))
        {
            anim.SetBool("isJump", true);
            rigid.velocity = Vector2.zero;
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    private void Move()
    {
        Bounds bounds = capsuleCollider2D.bounds;
        footPosition = new Vector2(bounds.center.x, bounds.min.y);
        isGround = Physics2D.OverlapCircle(footPosition, 0.1f, groundLayer);
        float h = Input.GetAxisRaw("Horizontal");
        if (isGround)
        {
            anim.SetBool("isJump", false);
            if (h != 0)
                anim.SetBool("isMove", true); 
            else
                anim.SetBool("isMove", false);
        }

        if (h < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
        transform.Translate(new Vector2(h, 0) * speed * Time.deltaTime);
    }

    private void DeathAnimator()
    {
        anim.Play("PlayerDie");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Trap"))
        {
            DeathAnimator();
        }
    }
}
