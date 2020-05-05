using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IKillable
{
    PlayerControls controls;
    private PlayerInput inputManager;

    protected Rigidbody2D rBody;
    new Collider2D collider2D;
    public Bullet bulletPrefab;
    private int bulletHealth = 1;
    public float fireRate;
    private float fireCooldown;
    public GameObject turret;
    public GameObject deathEffect;
    new protected Camera camera;

    public float speed;
    protected float currentSpeed;
    protected Vector2 movementInput;
    protected bool isShooting;

    private float powerDecay;
    private List<int> lastUpgrades = new List<int>();
    private int powerSpread = 1;
    private int powerPierce = 1;
    private int powerRate = 1;

    public delegate void DeathEvent();
    public event DeathEvent OnDeath;

    void Awake()
    {
        inputManager = new PlayerInput(this.gameObject);
        controls = new PlayerControls();
        rBody = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        camera = Camera.main;

        inputManager.MovementEvent += SetMovement;
        inputManager.ShootEvent += ToggleShoot;

        controls.Gameplay.Enable();
    }

    protected virtual void FixedUpdate()
    {
        ApplyShoot();
        Movement();
    }

    protected virtual void Update()
    {
        DecayPower();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //If player collides with powerup, increase player's power.
        if (other.tag == "Powerup")
        {
            Powerup powerUp = other.GetComponent<Powerup>();
            if (powerUp)
            {
                switch (powerUp.type)
                {
                    case Powerup.PowerUpType.FireRate:
                        powerRate++;
                        lastUpgrades.Add(1);
                        break;
                    case Powerup.PowerUpType.Piercing:
                        powerPierce++;
                        lastUpgrades.Add(2);
                        break;
                    case Powerup.PowerUpType.Spread:
                        powerSpread++;
                        lastUpgrades.Add(3);
                        break;
                    default:
                        Debug.Log("This was a powerup, but not of any known type?");
                        break;
                }
                powerDecay = 1000;
            }
            Destroy(other.gameObject);
        }
    }

    void SetMovement(Vector2 input)
    {
        movementInput = input;
    }

    void DecayPower()
    {
        if (lastUpgrades.Count > 0)
        {
            powerDecay = Mathf.Clamp((powerDecay - (powerSpread * 1 + powerPierce * 1 + powerRate * 1) * Time.deltaTime), 0, 100);
            if (powerDecay <= 0)
            {
                switch (lastUpgrades[0])
                {
                    case 1:
                        powerRate--;
                        break;
                    case 2:
                        powerPierce--;
                        break;
                    case 3:
                        powerSpread--;
                        break;
                }
                lastUpgrades.RemoveAt(0);
                if (powerSpread + powerPierce + powerRate > 3)
                {
                    powerDecay = 1000;
                }
            }
        }
    }

    public virtual void Movement()
    {
        //Called every FixedUpdate
    }

    void ToggleShoot(bool pressed)
    {
        //This is subscribed to the playerInput class.
        isShooting = pressed;
    }

    void ApplyShoot()
    {
        if (isShooting && fireCooldown <= 0f)
        {
            SpawnProjectile(0);
            for (int i = 1; i < powerSpread; i++)
            {
                SpawnProjectile(10 * i);
                SpawnProjectile(-10 * i);
            }
            fireCooldown = Mathf.Clamp(fireRate - (0.1f * powerRate), 0.0001f, Mathf.Infinity);
        }
        else if (fireCooldown > 0f)
        {
            fireCooldown -= 1f * Time.fixedDeltaTime;
        }
    }

    void SpawnProjectile(float rotationOffset)
    {
        Bullet newProjectile;
        Quaternion projectileRotation = Quaternion.Euler(0, 0, rotationOffset);
        newProjectile = Instantiate(bulletPrefab, turret.transform.position, turret.transform.rotation * projectileRotation);
        newProjectile.hp = bulletHealth * powerPierce;
    }

    public void Damage(int damage)
    {
        Kill();
    }

    void DeathEffect()
    {
        GameObject newExplosion;
        newExplosion = Instantiate(deathEffect, transform.position, transform.rotation);
        newExplosion.transform.localScale *= 2f;
        for (int i = 0; i < 8; i++)
        {
            //Player Death should be clearly visible, so I went ham here.
            Quaternion rotationAddition = Quaternion.Euler(0, 0, 45 * i);
            newExplosion = Instantiate(deathEffect, transform.position, transform.rotation * rotationAddition);
            newExplosion.transform.localScale *= 1.5f;
            Rigidbody2D newRigidBody = newExplosion.GetComponent<Rigidbody2D>();
            newRigidBody.velocity = newRigidBody.transform.right * 8;
        }
    }

    public void Kill()
    {
        OnDeath?.Invoke();
        controls.Gameplay.Disable();
        collider2D.enabled = false;
        isShooting = false;

        DeathEffect();
        gameObject.SetActive(false);
    }
}
