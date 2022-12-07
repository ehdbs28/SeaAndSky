using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoSingleton<ItemSpawner>
{
    private float regenerationTime = 3f;

    public void RespawnItem(Item item)
    {
        StartCoroutine(ItemRegenerationCoroutine(item));
    }

    private IEnumerator ItemRegenerationCoroutine(Item item)
    {
        yield return new WaitForSeconds(regenerationTime);
        item.gameObject.SetActive(true);
    }
}
