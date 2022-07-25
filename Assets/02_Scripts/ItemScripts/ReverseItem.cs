using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseItem : MonoBehaviour
{
    private const float BOX_SIZE = 2.3f;
    private LayerMask TARGET_LAYER;

    private const float UI_X_OFFSET = 1.5f;
    private const float UI_Y_OFFSET = 0.3f;

    private void Start()
    {
        // ÀÏ´Ü Box¸¸...
        TARGET_LAYER = LayerMask.GetMask("Box");
    }

    private void Update()
    {
        Collider2D collider = Physics2D.OverlapBox(transform.position, Vector2.one * BOX_SIZE, 0f, TARGET_LAYER);

        if (collider)
        {
            Vector2 point = collider.transform.position;
            Camera cam = point.y > 0 ? GameManager.Instance.skyCamera : GameManager.Instance.seaCamera;

            GameManager.Instance.UIManager.SetInteractionButton(true, cam.WorldToScreenPoint(GetUIPosition(point)));

            if (Input.GetKeyDown(KeyCode.C))
            {
                ReverseObject(collider);
            }
        }
        else
        {
            GameManager.Instance.UIManager.SetInteractionButton(false);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Vector3.one * BOX_SIZE);
    }

    private void ReverseObject(Collider2D collider)
    {
        GenerateShadow shadowController = collider.GetComponent<GenerateShadow>();
        shadowController.ChangeToShadow();
    }

    private Vector2 GetUIPosition(Vector2 point)
    {
        // O
        // I   ¤±(BOX)
        // A
        if (transform.position.x < point.x)
        {
            point.x += UI_X_OFFSET;
        }
        else
        {
            point.x += -UI_X_OFFSET;
        }

        point.y += UI_Y_OFFSET;
        return point;
    }
}