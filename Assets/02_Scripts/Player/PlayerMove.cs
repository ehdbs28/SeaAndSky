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
    [SerializeField] private ParticleSystem _attackParticle;
    [SerializeField] private GameObject _attackEffect;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallRunLayer;
    private float _rayDistance = 0.1f;

    [SerializeField] private MovementDataSO movementData;
    [SerializeField] private static float h;
    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] private static float r;
    [SerializeField] private static float l;

    [field:SerializeField] public bool GroundCheak {get; set;} = false;
    [field:SerializeField] public bool LeftCheak {get; set;} = false;
    [field:SerializeField] public bool RightCheak {get; set;} = false;

    private bool isAttack = false;

    public bool isLeft = false;

    new private BoxCollider2D collider;
    private Animator anim = null;
    private Transform visualObject;
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
        visualObject = transform.Find("VisualSprite");
        anim = visualObject.GetComponent<Animator>();
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
            Cheak();
            Move();
            PlayerAttack();
            Jump();
        }
    }

    private void Cheak(){
        Bounds bounds = collider.bounds;

        RaycastHit2D groundHit = Physics2D.CapsuleCast(bounds.center, bounds.size, CapsuleDirection2D.Vertical, 0,
                                                         (GameManager.Instance.PlayerState == AreaState.Sky) ? Vector2.down : Vector2.up,
                                                         _rayDistance, groundLayer);
        RaycastHit2D leftHit = Physics2D.BoxCast(bounds.center, bounds.size, 0, Vector2.left, _rayDistance, groundLayer);
        RaycastHit2D rightHit = Physics2D.BoxCast(bounds.center, bounds.size, 0, Vector2.right, _rayDistance, groundLayer);


        GroundCheak = (groundHit) ? true : false;
        LeftCheak = (leftHit) ? true : false;
        RightCheak = (rightHit) ? true : false;
    }

    public void Damage()
    {
        if (GameManager.Instance.IsPlayerDeath) return;

        GameManager.Instance.ReduceHeart(transform, _cheakPointTrm, () => { anim.SetTrigger("Dead"); });
    }

    private void Jump()
    {
        if(GroundCheak) JumpCount = 1;
        if ((Input.GetKeyDown(KeySetting.keys[Key.jump]) && JumpCount > 0))
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
        PlayerInput(out h);

        anim.SetBool("isMove", (h != 0));
        
        if (h < 0)
            isLeft = true;
        if (h > 0)
            isLeft = false;

        visualObject.localScale = new Vector3((isLeft) ? -1 : 1, _localScaleY, 1);

        Mathf.Clamp(rigid.velocity.x, 0, _maxSpeed);
        rigid.velocity = new Vector3(h * _speed, rigid.velocity.y, 0);
        onPlayerMove.Invoke(rigid.velocity);
    }

    private void PlayerInput(out float h){
        if (Input.GetKey(KeySetting.keys[Key.right]))
            h = 1;
        else if(Input.GetKey(KeySetting.keys[Key.left]))
            h = -1;
        else
            h = 0;
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
            onPlayerAttack.Invoke(); //사운드
            float attackPosX = (!(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)))
                                 ? (isLeft) ? transform.position.x - 1f : transform.position.x + 1f : transform.position.x;
            float attackPosY = (Input.GetKey(KeyCode.UpArrow)) ? transform.position.y + 1.5f : (Input.GetKey(KeyCode.DownArrow))
                                 ? transform.position.y - 1.5f : transform.position.y;
            Vector3 attackPos = new Vector3(attackPosX, attackPosY);

            float attackRotate = (Input.GetKey(KeyCode.UpArrow)) ? 90f : (Input.GetKey(KeyCode.DownArrow)) ? -90f
                                : (isLeft) ? -180 : 0;

            Collider2D collider = Physics2D.OverlapBox(attackPos, new Vector2(1.3f, 1.3f), 0f, enemyLayer); 
            if(collider){
                IHittable hittable = collider.GetComponent<IHittable>();
                Vector3 hitPos;
                Vector3 hitNormal;

                if(Input.GetKey(KeyCode.DownArrow)){
                    rigid.velocity = Vector2.zero;
                    rigid.velocity = Vector2.up * _attackReboundPower;
                }
                if(hittable != null){
                    hittable.GetHit();
                }

                GameManager.Instance.timeManager.TimeManaging(0.025f);
                Tilemap tilemap = collider.GetComponent<Tilemap>();

                if(tilemap != null){
                    tilemap.RefreshAllTiles();

                    hitPos = tilemap.WorldToCell(attackPos);
                }
                else hitPos = new Vector3(collider.bounds.center.x, collider.bounds.max.y);

                hitNormal = transform.position - collider.transform.position;
                
                _attackParticle.transform.SetPositionAndRotation(hitPos, Quaternion.AngleAxis(hitNormal.z, Vector3.forward));
                _attackParticle.Play();
            }

            _attackEffect.SetActive(true);
            _attackEffect.transform.position = attackPos;
            _attackEffect.transform.rotation = Quaternion.AngleAxis(attackRotate, Vector3.forward);
           
            yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack"));
            yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.75f);

            _attackEffect.SetActive(false);
            if(_attackParticle.isPlaying) _attackParticle.Stop();
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