using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 dir = Vector3.right;
    public float speed = 1f;
    public LayerMask playerLayer;
    private float maxDistance = 3f;
    public GameObject player = null;
    private int nextMove = 1;

    void Start()
    {
        Invoke("NextMove", 3f);
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, maxDistance, playerLayer);
        Debug.DrawRay(transform.position, transform.right * maxDistance, Color.red);
        if (hit)
        {
            Debug.Log(hit.collider.name);
            dir.x = Mathf.Abs(player.transform.position.x - transform.position.x);
            transform.position += dir.normalized * speed * Time.deltaTime;
        }
    }

    void NextMove()
    {
        nextMove *= 1;
        Invoke("NextMove", 3f);
    }
}
