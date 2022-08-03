using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleRegeneration : MonoBehaviour
{
    public float regenerationTime = 8f;
    private SoapBubbleItem _item;
    private bool isActive = true; 

    private void Awake()
    {
        _item = GetComponentInChildren<SoapBubbleItem>();
    }

    public void UseItem()
    {
        isActive = false;
        StartCoroutine(ItemRegenerationCoroutine());
    }

    private IEnumerator ItemRegenerationCoroutine()
    {
        yield return new WaitForSeconds(regenerationTime);
        isActive = true;
        _item.gameObject.SetActive(true);
    }
}
