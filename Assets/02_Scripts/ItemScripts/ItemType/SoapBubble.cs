using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoapBubble : MonoBehaviour
{
    [SerializeField] private float _destroyTime = 3f;
    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void DestroyBubbleAnim()
    {
        StartCoroutine(DestroyCoroutine());
    }

    public void DestroyBubble() //animation event
    {
        PlayerArea playerArea = FindObjectOfType<PlayerArea>();

        playerArea.IsSoapBubble = false;
        playerArea.ChangedState();
        
        gameObject.SetActive(false);
    }

    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(_destroyTime);
        _anim.Play("DestroyBubble");
    }
}
