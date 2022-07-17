using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FireColumn : MonoBehaviour
{
    private WaitForSeconds fireDelay = new WaitForSeconds(1f);
    private WaitForSeconds animationDelay = new WaitForSeconds(0.6f);
    private WaitForSeconds playDelay = new WaitForSeconds(2f);
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
            animator.SetTrigger(fireHash);

            yield return animationDelay;
            collider.enabled = true;

            yield return fireDelay;
            collider.enabled = false;
            renderer.enabled = false;

            yield return animationDelay;
            yield return playDelay;
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