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
        gameObject.SetActive(false);
    }

    private void Update() {
        _anim.SetFloat("AnimSpeed", (GameManager.Instance.PlayerState == AreaState.Sky) ? 5f : 1f);
    }

    public void StartBubble(){
        _anim.Play("StartBubble");
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
        
        transform.localScale = new Vector3(2.1f, 2.1f, 1f);
        gameObject.SetActive(false);
    }

    IEnumerator DestroyCoroutine()
    {
        float currentTime = 0;
        
        while(currentTime <= ((GameManager.Instance.PlayerState == AreaState.Sky) ? 0 : _destroyTime / 2)){
            currentTime += Time.deltaTime;
            yield return null;
        }
        currentTime = 0;

        _anim.Play("FadeBubble");
        
        while(currentTime <= ((GameManager.Instance.PlayerState == AreaState.Sky) ? 0 : _destroyTime / 2)){
            currentTime += Time.deltaTime;
            yield return null;
        }

        _anim.Play("DestroyBubble");
    }
}
