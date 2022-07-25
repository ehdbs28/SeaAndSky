using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseItemObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ReverseAbility ability = collision.GetComponent<ReverseAbility>();
            
            if (ability)
            {
                ability.IsReverse = true;
                gameObject.SetActive(false);
            }
        }
    }
}
