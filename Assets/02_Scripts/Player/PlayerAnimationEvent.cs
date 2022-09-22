using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    private PlayerMove _player;

    private void Start() {
        _player = GetComponentInParent<PlayerMove>();
    }

    public void DeadEvent(){
        _player.EndDeadAnim();
    }
}
