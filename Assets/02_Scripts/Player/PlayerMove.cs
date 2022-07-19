using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMove : MonoBehaviour, IDamage
{
    public static int doubleJumpCount = 0;
    private float _currentVelocity = 3f;
    private float _localScaleY = 1;
    public float LocalScaleY
    {
        set => _localScaleY = value;
        get => _localScaleY;
    }
    private Vector2 direction = Vector2.zero;
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
    private float _maxSpeed = 5;
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

    [SerializeField] private MovementDataSO movementData;
    [SerializeField] private static float h;
    [SerializeField] private LayerMask enemyLayer;

    private bool isGround = false;
    public bool IsGround
    {
        get => isGround;
    }
    private bool isAttack = false;
    //private bool isHead = false;

    public  bool isLeft = false;

    private Vector3 footPosition;
    private CapsuleCollider2D capsuleCollider2D;
    private Animator anim = null;
    private Rigidbody2D rigid;
    private Vector2 movementDirection;
    [SerializeField] private UnityEvent<Vector2> onPlayerMove;
    [SerializeField] private UnityEvent onPlayerJump;
    [SerializeField] private UnityEvent onPlayerAttack;

    private void Awake()
    {
        EventManager.StartListening("LoadStage", SetFirstPosition);
    }

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
    }

    public void Damege()
    {
        if (GameManager.Instance.IsPlayerDeath) return;
        Debug.Log("Death");
        anim.SetTrigger("Dead");
        GameManager.Instance.ReduceHeart();

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

    //����
    private void Jump()
    {
        if ((Input.GetKey(KeyCode.X) && isGround))
        {
            anim.SetBool("isJump", true);
            onPlayerJump.Invoke();

            rigid.velocity = Vector2.zero;
            rigid.AddForce(transform.up * _jumpPower, ForceMode2D.Impulse);
            //rigid.velocity = transform.up * _jumpPower;
            
            if(_localScaleY == -1)
            {
                rigid.velocity = Vector2.zero;
                rigid.AddForce(transform.up * _jumpPower * -1, ForceMode2D.Impulse);
            }
        }
    }

    //�����̱�
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

/*
        direction.x = h;
        if(direction.sqrMagnitude > 0)
        {
            if(Vector2.Dot(direction, movementDirection) < 0)
            {
                _currentVelocity = 0;
            }
            movementDirection = direction.normalized;
        }
        _currentVelocity = CalculateSpeed(direction);
        rigid.velocity = new Vector2(movementDirection.x * _currentVelocity, rigid.velocity.y);
        if (rigid.velocity.x > _maxSpeed)
        {
            rigid.velocity = new Vector2(_maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < _maxSpeed * -1)
        {
            rigid.velocity = new Vector2(_maxSpeed * -1, rigid.velocity.y);
        }
        Vector2 direction = new Vector2(h, 0);
        */
        rigid.position += (direction * _speed * Time.deltaTime);
        onPlayerMove.Invoke(rigid.velocity);
    }
    private float CalculateSpeed(Vector2 movementInput)
    {
        if(movementInput.sqrMagnitude > 0)
        {
            _currentVelocity += movementData.acceleration * Time.deltaTime;
        }
        else
        {
            _currentVelocity -= movementData.deAcceleration * Time.deltaTime;
        }

        return Mathf.Clamp(_currentVelocity, 0, _maxSpeed);
    }
    //���ݽ���
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

    //�÷��̾� ����
    IEnumerator Attack() 
    {
        if (!GameManager.Instance.IsPlayerDeath)
        {
            #region �÷��̾����
            //���ʰ���
            if (Input.GetKey(KeyCode.UpArrow))
            {
                GameObject swordAttack;

                swordAttack = Instantiate(swordAttackPrefab);
                swordAttack.transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.Euler(0, 0, 90));
                yield return new WaitForSeconds(0.1f);
                Destroy(swordAttack);
                isAttack = false;
            }

            //�Ʒ�����
            else if (Input.GetKey(KeyCode.DownArrow) && !isGround)
            {
                GameObject swordAttack;

                swordAttack = Instantiate(swordAttackPrefab);
                swordAttack.transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y - 1, 0), Quaternion.Euler(0, 0, -90));
                yield return new WaitForSeconds(0.1f);
                Destroy(swordAttack);
                isAttack = false;
            }
            //�����ʰ���
            else if (!isLeft)
            {
                GameObject swordAttack;

                swordAttack = Instantiate(swordAttackPrefab);
                swordAttack.transform.position = new Vector3(transform.position.x + 1, transform.position.y, 0);
                yield return new WaitForSeconds(0.1f);
                Destroy(swordAttack);
                isAttack = false;
            }
            //���ʰ���
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

    public void EndDeadAnim() //�ִϸ��̼ǿ� �̺�Ʈ�� �־���
    {
        gameObject.SetActive(false);
    }

    public void ChangePlayerState()
    {
        transform.localScale = new Vector3(-1, 1, 0);
    }
    private void SetFirstPosition()
    {
        transform.position = GameManager.Instance.PlayerPosition;
    }

    private void OnDestroy()
    {
        EventManager.StopListening("LoadStage", SetFirstPosition);
    }
}

