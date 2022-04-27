using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ConstantManager;

public class TeleportItem : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    void Start()
    {
        gameObject.SetActive(true);
        EventManager.StartListening(TELEPORTATION, PlayerTeleport);
    }

    void PlayerTeleport()
    {
        gameObject.SetActive(false);
    }
}
