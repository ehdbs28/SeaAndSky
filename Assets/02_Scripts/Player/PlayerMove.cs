using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMove : MonoBehaviour
{
    
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject swordAttackPrefab;
    [SerializeField] private static float h;
    [SerializeField] private LayerMask enemyLayer;

    private bool isGround = false;
    private bool isAttack = false;
    private bool isDeath = false;
    public static bool isLeft = false;

    private Vector3 footPosition;
    private CapsuleCollider2D capsuleCollider2D;
    private Animator anim = null;
    private Rigidbody2D rigid;

    
    private bool isHead = false;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (!isDeath)
        {
            Move();
            Jump();
            PlayerAttack();
            MoveLimit();
        }
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
        if (isGround && Input.GetKey(KeyCode.X))
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
            isLeft = true;
        if (h > 0)
            isLeft = false;
        
        if(isLeft)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);

        transform.Translate(new Vector2(h, 0) * speed * Time.deltaTime);
    }


    //공격실행
    private void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.Z))
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
        //위쪽공격
        if (Input.GetKey(KeyCode.UpArrow))
        {
            GameObject swordAttack;

            swordAttack = Instantiate(swordAttackPrefab);
            swordAttack.transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.Euler(0, 0, 90));
            yield return new WaitForSeconds(0.1f);
            Destroy(swordAttack);
            isAttack = false;
        }

        //아래공격
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            GameObject swordAttack;
            
            swordAttack = Instantiate(swordAttackPrefab);
            swordAttack.transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y - 1, 0), Quaternion.Euler(0, 0, -90));
            yield return new WaitForSeconds(0.1f);
            Destroy(swordAttack);
            isAttack = false;
        }
        //오른쪽공격
        else if(!isLeft)
        {
            GameObject swordAttack;
            
            swordAttack = Instantiate(swordAttackPrefab);
            swordAttack.transform.position = new Vector3(transform.position.x + 1, transform.position.y, 0);
            yield return new WaitForSeconds(0.1f);
            Destroy(swordAttack);
            isAttack = false;
        }
        //왼쪽공격
        else if(isLeft)
        {
            GameObject swordAttack;
            
            swordAttack = Instantiate(swordAttackPrefab);
            swordAttack.transform.position = new Vector3(transform.position.x - 1, transform.position.y, 0);
            swordAttack.transform.localScale = new Vector3(-1, 1, 1);
            yield return new WaitForSeconds(0.1f);
            Destroy(swordAttack);
            isAttack = false;
        } 
    }

    //enemy 나 trap 닿으면 죽기
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isHead && (collision.collider.CompareTag("Trap") || collision.collider.CompareTag("Enemy")))
        {
            anim.Play("PlayerDie");
            isDeath = true;
        }


    }
}
