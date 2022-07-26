using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateShadow : MonoBehaviour
{
    private GameObject shadow;
    public GameObject Shadow { get => shadow; }

    private SpriteRenderer shadowRenderer;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private bool isCollide = true;
    [SerializeField] private bool isMoved = false;

    private Rigidbody2D rigid;
    private float gravity;
    private LayerMask _shadowLayer;
    private DissolveEffect dissolveEffect;

    void Start()
    {
        shadow = new GameObject($"{name}Shadow");
        rigid = GetComponentInChildren<Rigidbody2D>();
        dissolveEffect = GetComponent<DissolveEffect>();
        if(rigid != null)
            gravity = rigid.gravityScale;

        transform.GetComponentsInChildren<SpriteRenderer>();

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        shadowRenderer = shadow.AddComponent<SpriteRenderer>();

        Vector3 scale = transform.localScale;
        scale.y *= -1;
        shadow.transform.localScale = scale;

        shadow.transform.eulerAngles = -spriteRenderer.transform.eulerAngles;
        shadow.gameObject.layer = _shadowLayer;
        shadowRenderer.color = new Color(0, 0, 0, 0.4f);

        if (isCollide)
        {
            SettingCollider();
        }
        if (isMoved)
        {
            SettingRigidbody();
        }

        shadow.layer = gameObject.layer;
        shadow.tag = gameObject.tag;
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

    public void ChangeToShadow()
    {
        gravity *= -1f;
        rigid.gravityScale = gravity;
        
        AreaState state = (gravity > 0) ? AreaState.Sky : AreaState.Sea;
        
        dissolveEffect.PlayEffect(state);
        transform.position = shadow.transform.position;
    }
}