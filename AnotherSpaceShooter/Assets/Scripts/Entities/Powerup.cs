using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public PowerUpType type;
    private Rigidbody2D rBody;
    public List<Sprite> sprites;
    public float lifetime;
    public float speed;
    //public SpriteRenderer sRenderer;

    public Powerup(PowerUpType pType)
    {
        type = pType;
    }

    private void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        rBody.velocity = Random.onUnitSphere * speed;
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        switch (type)
        {
            case Powerup.PowerUpType.FireRate:
                spriteRenderer.sprite = sprites[0];
                break;
            case Powerup.PowerUpType.Piercing:
                spriteRenderer.sprite = sprites[1];
                break;
            case Powerup.PowerUpType.Spread:
                spriteRenderer.sprite = sprites[2];
                break;
        }

        Destroy(gameObject, lifetime);
    }

    public enum PowerUpType
    {
        Piercing,
        Spread,
        FireRate
    }


}
