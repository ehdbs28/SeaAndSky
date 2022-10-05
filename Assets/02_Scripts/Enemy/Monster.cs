using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Monster : MonoBehaviour, IHittable
{
    private float maxDistance = 3f;
    public float _Maxhp;
    private float _hp;
    public float _deadTime;
    public float _speed;
    public Transform target;
    private SpriteRenderer _spriteRenderer = null;
    public SpriteRenderer _SpriteRenderer { get => _spriteRenderer; }
    private Animator _anim = null;
    private bool isDead = false;
    public LayerMask moveableLayer;
    [SerializeField] private AIState _currentState;

    [field: SerializeField] public UnityEvent OnDie { get; set; }
    [field: SerializeField] public UnityEvent OnGetHit { get; set; }
    private UnityAction OnMonsterChangedDir;
    private Vector2 _monsterDir = Vector2.right;

    public Vector2 MonsterDir
    {
        get => _monsterDir;
        set
        {
            _monsterDir = value;
            _monsterDir.Normalize();
            OnMonsterChangedDir?.Invoke();
        }
    }
    private void Awake()
    {
        target = GameObject.Find("Player").transform;
        _anim = transform.Find("Sprite").GetComponent<Animator>();
        _spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        OnMonsterChangedDir += ChangedVelocity;
                _hp = _Maxhp;
    }
    public void ChangedVelocity()
    {
        if(MonsterDir.x == 1)
        {
            transform.localScale = Vector2.one;
        }
        else
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }
    public void ChangeState(AIState state) 
    {
        _currentState = state;
    }
    private void Update()
    {
        if(target == null)
        {
            return;
        }
        else
        {
            _currentState.UpdateState();
        }
    }
    public bool CheckFrontGround()
    {
        Vector2 nextPos = new Vector2(transform.position.x + MonsterDir.x * 0.4f, transform.position.y - 0.5f);
     
        RaycastHit2D hit2D = Physics2D.Raycast(nextPos, transform.up * -1f, maxDistance, moveableLayer);
        if (hit2D)
        {
            return true;
        }
        return false;
    }
    public bool CheckFrontWall()
    {
        Vector2 nextPos = new Vector2(transform.position.x + MonsterDir.x, transform.position.y);
        Debug.DrawRay(transform.position, transform.right * MonsterDir * 0.3f, Color.yellow);
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, transform.right * MonsterDir, 0.3f, moveableLayer);
        if (hit2D)
        {
            return true;
        }
        return false;
    }

    public void ReSpawn(Vector2 position)
    {
        transform.position = position;
        _hp = _Maxhp;
    }

    public void GetHitAnim(){
        _anim.SetTrigger("OnHit");
    }

    public void GetHit()
    {
        _hp--;
        OnGetHit?.Invoke();
        if (_hp <= 0)
        {
            isDead = true;
            OnDie?.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            IDamage iDamage = collision.transform.GetComponent<IDamage>();
            iDamage?.Damage();
        }
    }
}
