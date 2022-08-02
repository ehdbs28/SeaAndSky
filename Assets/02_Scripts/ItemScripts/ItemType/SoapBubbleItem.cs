using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoapBubbleItem : MonoBehaviour
{
    private SoapBubble _soapBubble;

    private void Awake()
    {
        gameObject.SetActive(true);
        _soapBubble = FindObjectOfType<SoapBubble>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerArea playerArea = FindObjectOfType<PlayerArea>();
        _soapBubble.gameObject.SetActive(true);
        _soapBubble.DestroyBubbleAnim();

        playerArea.IsSoapBubble = true;
        playerArea.ChangedState();

        gameObject.SetActive(false);
    }
}
