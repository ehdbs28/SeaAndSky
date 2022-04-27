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
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform player = null;
    private SpriteRenderer _spriteRenderer;

    private Vector3 dir = Vector3.right;
    private EnemyState _state = EnemyState.Idle;
    private float speed = 1f;
    private float maxDistance = 3f;
    private float delay = 2;

    private bool isAware = false;

    void Start()
    {
        _spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        Invoke("NextMove", delay);
    }

    void Update()
    {
        EnemyDir();
        EnemyMove();
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
