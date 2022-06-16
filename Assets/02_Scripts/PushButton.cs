using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButton : MonoBehaviour
{
    public UnityEvent onButtonPress;
    public UnityEvent onButtonPull;
    public Sprite pushSprite;
    public Sprite pullSprite;
    private SpriteRenderer curSprite;

    private List<GameObject> collisions = new List<GameObject>();

    public LayerMask targetLayer;


    private void Start()
    {
        curSprite = GetComponent<SpriteRenderer>();
        onButtonPress.AddListener(Press);
        onButtonPull.AddListener(Pull);
    }

    public void Pull()
    {
        Debug.Log("¶¼Áü");
        curSprite.sprite = pullSprite;
    }

    public void Press()
    {
        Debug.Log("´­¸²");
        curSprite.sprite = pushSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & targetLayer) != 0)
        {
            if (!collisions.Contains(collision.gameObject) || collisions.Count == 0)
                collisions.Add(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & targetLayer) != 0)
        {
            onButtonPress.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & targetLayer) != 0)
        {
            if (collisions.Contains(collision.gameObject))
            {
                collisions.Remove(collision.gameObject);
                onButtonPull.Invoke();
            }
        }
    }
}
