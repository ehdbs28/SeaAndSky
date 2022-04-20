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
    private bool isAttack = false;
    public GameObject swordAttackPrefab1;
    public GameObject swordAttackPrefab2;
    public static float h;
    public LayerMask enemyLayer;
    private bool isHead = false;

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
        PlayerAttack();
        //EnemyHeadJump();
        MoveLimit();
    }

    private void MoveLimit()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
            if (pos.x < 0f) pos.x = 0f;
            if (pos.x > 1f) pos.x = 1f;
            if (pos.y < 0f) pos.y = 0f;
            if (pos.y > 1f) pos.y = 1f;
        transform.position = Camera.main.ViewportToWorldPoint(pos);

    }
    //점프
    private void Jump()
    {
        if (isGround && Input.GetButton("Jump"))
        {
            anim.SetBool("isJump", true);
            rigid.velocity = Vector2.zero;
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    //머리밟고 점프
    private void EnemyHeadJump()
    {
        Bounds bounds = capsuleCollider2D.bounds;
        footPosition = new Vector2(bounds.center.x, bounds.min.y);
        isHead = Physics2D.OverlapCircle(footPosition, 0.1f, enemyLayer);
        if (isHead)
        {
            anim.SetBool("isJump", true);
            rigid.velocity = Vector2.zero;
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    //움직이기
    private void Move()
    {
        Bounds bounds = capsuleCollider2D.bounds;
        footPosition = new Vector2(bounds.center.x, bounds.min.y);
        isGround = Physics2D.OverlapCircle(footPosition, 0.1f, groundLayer);
        h = Input.GetAxisRaw("Horizontal");
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

    //공격실행
    private void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!isAttack)
            {
                StartCoroutine(Attack());
                anim.Play("PlayerAttack");
                isAttack = true;
                
            }
        }
    }

    //플레이어 공격
    IEnumerator Attack() 
    {
        //아래공격
        if (Input.GetKey(KeyCode.DownArrow))
        {
            GameObject swordAttack1;
            GameObject swordAttack2;
            swordAttack1 = Instantiate(swordAttackPrefab1);
            swordAttack1.transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y - 1, 0), Quaternion.Euler(0, 0, -90));
            yield return new WaitForSeconds(0.1f);
            swordAttack2 = Instantiate(swordAttackPrefab2);
            swordAttack2.transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y - 1, 0), Quaternion.Euler(0, 0, -90));
            isAttack = false;
        }
        //오른쪽공격
        else if(h >= 0)
        {
            GameObject swordAttack1;
            GameObject swordAttack2;
            swordAttack1 = Instantiate(swordAttackPrefab1);
            swordAttack1.transform.position = new Vector3(transform.position.x + 1, transform.position.y, 0);
            yield return new WaitForSeconds(0.1f);
            swordAttack2 = Instantiate(swordAttackPrefab2);
            swordAttack2.transform.position = new Vector3(transform.position.x + 1, transform.position.y, 0);
            isAttack = false;
        }
        //왼쪽공격
        else if(h < 0)
        {
            GameObject swordAttack1;
            GameObject swordAttack2;
            swordAttack1 = Instantiate(swordAttackPrefab1);
            swordAttack1.transform.position = new Vector3(transform.position.x - 1, transform.position.y, 0);
            swordAttack1.transform.localScale = new Vector3(-1, 1, 1);
            yield return new WaitForSeconds(0.1f);
            swordAttack2 = Instantiate(swordAttackPrefab2);
            swordAttack2.transform.position = new Vector3(transform.position.x - 1, transform.position.y, 0);
            swordAttack2.transform.localScale = new Vector3(-1, 1, 1);
            isAttack = false;
        } 
    }

    //enemy 나 trap 닿으면 죽기
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isHead && (collision.collider.CompareTag("Trap") || collision.collider.CompareTag("Enemy")))
        {
            DeathAnimator();
        }
    }
}
