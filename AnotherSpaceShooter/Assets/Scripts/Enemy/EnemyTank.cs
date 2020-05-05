using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : Enemy
{
    public GameObject turretBase;
    void Start()
    {
        InvokeRepeating("UpdateTargetPos", 0, Random.Range(4f, 6f));
        InvokeRepeating("Fire", Random.Range(1f / fireRate, 20f / fireRate), Random.Range(1f / fireRate, 20f / fireRate));
    }

    private void Update()
    {
        transform.right = targetPosition - (Vector2)transform.position;
        turretBase.transform.right = target.position - transform.position;
    }

    void UpdateTargetPos()
    {
        targetPosition = CustomScript.FindRandomInsideCircle(target.position, 2);
        return;
    }

}
