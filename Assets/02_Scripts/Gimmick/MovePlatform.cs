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

    private void Awake()
    {
        originVector = transform.position;
    }
    public void MoveToOffsetPosistion()
    {
        seq = DOTween.Sequence();
        Vector2 endVec = new Vector2(originVector.x + offsetX, originVector.x + offsetY);
        seq.Append(transform.DOMove(endVec, moveTime));
    }
    public void MoveToOriginPosition()
    {

    }

}
