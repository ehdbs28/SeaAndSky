using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FireColumn : MonoBehaviour
{
    private WaitForSeconds delay = new WaitForSeconds(3f);
    private readonly int fireHash = Animator.StringToHash("Fire");

    private Animator animator;

    new private SpriteRenderer renderer;
    new private Collider2D collider;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        renderer = GetComponentInChildren<SpriteRenderer>();
        collider = GetComponent<Collider2D>();

        renderer.sprite = null;
        StartCoroutine(ActiveColumn());
    }

    private IEnumerator ActiveColumn()
    {
        while (true)
        {
            renderer.enabled = true;
            collider.enabled = true;
            animator.SetTrigger(fireHash);
            yield return delay;

            renderer.enabled = false;
            collider.enabled = false;
            yield return delay;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.IsPlayerDeath = true;
        }
    }
}