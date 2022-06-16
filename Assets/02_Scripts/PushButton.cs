using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButton : MonoBehaviour
{
    public UnityEvent OnButtonPress;
    public UnityEvent OnButtonPull;
    public Sprite pushSprite;
    public Sprite pullSprite;
    SpriteRenderer curSprite;
    private void Start()
    {
        curSprite = GetComponent<SpriteRenderer>();
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


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnButtonPress.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnButtonPull.Invoke();
        }
    }
}
