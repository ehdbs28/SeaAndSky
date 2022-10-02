using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerArea : MonoBehaviour
{
    private PlayerMove _playerMove;
    private Rigidbody2D _rigid;
    private GenerateShadow _generateShadow;
    [field: SerializeField] private UnityEvent<AreaState> onChangeArea;
    [field: SerializeField] private UnityEvent _failedChangeArea;
    private bool _isSoapBubble = false;
    private float _circleGizmoSize = 0.2f;
    [SerializeField] private LayerMask _isWhatGround;
    public bool IsSoapBubble
    {
        get => _isSoapBubble;
        set => _isSoapBubble = value;
    }

    private void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
        _rigid = GetComponent<Rigidbody2D>();
        _generateShadow = GetComponent<GenerateShadow>();
    }
    private void Start()
    {
        ChangedState();
    }
    void Update()
    {
        if (GameManager.Instance.GameState != GameState.InGame) return;

        if (Input.GetKeyDown(KeySetting.keys[Key.changeworld]) && _playerMove.GroundCheck)
        {
            SetStateChanged();
        }
        //CheckState();
    }

    public void ChangedState()
    {
        if (GameManager.Instance.PlayerState == AreaState.Sky)
        {
            _playerMove.LocalScaleY = 1;
            _playerMove.Speed = 6.2f;
            _rigid.gravityScale = 3.5f;
            _playerMove.JumpPower = 11f;

            onChangeArea.Invoke(AreaState.Sky);
        }
        else if (GameManager.Instance.PlayerState == AreaState.Sea)
        {
            _playerMove.LocalScaleY = _isSoapBubble ? 1 : -1;
            _playerMove.Speed = 5.2f;
            _rigid.gravityScale = _isSoapBubble ? 1f : -2f;
            _playerMove.JumpPower = 17;
            onChangeArea.Invoke(AreaState.Sea);
        }

        EventManager<AreaState>.TriggerEvent("ChangeArea", GameManager.Instance.PlayerState);
    }

    public void SetStateChanged()
    {
        if (Physics2D.OverlapCircle(_generateShadow.Shadow.transform.position, _circleGizmoSize, _isWhatGround))
        {
            _failedChangeArea.Invoke();
            return;
        }
        if (GameManager.Instance.PlayerState == AreaState.Sea)
        {
            GameManager.Instance.PlayerState = AreaState.Sky;
            _rigid.drag = 1f;
        }
        else if (GameManager.Instance.PlayerState == AreaState.Sky)
        {
            GameManager.Instance.PlayerState = AreaState.Sea;
            _rigid.drag = 2f;
        }

        Vector2 chagedPos = new Vector2(transform.position.x, -transform.position.y);
        transform.position = chagedPos;
        ChangedState();
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //if (_generateShadow.Shadow != null)
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawWireSphere(_generateShadow.Shadow.transform.position, _circleGizmoSize);
        //}
    }
#endif
}
