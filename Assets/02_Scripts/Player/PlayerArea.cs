using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerArea : MonoBehaviour
{
    [SerializeField] private float _groundJumpPower = 14f;
    [SerializeField] private float _seaJumpPower = 17f;

    private Player _player;
    private Rigidbody2D _rigid;
    private GenerateShadow _generateShadow;
    [field: SerializeField] private UnityEvent<AreaState> onChangeArea;
    [field: SerializeField] private UnityEvent _failedChangeArea;
    private bool _isSoapBubble = false;
    private float _circleGizmoSize = 0.3f;
    [SerializeField] private LayerMask _isWhatGround;
    public bool IsSoapBubble
    {
        get => _isSoapBubble;
        set => _isSoapBubble = value;
    }

    private void Awake()
    {
        _player = GetComponent<Player>();
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

        if (Input.GetKeyDown(KeySetting.keys[Key.changeworld]))//&& _player.IsGorund)
        {
            SetStateChanged();
        }
        //CheckState();
    }

    public void ChangedState()
    {
        _player.Rigidbody.velocity = Vector2.zero;
        if (GameManager.Instance.PlayerState == AreaState.Sky)
        {
            _player.PlayerFlip(_player.VisualObj.localScale.x, 1);
            _player.Speed = 6.2f;
            _rigid.gravityScale = 3.5f;
            _player.JumpPower = _groundJumpPower;

            onChangeArea.Invoke(AreaState.Sky);
        }
        else if (GameManager.Instance.PlayerState == AreaState.Sea)
        {
           _player.PlayerFlip(_player.VisualObj.localScale.x, (_isSoapBubble) ? 1 : -1);
            _player.Speed = 5.2f;
            _rigid.gravityScale = _isSoapBubble ? 1f : -2f;
            _player.JumpPower = _seaJumpPower;
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
}
