using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour
{
    public bool ispush = false;
    private SpriteRenderer spriteRenderer;
    new private Collider2D collider;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }
    private void Update()
    {
        //if()
    }
}
