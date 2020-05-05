using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTiles : MonoBehaviour
{
    public int width;
    public int height;
    public float size;
    private Vector2 offset;
    public List<Sprite> sprites;

    void Start()
    {
        offset = new Vector2(-4.75f, -2.75f);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject obj = new GameObject(x + " " + y);
                SpriteRenderer spriteRenderer = obj.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count)];
                obj.transform.parent = transform;
                obj.transform.position = new Vector3(offset.x + x * size, offset.y + y * size, 1);
            }
        }
    }
}
