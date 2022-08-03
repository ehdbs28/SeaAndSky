using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrass : MonoBehaviour
{
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float yPosition;

    [SerializeField] private Sprite[] sprites;

    [SerializeField] private int count = 30;

    [SerializeField] private LayerMask layer;
    [SerializeField] private Material material;

    private Vector2 rayDirection;
    private Vector3 rotation = Vector3.zero;

    [SerializeField] private Color spriteColor = Color.white;

    public enum Direction
    {
        Up,
        Down,
        Right,
        Left
    }

    [SerializeField] private Direction direction;

    private void Awake()
    {
        //EventManager.StartListening("LoadStage", Generate);
    }

    void Start()
    {
        switch (direction)
        {
            case Direction.Up:
                rayDirection = Vector2.up;
                rotation = Vector3.forward * 180f;
                break;
            case Direction.Down:
                rayDirection = Vector2.down;
                break;
            case Direction.Right:
                rayDirection = Vector2.right;
                rotation = Vector3.forward * -90f;
                break;
            case Direction.Left:
                rayDirection = Vector2.left;
                rotation = Vector2.left * 90f;
                break;
        }

        Generate();
    }

    private void Generate()
    {
        GameObject temp = null;

        for (int i = 0; i < count; i++)
        {
            float randomX = (int)Random.Range(minX, maxX) + 0.5f;
            SpriteRenderer renderer = new GameObject($"Grass{i}").AddComponent<SpriteRenderer>();
            renderer.sprite = sprites[Random.Range(0, sprites.Length)];
            renderer.material = material;
            renderer.color = spriteColor;

            renderer.transform.SetPositionAndRotation(GetGrassPosition(randomX), Quaternion.Euler(rotation));
            renderer.transform.SetParent(transform);
        }

        temp?.SetActive(true);
    }

    Vector2 GetGrassPosition(float x)
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(new Vector3(x, yPosition), rayDirection, 30f, layer);
        return new Vector2(x, hitInfo.point.y);
    }

    private void OnDestroy()
    {
        //EventManager.StopListening("LoadStage", Generate);
    }
}