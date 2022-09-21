using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackEffect : MonoBehaviour
{   
    private PlayerMove _player;

    private void Awake() {
        _player = transform.parent.GetComponentInParent<PlayerMove>();
    }

    private void OnEnable() {
        Vector3 parentPos = transform.parent.parent.position;

        Vector3 scale = new Vector3((_player.isLeft) ? -1f : 1f, 3f, 1f);

        float x = (_player.IsGround) ? (_player.isLeft) ? parentPos.x - 1.5f : parentPos.x + 1.5f : parentPos.x;
        float y = (Input.GetKey(KeyCode.UpArrow)) ? parentPos.y + 1.5f : (Input.GetKey(KeyCode.DownArrow)) ? parentPos.y - 1.5f : parentPos.y;
        Vector3 position = new Vector3(x, y);

        float rotate = (Input.GetKey(KeyCode.UpArrow)) ? 90 : (Input.GetKey(KeyCode.DownArrow)) ? -90 : 0;

        transform.localScale = scale;
        transform.position = position;
        transform.rotation = Quaternion.AngleAxis(rotate, Vector3.forward);
    }
}