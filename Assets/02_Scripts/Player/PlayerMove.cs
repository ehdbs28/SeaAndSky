using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMove : MonoBehaviour
{
    public static int doubleJumpCount = 0;

    private float _localScaleY = 1;
    public float LocalScaleY
    {
        set => _localScaleY = value;
        get => _localScaleY;
    }
    private float _speed;
    public float Speed
    {
        get => _speed;
        set
        {
            if (value < 0)
                value = 0;
            _speed = value;
        }
    }
    [SerializeField] private float _jumpPower;
    public float JumpPower
    {
        get => _jumpPower;
        set
        {
            if (value < 0)
                value = 0;
            _jumpPower = value;
        }
    }

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject swordAttackPrefab;
    [SerializeField] private static float h;
    [SerializeField] private LayerMask enemyLayer;

    private bool isGround = false;
    public bool IsGround
    {
        get => isGround;
    }
    private bool isAttack = false;
    private bool isHead = false;

    public  bool isLeft = false;

    private Vector3 footPosition;
    private CapsuleCollider2D capsuleCollider2D;
    private Animator anim = null;
    private Rigidbody2D rigid;

    [SerializeField] private UnityEvent<Vector2> onPlayerMove;
    [SerializeField] private UnityEvent onPlayerJump;
    [SerializeField] private UnityEvent onPlayerAttack;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        _speed = 5f;
    }


    void Update()
    {
        if (!GameManager.Instance.IsPlayerDeath)
        {
            Move();
            PlayerAttack();

            if(doubleJumpCount > 0)
            {
                DoubleJumpItem();
                return;
            }
            Jump();
        }
        else
        {
            Debug.Log("Death");
            anim.SetTrigger("Dead");
        }
    }

    private void DoubleJumpItem()
    {
        if (doubleJumpCount > 0 && (Input.GetKey(KeyCode.X)))
        {
            anim.SetBool("isJump", true);
            rigid.velocity = Vector2.zero;
            rigid.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
            doubleJumpCount -= 1;
        }
    }

    //점프
    private void Jump()
    {
        if ((Input.GetKey(KeyCode.X) && isGround))
        {
            anim.SetBool("isJump", true);
            onPlayerJump.Invoke();

            rigid.velocity = Vector2.zero;
            rigid.AddForce(transform.up * _jumpPower, ForceMode2D.Impulse);
            
            if(_localScaleY == -1)
            {
                rigid.velocity = Vector2.zero;
                rigid.AddForce(transform.up * _jumpPower * -1, ForceMode2D.Impulse);
            }
        }
    }

    //움직이기
    private void Move()
    {
        Bounds bounds = capsuleCollider2D.bounds;
        if(_localScaleY == 1)
        {
            footPosition = new Vector2(bounds.center.x, bounds.min.y);
        }
        else if(_localScaleY == -1)
        {
            footPosition = new Vector3(bounds.center.x, bounds.max.y);
        }
        
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
            transform.localScale = new Vector3(-1, _localScaleY, 1);
        else
            transform.localScale = new Vector3(1, _localScaleY, 1);

        Vector2 direction = new Vector2(h, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        onPlayerMove.Invoke(rigid.velocity);
    }

    //공격실행
    private void PlayerAttack()
    {
        if (GameManager.Instance.IsPlayerDeath) return;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!isAttack)
            {
                onPlayerAttack.Invoke();
                StartCoroutine(Attack());
                anim.SetTrigger("Attack");
                isAttack = true;
            }
        }
    }

    //플레이어 공격
    IEnumerator Attack() 
    {
        if (!GameManager.Instance.IsPlayerDeath)
        {
            #region 플레이어공격
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
            else if (!isLeft)
            {
                GameObject swordAttack;

                swordAttack = Instantiate(swordAttackPrefab);
                swordAttack.transform.position = new Vector3(transform.position.x + 1, transform.position.y, 0);
                yield return new WaitForSeconds(0.1f);
                Destroy(swordAttack);
                isAttack = false;
            }
            //왼쪽공격
            else if (isLeft)
            {
                GameObject swordAttack;

                swordAttack = Instantiate(swordAttackPrefab);
                swordAttack.transform.position = new Vector3(transform.position.x - 1, transform.position.y, 0);
                swordAttack.transform.localScale = new Vector3(-1, 1, 1);
                yield return new WaitForSeconds(0.1f);
                Destroy(swordAttack);
                isAttack = false;
            }
#endregion
        }
    }

    //enemy 나 trap 닿으면 죽기
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isHead && (collision.collider.CompareTag("Trap") || collision.collider.CompareTag("Enemy")))
        {
            GameManager.Instance.ReduceHeart();
            //isDeath = true;
        }
    }
    public void EndDeadAnim() //애니메이션에 이벤트로 넣었음
    {
        gameObject.SetActive(false);
    }

    public void ChangePlayerState()
    {
        transform.localScale = new Vector3(-1, 1, 0);
    }
}
