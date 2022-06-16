using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimationData : MonoBehaviour
{
    private Animator _animator = null;
    public Animator _Animator { get => _animator; }
    private void Awake()
    {
        _animator = transform.parent.Find("Sprite").GetComponent<Animator>();   
    }
}
