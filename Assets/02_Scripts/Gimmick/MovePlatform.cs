using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MovePlatform : MonoBehaviour
{
    [SerializeField] 
    private float offsetX, offsetY;
    [SerializeField]
    private float moveTime =1f;
    private Vector2 originVector;
    private Sequence seq;

    private void Start()
    {
        originVector = transform.position;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            MoveToOffsetPosistion();
        }
    }
    public virtual void MoveToOffsetPosistion()
    {
        seq = DOTween.Sequence();
        Vector2 endVec = new Vector2(originVector.x + offsetX, originVector.y + offsetY);
        seq.Append(transform.DOMove(endVec, moveTime));
    }
    public virtual void MoveToOriginPosition()
    {
        seq = DOTween.Sequence();
        seq.Append(transform.DOMove(originVector, moveTime));
    }

}
