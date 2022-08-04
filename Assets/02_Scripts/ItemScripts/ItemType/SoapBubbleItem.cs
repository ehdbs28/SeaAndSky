using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoapBubbleItem : MonoBehaviour
{
    private SoapBubble _soapBubble;
    public UnityEvent OnGetItem;
    private void Awake()
    {
        gameObject.SetActive(true);
        _soapBubble = FindObjectOfType<SoapBubble>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerArea playerArea = FindObjectOfType<PlayerArea>();
            _soapBubble.gameObject.SetActive(true);
            _soapBubble.DestroyBubbleAnim();

            playerArea.IsSoapBubble = true;
            playerArea.ChangedState();

            gameObject.SetActive(false);
            OnGetItem?.Invoke();
        }
    }
}
