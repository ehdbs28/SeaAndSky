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
    [SerializeField] private bool isColChanged = false;
    [SerializeField] private bool isChild = false;
    private Collider2D col;
    private Collider2D myCol;
    private Rigidbody2D rigid;
    private float gravity;
    private LayerMask _shadowLayer;
    private DissolveEffect dissolveEffect;

    void Start()
    {
        shadow = new GameObject($"{name}Shadow");
        rigid = GetComponentInChildren<Rigidbody2D>();
        dissolveEffect = GetComponent<DissolveEffect>();
        if (rigid != null)
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
            myCol = GetComponentInChildren<Collider2D>();
            col = shadow.AddComponent(myCol.GetType()) as Collider2D;
            SettingCollider();
        }
        if (isMoved)
        {
            SettingRigidbody();
        }
        if (isChild)
        {
            Shadow.transform.SetParent(transform);
        }
        shadow.layer = gameObject.layer;
        shadow.tag = gameObject.tag;
    }

    void Update()
    {
        shadow.transform.position = new Vector2(transform.position.x, -transform.position.y);
        shadow.transform.eulerAngles = -spriteRenderer.transform.eulerAngles;

        if (!spriteRenderer.sprite && !shadowRenderer.sprite) return;
        shadowRenderer.sprite = spriteRenderer.sprite;
        Vector3 curScale = spriteRenderer.transform.localScale;
        curScale.y *= -1f;
        shadow.transform.localScale = curScale;

        if (isColChanged) SettingCollider();
    }

    private void SettingCollider()
    {
        if (myCol is BoxCollider2D)
        {
            BoxCollider2D bCol = col as BoxCollider2D;
            bCol.offset = myCol.offset;
            bCol.size = (myCol as BoxCollider2D).size;
        }
        if (myCol is PolygonCollider2D)
        {
            PolygonCollider2D pCol = col as PolygonCollider2D;
            pCol.points = (myCol as PolygonCollider2D).points;
            pCol.offset = myCol.offset;
        }
        else
        {
            col.offset = myCol.offset;
        }
    }

    private void SettingRigidbody()
    {
        Rigidbody2D rigid = shadow.AddComponent<Rigidbody2D>();
        rigid.isKinematic = true;
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