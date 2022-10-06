using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour, IDamage
{
    [Header("Movement")]
    [SerializeField] private float _speed;
    [SerializeField] private float _slidingSpeed;
    [SerializeField] private float _attackReboundPower;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _wallJumpPower;
    [SerializeField] private int _jumpCount = 1;

    [Header("AttackEffect")]
    [SerializeField] private ParticleSystem _attackParticle;
    [SerializeField] private GameObject _attackEffect;

    [Header("LayerMask")]
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _wallRunLayer;

    [Header("UnityEvent")]
    public UnityEvent<Vector2> OnPlayerMove;
    [SerializeField] private UnityEvent OnPlayerJump;
    [SerializeField] private UnityEvent OnPlayerAttack;

    [Header("Sprite")]
    [SerializeField] private Sprite _cheakPointImage;

    private bool _isAttack = false;
    private bool _isJump = false;
    private bool _isGround = false;
    private bool _isWall = false;
    private bool _isWallJump = false;

    private float _rayDistance = 0.1f;
    private float _wallCheckDistance = 0.15f;

    private Vector2 _cheakPointTrm = new Vector2(-89.32f, 14.9f);

    private CapsuleCollider2D _collider;
    private Animator _anim;
    private Transform _visualObject;
    private Rigidbody2D _rigid;

    //Property
    public Transform VisualObj {get => _visualObject; set => _visualObject = value;}
    public float JumpPower {get => _jumpPower; set => _jumpPower = value;}
    public int JumpCount {get => _jumpCount; set => _jumpCount = value;}
    public float Speed {get => _speed; set => _speed = value;}
    public bool IsGorund {get => _isGround; set => _isGround = value;}

    private void Awake() {
        EventManager.StartListening("LoadStage", SetFirstPosition); 

        _rigid = GetComponent<Rigidbody2D>();
       _collider = GetComponent<CapsuleCollider2D>();
       _visualObject = transform.Find("VisualSprite");
       _anim = _visualObject.GetComponent<Animator>();   
    }

    private void Update()
    {
        if(!GameManager.Instance.IsPlayerDeath){
            Move();
            Jump();
            Attack();
        }
    }

    private void Move(){
        if(!_isWallJump){
            float h = 0;
            if(Input.GetKey(KeySetting.keys[Key.right])) h = 1;
            if(Input.GetKey(KeySetting.keys[Key.left])) h = -1;

            _anim.SetBool("IsMove", (h != 0)); 
            if(h != 0 && !_isWall) PlayerFlip(h, _visualObject.localScale.y);

            _rigid.velocity = new Vector2(h * _speed, _rigid.velocity.y);
            OnPlayerMove.Invoke(_rigid.velocity);
        }
    }

    private void Jump(){
        Bounds bounds = _collider.bounds;
        _isGround = Physics2D.CapsuleCast(transform.position, bounds.size, CapsuleDirection2D.Vertical, 0, Vector2.down * _visualObject.localScale.y, _rayDistance, _groundLayer);
        _isWall = Physics2D.BoxCast(transform.position, bounds.size, 0, Vector2.right * _visualObject.localScale.x, _wallCheckDistance, _wallRunLayer);

        if(_isGround) _jumpCount = 1;
        if(_isWall && !_isWallJump){
            _rigid.velocity = new Vector2(_rigid.velocity.x, _rigid.velocity.y * _slidingSpeed);
        }

        if(Input.GetKeyDown(KeySetting.keys[Key.jump])){
            if(!_isWall && _jumpCount > 0){

                 _anim.SetTrigger("IsJump");
                OnPlayerJump.Invoke();
                _rigid.velocity = Vector2.zero;

                _rigid.velocity = (_visualObject.up * _visualObject.localScale.y) * _jumpPower;
            }

            if(_isWall && !_isWallJump){
                _isWallJump = true;
                Invoke("WallJumpToggle", 0.15f);
                _anim.SetTrigger("IsJump");
                OnPlayerJump.Invoke();
                _rigid.velocity = Vector2.zero;

                _rigid.velocity = new Vector2(-_visualObject.localScale.x * _wallJumpPower, 0.9f * _wallJumpPower);
                PlayerFlip(-_visualObject.localScale.x, _visualObject.localScale.y);
            }
            _jumpCount--;
        }
    }

    private void WallJumpToggle(){
        _isWallJump = false;
    }

    private void Attack(){
        if(Input.GetKeyDown(KeySetting.keys[Key.attack]) && !_isAttack){
            _isAttack = true;
            float position_x = transform.position.x + _visualObject.localScale.x;
            float position_y = transform.position.y;
            float rotation = (_visualObject.localScale.x > 0) ? 0f : -180f;
            bool isLookDown = false;

            if(Input.GetKey(KeyCode.UpArrow)){
                position_x = transform.position.x;
                position_y = transform.position.y + 1.5f;
                rotation = 90f;
            }
            else if(Input.GetKey(KeyCode.DownArrow)){
                position_x = transform.position.x;
                position_y = transform.position.y - 1.5f;
                rotation = -90f;
                isLookDown = true;
            }

            StartCoroutine(AttackCoroutine(new Vector2(position_x, position_y), rotation, isLookDown));
            _anim.SetTrigger("Attack");
        }
    }

    IEnumerator AttackCoroutine(Vector2 attackPos, float rotation, bool isLookDown = false){
        OnPlayerAttack.Invoke();

        Collider2D checkBox = Physics2D.OverlapBox(attackPos, new Vector2(1.3f, 1.3f), 0f, _enemyLayer);
        if(checkBox){
            Tilemap tilemap = checkBox.GetComponent<Tilemap>();
            IHittable ihittable = checkBox.GetComponent<IHittable>();
            Vector3 hitPos, hitNormal = transform.position - checkBox.transform.position;

            if(isLookDown){
                _rigid.velocity = Vector2.zero;
                _rigid.velocity = Vector2.up * _attackReboundPower;
            }

            if(ihittable != null) ihittable.GetHit();

            if(tilemap != null){
                tilemap.RefreshAllTiles();
                hitPos = tilemap.WorldToCell(attackPos);
            }
            else hitPos = new Vector3(checkBox.bounds.center.x, checkBox.bounds.max.y);

            _attackParticle.transform.SetPositionAndRotation(hitPos, Quaternion.AngleAxis(hitNormal.z, Vector3.forward));
            _attackParticle.Play();
        }

        _attackEffect.transform.SetPositionAndRotation(attackPos, Quaternion.AngleAxis(rotation, Vector3.forward));
        _attackEffect.SetActive(true);

        yield return new WaitUntil(() => _anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack"));
        yield return new WaitUntil(() => _anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.75f);

        _attackEffect.SetActive(false);
        _isAttack = false;
    }

    public void EndDeadAnim(){
        gameObject.SetActive(false);
    }

    public void PlayerFlip(float x_Value, float y_Value){
        _visualObject.localScale = new Vector3(x_Value, y_Value, 1f);
    }

    private void SetFirstPosition(){
        transform.position = GameManager.Instance.PlayerPosition;
    }

    public void Damage(){
        if (GameManager.Instance.IsPlayerDeath) return;

        GameManager.Instance.ReduceHeart(transform, _cheakPointTrm, () => { _anim.SetTrigger("Dead"); });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheakPoint"))
        {
            _cheakPointTrm = collision.transform.position;
            collision.GetComponent<SpriteRenderer>().sprite = _cheakPointImage;
        }
    }

    private void OnDestroy(){
        EventManager.StopListening("LoadStage", SetFirstPosition);
    }
}
