using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTrap : MonoBehaviour
{
        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            IDamage iDamage = collision.transform.GetComponent<IDamage>();
            iDamage?.Damege();
        }
    }
}
