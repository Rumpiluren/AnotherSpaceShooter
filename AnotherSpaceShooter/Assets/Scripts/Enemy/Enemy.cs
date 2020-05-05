using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour, IKillable
{
    public Transform target;
    protected Vector2 targetPosition;

    public Bullet bulletPrefab;
    public List<Transform> turrets = new List<Transform>();
    private int activeTurret;

    public string enemyName;
    public float fireRate;
    public float speed;
    public float health;
    public GameObject deathEffect;
    public float desiredDistance = 1f;

    protected Rigidbody2D rBody;

    public delegate void DeathEvent();
    public event DeathEvent OnDeath;

    public Powerup powerUp;
    public int dropRate;

    void Awake()
    {
        //Initialization
        rBody = GetComponent<Rigidbody2D>();
        if (turrets.Count == 0) { turrets.Add(transform); }     //Ensure there is a turret transform; this is used to spawn bullets.
        targetPosition = Vector2.zero;
    }

    private void FixedUpdate()
    {
        SetVelocity();
    }

    void SetVelocity()
    {
        //Sets velocity based on distance. Zero if we are close enough.
        if (Vector2.Distance(transform.position, targetPosition) > .2f)
        {
            rBody.velocity = (targetPosition - (Vector2)transform.position).normalized * speed;
        }
        else
        {
            rBody.velocity = Vector3.zero;
        }
    }

    void Fire()
    {
        //Several turrets on one entity means the enemy alternates their active turret with each fire.
        if (activeTurret >= turrets.Count)
        {
            activeTurret = 0;
        }

        Transform turret = turrets[activeTurret];

        //Find the angle between the turret and the target.
        Vector2 distangle = target.position - turret.transform.position;
        float angle = Mathf.Atan2(distangle.y, distangle.x) * Mathf.Rad2Deg;

        //Spawn bullet and set the speed
        Bullet newBullet = Instantiate(bulletPrefab, turret.transform.position, Quaternion.Euler(0, 0, angle));

        //Increment activeTurret for next shooting instance
        activeTurret++;
    }

    void UpdateTargetPos()
    {
        Debug.Log("ERROR: Base Enemy Function 'UpdateTargetPos' function called. This resets the targetPosition. Implement one in the enemy script instead.");
        targetPosition = Vector2.zero;
        return;
    }

    public void Damage(int damage)
    {
        //Decrease HP, Kill if 0
        health -= damage;
        if (health <= 0f)
        {
            Kill();
        }
    }

    public void Kill()
    {
        //Roll powerup drop on death, instantiate death particle effect, destroy this object
        OnDeath?.Invoke();
        if (Random.Range(1, 100) <= dropRate)
        {
            Powerup spawnedPowerUp = Instantiate(powerUp, transform.position, Quaternion.identity);
            spawnedPowerUp.type = (Powerup.PowerUpType)Random.Range(0, 3);
        }
        Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
