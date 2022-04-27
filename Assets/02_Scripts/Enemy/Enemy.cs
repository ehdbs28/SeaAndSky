using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject player = null;

    private SpriteRenderer _spriteRenderer;

    private Vector3 dir = Vector3.right;

    private float speed = 1f;
    private float maxDistance = 3f;
    private float delay = 2f;

    private bool isAware = false;

    void Start()
    {
        _spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        EnemyDir();
        EnemyMove();
    }

    private void EnemyDir()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, maxDistance, playerLayer);
        Debug.DrawRay(transform.position, transform.right * maxDistance, Color.red);
        if (hit)
        {
            StopAllCoroutines();
            _spriteRenderer.color = Color.red;
            dir.x = Mathf.Abs(player.transform.position.x - transform.position.x);
            dir.Normalize();
        }
        else
        {
            if (!isAware)
            {
                _spriteRenderer.color = Color.white;
                StartCoroutine(NextMove());  //땅 없으면 멈추기        //움직이면서 점프 벽
                dir.Normalize();
            }
        }
    }

    private void EnemyMove()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    IEnumerator NextMove()
    {
        isAware = true;
        yield return new WaitForSeconds(delay);
        dir.x *= -1;
        transform.rotation = dir.x == -1 ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
        isAware = false;
    }
}
