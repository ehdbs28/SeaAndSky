using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum EnemyState
    {
        Idle,
        Chase
    }

    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform player = null;
    [SerializeField] private float jumpPower = 5;
    [SerializeField] private Vector2 _flatPos;

    private SpriteRenderer _spriteRenderer;
    private Vector3 dir = Vector3.right;
    private EnemyState _state = EnemyState.Idle;
    private float speed = 1f;
    private float maxDistance = 3f;
    private float delay;
    private BoxCollider2D boxCollider2D;
    private Vector3 rightPosition;
    private Vector3 leftPosition;
    private Rigidbody2D rigid;

    private bool isColliderRightWall = false;
    private bool isColliderLeftWall = false;
    private bool isJump = false;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        _spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        Invoke("NextMove", delay);
    }

    void Update()
    {
        EnemyDir();
        EnemyMove();
        ColiderWall();
    }

    private void EnemyDir()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right * dir.x, maxDistance, playerLayer);
        Debug.DrawRay(transform.position, transform.right * dir.x * maxDistance, Color.red);
        if (hit)
        {
            StateChanged(EnemyState.Chase);
            CancelInvoke();
            _spriteRenderer.color = Color.red;
            dir.x = Mathf.Abs(player.position.x - transform.position.x);
            dir.Normalize();
        }
        else if(_state != EnemyState.Idle)
        {
            _spriteRenderer.color = Color.white;
            StateChanged(EnemyState.Idle);
        }
    }

    private void ColiderWall()
    {
        Bounds bounds = boxCollider2D.bounds;
        rightPosition = new Vector2(bounds.max.x, bounds.center.y);
        isColliderRightWall = Physics2D.OverlapCircle(rightPosition, 0.1f, wallLayer);

        Bounds _bounds = boxCollider2D.bounds;
        leftPosition = new Vector2(bounds.min.x, bounds.center.y);
        isColliderLeftWall = Physics2D.OverlapCircle(leftPosition, 0.1f, wallLayer);

        if (isColliderRightWall || isColliderLeftWall)
        {
            isJump = true;
            EnemyJump();
        }
    }

    private void EnemyJump()
    {
        rigid.velocity = Vector2.zero;
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        CancelInvoke();
        Invoke("NextMove", delay);
        isJump = false;
    }

    private void EnemyMove()
    {

        RaycastHit2D _hit = Physics2D.Raycast(new Vector2(transform.position.x + dir.x * 0.1f, transform.position.y), transform.up * -1, maxDistance, wallLayer); 
        Debug.DrawRay(new Vector2(transform.position.x + dir.x * 0.5f, transform.position.y), transform.up * -1 *maxDistance, Color.green);

        if (_hit)
        {
            transform.position += dir * speed * Time.deltaTime;
        }

    }

    private void StateChanged(EnemyState newState)
    {
        _state = newState;
        if(_state == EnemyState.Idle && !isJump)
        {
            delay = Random.Range(4f, 5f);
            Invoke("NextMove", delay);
        }
    }
    private void NextMove()
    {
        delay = Random.Range(4f, 6f);
        dir.x *= -1;
        transform.localScale = dir.x == -1 ? new Vector2(-1, 1) : new Vector2(1, 1);
        //transform.rotation = dir.x == -1 ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
        Invoke("NextMove", delay);
    }
}
