using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateShadow : MonoBehaviour
{
    private GameObject shadow;
    [SerializeField] private bool isCollide = true;
    [SerializeField] private bool isMoved = false;

    void Start()
    {
        shadow = new GameObject($"{name}Shadow");
        shadow.transform.localScale = transform.localScale;

        SettingRenderer();
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
    }

    private void SettingRenderer()
    {
        SpriteRenderer myRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer renderer = shadow.AddComponent<SpriteRenderer>();

        renderer.sprite = myRenderer.sprite;
        Color color = Color.black;
        color.a = 0.4f;

        renderer.color = color;
    }

    private void SettingCollider()
    {
        // LATER: 다른 콜라이더도 가능하도록 하게 해보자
        shadow.AddComponent<BoxCollider2D>();
    }

    private void SettingRigidbody()
    {
        Rigidbody2D rigid = shadow.AddComponent<Rigidbody2D>();
        rigid.gravityScale = 0f;
    }
}
