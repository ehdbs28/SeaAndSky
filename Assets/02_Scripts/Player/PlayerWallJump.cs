using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJump : MonoBehaviour
{
    [SerializeField] private PhysicsMaterial2D _physicsMat;

    private bool _isWall = false;
    public bool IsWall {get => _isWall;}

    
}
