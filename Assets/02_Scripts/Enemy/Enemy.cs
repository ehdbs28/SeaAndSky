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
    private SpriteRenderer _spriteRenderer;

    private Vector3 dir = Vector3.right;
    private EnemyState _state = EnemyState.Idle;
    private float speed = 1f;
    private float maxDistance = 3f;
    private float delay = 2;
    private BoxCollider2D boxCollider2D;
    private Vector3 sidePosition;
    private Rigidbody2D rigid;

    private bool isColliderWall = false;
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
        sidePosition = new Vector2(bounds.max.x, bounds.center.y);
        isColliderWall = Physics2D.OverlapCircle(sidePosition, 0.1f, wallLayer);

        if (isColliderWall && !isJump)
        {
            rigid.velocity = Vector2.zero;
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    private void EnemyMove()
    {
        transform.position += dir * speed * Time.deltaTime;
    }
    private void StateChanged(EnemyState newState)
    {
        _state = newState;
        if(_state == EnemyState.Idle)
        {
            delay = Random.Range(1.5f, 3f);
            Invoke("NextMove", delay);
        }
    }
    private void NextMove()
    {
        delay = Random.Range(1.5f, 3f);
        Debug.Log(1);
        dir.x *= -1;
        transform.localScale = dir.x == -1 ? new Vector2(-1, 1) : new Vector2(1, 1);
        //transform.rotation = dir.x == -1 ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
        Invoke("NextMove", delay);
    }
}
