using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HidePlat : MonoBehaviour
{
    [SerializeField] private float destroyTime = 3f;
    [SerializeField] private float spawnTime = 5f;
    [SerializeField] private float durationTime = 2f;
    [SerializeField] private GameObject plat;
    private SpriteRenderer sp;
    private BoxCollider2D col;
    private Vector3 dir;

    private void Start()
    {
        dir = plat.transform.position;
        sp = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            StartCoroutine(HideTime());
        }
    }

    IEnumerator HideTime()
    { 
        yield return new WaitForSeconds(destroyTime);

        plat.transform.DOMoveY(transform.position.y - 3, durationTime);
        sp.DOFade(0, durationTime);
        col.enabled = false;
        yield return new WaitForSeconds(spawnTime);

        col.enabled = true;
        plat.transform.position = dir;
        sp.color = new Color(1, 1, 1, 1);
    }
}
