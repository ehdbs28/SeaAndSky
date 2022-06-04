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

    void Start()
    {
        Generate();
    }

    private void Generate()
    {
        for (int i = 0; i < count; i++)
        {
            float randomX = (int)Random.Range(minX, maxX) + 0.5f;
            SpriteRenderer renderer = new GameObject($"Grass{i}").AddComponent<SpriteRenderer>();
            renderer.sprite = sprites[Random.Range(0, sprites.Length)];
            renderer.material = material;

            renderer.transform.position = GetGrassPosition(randomX);
            renderer.transform.SetParent(transform);
        }
    }

    Vector2 GetGrassPosition(float x)
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(new Vector3(x, yPosition), Vector3.down, 30f, layer);
        return new Vector2(x, hitInfo.point.y);
    }
}
