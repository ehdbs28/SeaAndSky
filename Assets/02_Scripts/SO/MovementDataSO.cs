using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SO/MoveData")]
public class MovementDataSO : ScriptableObject
{
    public float maxSpeed = 5;

    public float acceleration = 50, deAcceleration = 50;
} 
