using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    private Player _player;

    private void Start() {
        _player = GetComponentInParent<Player>();
    }

    public void DeadEvent(){
        _player.EndDeadAnim();
    }
}
