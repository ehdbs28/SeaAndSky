using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectFish : MonoBehaviour
{
    private UIManager ui;
    public GameObject player;

    private void Start()
    {
        ui = FindObjectOfType<UIManager>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, 1.5f, LayerMask.GetMask("Player"));
        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, 1.5f, LayerMask.GetMask("Player"));

        if (hit || hit2)
        {
            transform.DOMove(player.transform.position, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ui.collectCount++;
            Destroy(gameObject);
        }
    }
}
