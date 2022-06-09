using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    private Transform _player;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerMove>().transform;
    }
    private void Update()
    {
        transform.position = new Vector2(_player.position.x, -_player.position.y);
    }
}
