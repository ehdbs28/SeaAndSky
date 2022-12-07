using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public abstract void GetItem(Player player);

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            Player p = other.GetComponent<Player>();
            if(p==null) return;
            GetItem(p);
            gameObject.SetActive(false);
            ItemSpawner.Instance.RespawnItem(this);
        }
    }
}
