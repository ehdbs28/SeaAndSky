using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class PlayerMove : MonoBehaviour, IDamage
{
    public static int JumpCount = 1;
    private float _currentVelocity = 3f;
    private float _localScaleY = 1;
    public float LocalScaleY
    {
        set => _localScaleY = value;
        get => _localScaleY;
    }
    private Vector2 direction = Vector2.zero;
    [SerializeField]private float _speed;
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
    private float _maxSpeed = 20;
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
    [SerializeField] private float _attackReboundPower;
    [SerializeField] private GameObject _attackParticle;
    [SerializeField] private GameObject _attackEffect;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private MovementDataSO movementData;
    [SerializeField] private static float h;
    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] private static float r;
    [SerializeField] private static float l;

    private bool isGround = false;
    public bool IsGround
    {
        get => isGround;
    }
    private bool isAttack = false;
    //private bool isHead = false;

    public bool isLeft = false;

    private Vector3 footPosition;
    new private BoxCollider2D collider;
    private Animator anim = null;
    private Rigidbody2D rigid;
    private Vector2 movementDirection;
    public UnityEvent<Vector2> onPlayerMove;
    [SerializeField] private UnityEvent onPlayerJump;
    [SerializeField] private UnityEvent onPlayerAttack;

    private Vector2 _cheakPointTrm = new Vector2(-89.32f, 14.9f);
    [SerializeField] Sprite _cheakPointImg;
   
    private void Awake()
    {
        EventManager.StartListening("LoadStage", SetFirstPosition);
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        anim = transform.Find("VisualSprite").GetComponent<Animator>();
        _maxSpeed = movementData.maxSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheakPoint"))
        {
            _cheakPointTrm = collision.transform.position;
            collision.GetComponent<SpriteRenderer>().sprite = _cheakPointImg;
        }
    }

    void Update()
    {
        if (!GameManager.Instance.IsPlayerDeath)
        {
            Move();
            PlayerAttack();
            Jump();
        }
    }

    public void Damage()
    {
        if (GameManager.Instance.IsPlayerDeath) return;

        GameManager.Instance.ReduceHeart(transform, _cheakPointTrm, () => { anim.SetTrigger("Dead"); });
    }

    private void Jump()
    {
        if(isGround) JumpCount = 1;
        if ((Input.GetKeyDown(KeySetting.keys[Key.jump]) && isGround && JumpCount > 0))
        {
            anim.SetTrigger("IsJump");
            onPlayerJump.Invoke();

            rigid.velocity = Vector2.zero;

            if(_localScaleY == 1)
                rigid.AddForce(transform.up * _jumpPower, ForceMode2D.Impulse);
            else if(_localScaleY == -1)
                rigid.AddForce(transform.up * _jumpPower * -1, ForceMode2D.Impulse);

            JumpCount--;
        }
    }

    private void Move()
    {
        Bounds bounds = collider.bounds;
        footPosition = new Vector2(bounds.center.x, (_localScaleY == 1) ? bounds.min.y : bounds.max.y);
        isGround = Physics2D.OverlapCircle(footPosition, 0.1f, groundLayer);

        PlayerInput(out h);

        anim.SetBool("isMove", (h != 0));
        
        if (h < 0)
            isLeft = true;
        if (h > 0)
            isLeft = false;

        transform.localScale = new Vector3((isLeft) ? -1 : 1, _localScaleY, 1);

        Mathf.Clamp(rigid.velocity.x, 0, _maxSpeed);
        rigid.velocity = new Vector3(h * _speed, rigid.velocity.y, 0);
        onPlayerMove.Invoke(rigid.velocity);
    }

    private void PlayerInput(out float h){
        int temp = 0;

        if (Input.GetKey(KeySetting.keys[Key.right]))
            temp = 1;
        else if(Input.GetKey(KeySetting.keys[Key.left]))
            temp = -1;

        h = temp;
    }

    private float CalculateSpeed(Vector2 movementInput)
    {
        if (movementInput.sqrMagnitude > 0)
        {
            _currentVelocity += movementData.acceleration * Time.deltaTime;
        }
        else
        {
            _currentVelocity -= movementData.deAcceleration * Time.deltaTime;
        }

        return Mathf.Clamp(_currentVelocity, 0, _maxSpeed);
    }

    private void PlayerAttack()
    {
        if (GameManager.Instance.IsPlayerDeath) return;

        if(Input.GetKeyDown(KeySetting.keys[Key.attack]))
        {
            if (!isAttack)
            {
                isAttack = true;
                StartCoroutine(Attack());
                anim.SetTrigger("Attack");
            }
        }
    }

    IEnumerator Attack() 
    {
        if (!GameManager.Instance.IsPlayerDeath)
        {
            ParticleSystem particle = null;
            onPlayerAttack.Invoke(); //사운드
            float attackPosX = (isGround) ? (isLeft) ? transform.position.x - 1f : transform.position.x + 1f : transform.position.x;
            float attackPosY = (Input.GetKey(KeyCode.UpArrow)) ? transform.position.y + 1.5f : (Input.GetKey(KeyCode.DownArrow)) ? transform.position.y - 1.5f : transform.position.y;
            Vector3 attackPos = new Vector3(attackPosX, attackPosY);
            float attackRotate = (Input.GetKey(KeyCode.UpArrow)) ? 90f : (Input.GetKey(KeyCode.DownArrow)) ? -90f : 0f;

            Collider2D collider = Physics2D.OverlapBox(attackPos, new Vector2(1.3f, 1.3f), 0f, enemyLayer); 
            if(collider){
                IHittable hittable = collider.GetComponent<IHittable>();

                if(Input.GetKey(KeyCode.DownArrow)){
                    rigid.velocity = Vector2.zero;
                    rigid.velocity = Vector2.up * _attackReboundPower;
                }
                if(hittable != null){
                    hittable.GetHit();
                }

                GameManager.Instance.timeManager.TimeManaging(0.025f);
                
                Vector3 hitPos;
                if(collider.GetComponent<Tilemap>() != null){
                    Tilemap tilemap = collider.GetComponent<Tilemap>();

                    tilemap.RefreshAllTiles();

                    float x = tilemap.WorldToCell(attackPos).x;
                    float y = tilemap.WorldToCell(attackPos).y;

                    hitPos = new Vector3(x, y);
                }
                else hitPos = new Vector3(collider.bounds.center.x, collider.bounds.max.y);

                Vector3 hitNormal = transform.position - collider.transform.position;
                particle = GameObject.Instantiate(_attackParticle, hitPos, Quaternion.Euler(hitNormal.x, hitNormal.y, hitNormal.z)).GetComponent<ParticleSystem>();
                particle.Play();
            }

            _attackEffect.SetActive(true);
            _attackEffect.transform.position = attackPos;
            _attackEffect.transform.rotation = Quaternion.AngleAxis(attackRotate, Vector3.forward);
           
            yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack"));
            yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.75f);

            _attackEffect.SetActive(false);
            if(particle != null) Destroy(particle.gameObject);
            isAttack = false;
        }
    }

    public void EndDeadAnim() 
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