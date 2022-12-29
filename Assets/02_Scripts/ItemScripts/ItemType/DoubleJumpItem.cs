using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpItem : Item
{
    [SerializeField] private GameObject _getItemParticle;

    public override void GetItem(Player player)
    {
        GameObject particle = Instantiate(_getItemParticle);
        particle.transform.position = transform.position;
        player.CanDoubleJump = true;
    }
}
