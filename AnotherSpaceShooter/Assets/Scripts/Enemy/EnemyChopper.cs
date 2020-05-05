using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChopper : Enemy
{
    void Start()
    {
        InvokeRepeating("Fire", Random.Range(1f / fireRate, 20f / fireRate), Random.Range(1f / fireRate, 20f / fireRate));
    }

    private void Update()
    {
        UpdateTargetPos();
        speed = (transform.position - (Vector3)targetPosition).magnitude * 1.2f;
        transform.right = target.position - transform.position;
    }

    void UpdateTargetPos()
    {
        targetPosition = CustomScript.FindPosAtDistance(transform.position, target.position, desiredDistance);
    }
}
