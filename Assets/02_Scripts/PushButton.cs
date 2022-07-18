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

    [SerializeField] private LayerMask _targetLayer;
    private bool _isEnter = false;

    private void Start()
    {
        curSprite = GetComponent<SpriteRenderer>();
        onButtonPress.AddListener(Press);
        onButtonPull.AddListener(Pull);
    }

    public void Pull()
    {
        curSprite.sprite = pullSprite;
    }

    public void Press()
    {
        curSprite.sprite = pushSprite;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_isEnter) return;
        if ((_targetLayer &(1 << collision.gameObject.layer)) > 0)
        {   
            Debug.Log("stay");
            _isEnter = true;
            if (!collisions.Contains(collision.gameObject) || collisions.Count == 0)
                collisions.Add(collision.gameObject);
            onButtonPress?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((_targetLayer & (1 << collision.gameObject.layer)) > 0)
        {
            { 
                if (collisions.Contains(collision.gameObject))
                {
                    collisions.Remove(collision.gameObject);
                    _isEnter = false;
                    onButtonPull?.Invoke();
                }
            }
        }
    }
}
