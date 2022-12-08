using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpItem : Item
{
    public override void GetItem(Player player)
    {
        player.CanDoubleJump = true;
    }
}
