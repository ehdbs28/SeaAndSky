using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDie : MonoBehaviour
{


    [Header("OnOff¿©ºÎ")]
    public bool isToggle;

    private bool isActive = false;
    public float timeDelay = 3f;

    private Animator animator;
    private int _hashIsActive = Animator.StringToHash("isActive");
    private float timer = 0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        OnOffTimer();
    }
    public void OnTrapAnim()
    {
        ChangeActive(true);
    }
    public void OffTrapAnim()
    {
        ChangeActive(false);
    }
    private void OnOffTimer()
    {
        if (isToggle == true)
        {
            timer += Time.deltaTime;
            if (timer >= timeDelay)
            {
                timer = 0;
                if (isActive)
                {
                    animator.SetBool(_hashIsActive, false);
                }
                else
                {
                    animator.SetBool(_hashIsActive, true);
                }
            }
        }
    }

    private void ChangeActive(bool value)
    {
        isActive = value;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && isActive)
        {
            GameManager.Instance.ReduceHeart();
        }
    }

}
