using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShip : Enemy, IKillable
{
    void Start()
    {
        InvokeRepeating("Fire", 0, 1f / fireRate);
    }

    private void Update()
    {
        float oldSpeed = speed;

        if (Vector2.Distance(targetPosition, transform.position) < 0.2f)
        {
            UpdateTargetPos();
        }
        if (Vector2.Distance(target.position, transform.position) < desiredDistance)
        {
            targetPosition = CustomScript.FindPosAtDistance(targetPosition, target.position, desiredDistance);
        }
        speed = Mathf.Lerp(speed, (transform.position - (Vector3)targetPosition).magnitude * 0.8f, 0.1f);
        transform.right = target.position - transform.position;
    }

    void UpdateTargetPos()
    {
        targetPosition = CustomScript.FindRandomInsideCircle(target.position, desiredDistance);
    }
}
