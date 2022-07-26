using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FireColumn : MonoBehaviour
{
    private WaitForSeconds animationDelay = new WaitForSeconds(0.6f);
    private WaitForSeconds playDelay = new WaitForSeconds(2f);
    private readonly int fireHash = Animator.StringToHash("Fire");

    private Animator animator;

    new private SpriteRenderer renderer;
    new private Collider2D collider;

    private float length;
    private readonly float interval = 0.2f;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        renderer = GetComponentInChildren<SpriteRenderer>();
        collider = GetComponent<Collider2D>();

        renderer.sprite = null;
        StartCoroutine(ActiveColumn());

        length = animator.runtimeAnimatorController.animationClips[0].length;
    }

    private IEnumerator ActiveColumn()
    {
        while (true)
        {
            collider.enabled = true;
            renderer.enabled = true;
            animator.SetTrigger(fireHash);

            yield return new WaitForSeconds(length - interval);

            collider.enabled = false;
            renderer.sprite = null;
            
            yield return new WaitForSeconds(interval);

            renderer.enabled = false;

            yield return animationDelay;
            yield return playDelay;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //GameManager.Instance.IsPlayerDeath = true;
            IDamage damage = collision.transform.GetComponent<IDamage>();
            damage?.Damege();
        }
    }
}