using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TrapType
{
    Blade,
    Spike
}
public class ObstacleDie : MonoBehaviour
{
    public TrapType _trapType;
    public float offsetX = 1f;  
    float currentPosition;  
    float speed = 2.0f;  

    Vector3 originPos;

    [Header("OnOff¿©ºÎ")]
    public bool isToggle;

    private bool isActive = false;
    public float timeDelay = 3f;

    private Animator animator;
    private int _hashIsActive = Animator.StringToHash("isActive");
    private float timer = 0f;

    private void Awake()
    {
        originPos = transform.position;
        animator = GetComponent<Animator>();
        currentPosition = transform.position.x;
        isActive = isToggle == true ? false : true;
    }

    public void Update()
    {
        OnOffTimer();
        MoveBlade();
    }

    public void MoveBlade(){
        if(_trapType == TrapType.Blade)
        {
            currentPosition += Time.deltaTime * speed;
            if (currentPosition >= originPos.x)
            {
                speed *= -1;
                currentPosition = originPos.x;
            }
            else if (currentPosition <= originPos.x -offsetX)
            {
                speed *= -1;
                currentPosition = originPos.x - offsetX;
            }
            transform.position = new Vector3(currentPosition, transform.position.y, 0);
        }
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
        if (_trapType != TrapType.Spike) return;
        if (isToggle == true)
        {
            //Debug.Log(gameObject.name);
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
            if(collision.collider.CompareTag("Player") && isActive)
            {
                GameManager.Instance.ReduceHeart();
            }
    }

}
