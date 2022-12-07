using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoapBubbleItem : Item
{
    private SoapBubble _soapBubble;

    private void Awake()
    {
        _soapBubble = FindObjectOfType<SoapBubble>();
    }

    public override void GetItem(Player player)
    {
        PlayerArea playerArea = player.GetComponent<PlayerArea>();
        _soapBubble.gameObject.SetActive(true);
        _soapBubble.DestroyBubbleAnim();

        playerArea.IsSoapBubble = true;
        playerArea.ChangedState();
    }
}
