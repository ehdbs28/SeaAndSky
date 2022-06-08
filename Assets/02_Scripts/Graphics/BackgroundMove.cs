using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private int mainTextureID = Shader.PropertyToID("_MainTex");

    [Flags]
    private enum MoveType
    {
        MoveX = 1,
        MoveY = 2
    }

    [Range(0.01f, 1f)]
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private MoveType moveType;

    [SerializeField] private float offsetX = 1f;
    [SerializeField] private float offsetY = 1f;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        Vector2 offset = meshRenderer.material.GetTextureOffset(mainTextureID);

        if ((moveType & MoveType.MoveX) != 0)
            offset.x += speed * Time.deltaTime;

        if ((moveType & MoveType.MoveY) != 0)
            offset.y += speed * Time.deltaTime;

        meshRenderer.material.SetTextureOffset(mainTextureID, offset);
        meshRenderer.material.SetTextureScale(mainTextureID, new Vector2(offsetX, offsetY));

    }
}