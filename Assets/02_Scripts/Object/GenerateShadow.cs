using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateShadow : MonoBehaviour
{
    private GameObject shadow;
    private SpriteRenderer shadowRenderer;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private bool isCollide = true;
    [SerializeField] private bool isMoved = false;

    void Start()
    {
        shadow = new GameObject($"{name}Shadow");
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        shadowRenderer = shadow.AddComponent<SpriteRenderer>();

        Vector3 scale = transform.localScale;
        scale.y *= -1;
        shadow.transform.localScale = scale;

        shadow.transform.eulerAngles = -spriteRenderer.transform.eulerAngles;

        shadowRenderer.color = new Color(0, 0, 0, 0.4f);

        if (isCollide)
        {
            SettingCollider();
        }
        if (isMoved)
        {
            SettingRigidbody();
        }
    }

    void Update()
    {
        shadow.transform.position = new Vector2(transform.position.x, -transform.position.y);

        if (!spriteRenderer.sprite && !shadowRenderer.sprite) return;
        shadowRenderer.sprite = spriteRenderer.sprite;

        Vector3 curScale = transform.localScale;
        curScale.y *= -1f;
        shadow.transform.localScale = curScale;
    }

    private void SettingCollider()
    {
        BoxCollider2D col = shadow.AddComponent<BoxCollider2D>();
        BoxCollider2D myCol = GetComponentInChildren<BoxCollider2D>();

        col.size = myCol.size;
        col.offset = myCol.offset;
    }

    private void SettingRigidbody()
    {
        Rigidbody2D rigid = shadow.AddComponent<Rigidbody2D>();
        rigid.gravityScale = 0f;
    }
}
