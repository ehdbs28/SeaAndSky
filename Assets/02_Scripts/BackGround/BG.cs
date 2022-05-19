using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{
    [SerializeField] private Vector2 _parallaxRatio;
    private Transform _mainCamTrm;
    private Vector3 _lastCamPos;
    private float _textureUnitSizeX;
    private float _textureUnitSizeY;

    private void Start()
    {
        _mainCamTrm = Camera.main.transform;
        _lastCamPos = _mainCamTrm.position;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Sprite sprite = sr.sprite;
        Texture2D texture = sprite.texture;

        _textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
        _textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        Vector3 deltaMove = _mainCamTrm.position = _lastCamPos;

        transform.position += new Vector3(deltaMove.x * _parallaxRatio.x, deltaMove.y * _parallaxRatio.y, 0);

        _lastCamPos = _mainCamTrm.position;

        if(Mathf.Abs(_mainCamTrm.position.x - transform.position.x) > _textureUnitSizeX)
        {
            float offsetPositionX = Mathf.Abs(_mainCamTrm.position.x - transform.position.x) % _textureUnitSizeX;
            transform.position = new Vector3(_mainCamTrm.position.x + offsetPositionX, transform.position.y);
        }

        if(Mathf.Abs(_mainCamTrm.position.y - transform.position.y) > _textureUnitSizeY)
        {
            float offsetPositionY = Mathf.Abs(_mainCamTrm.position.y - transform.position.y) % _textureUnitSizeY;
            transform.position = new Vector3(_mainCamTrm.position.y + offsetPositionY, transform.position.y);   
        }
    }
}
