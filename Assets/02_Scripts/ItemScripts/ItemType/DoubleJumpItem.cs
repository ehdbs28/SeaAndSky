using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpItem : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(true);  
    }

    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.CompareTag("Player")){
            Player player = other.transform.GetComponent<Player>();
            if(player != null) player.CanDoubleJump = true;
            gameObject.SetActive(false);
        }
    }
}
