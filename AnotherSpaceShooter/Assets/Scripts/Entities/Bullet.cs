using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public GameObject impactEffect;
    public int damage = 1;
    public int hp = 1;

    protected Rigidbody2D rigidBody;
    protected GameObject owner;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = transform.right * speed;
        Destroy(gameObject, lifetime);
        damage = 1;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Instantiate(impactEffect, transform.position, transform.rotation);
        if (hitInfo.attachedRigidbody != null && rigidBody != null)
        {
            hitInfo.attachedRigidbody.AddForce(rigidBody.velocity * (speed * 2));
        }

        MonoBehaviour[] list = hitInfo.gameObject.GetComponents<MonoBehaviour>();
        foreach(MonoBehaviour mb in list)
        {
            if (mb is IKillable)
            {
                IKillable killable = (IKillable)mb;
                killable.Damage(damage);
            }
        }

        hp--;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
