using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : Player
{
    public GameObject turretBase;

    protected override void Update()
    {
        if (isShooting)
        {
            //If player is shooting, rotate the turret sprite so that it points towards the mouse position.
            Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 reformedMousePosition = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
            turretBase.transform.right = reformedMousePosition - transform.position;
        }
        else
        {
            //If we are not shooting, return the turret to its default state.
            turretBase.transform.right = transform.right;
        }

        //Base needs to be called after turret rotation, or bullets will be fired in strange directions.
        base.Update();
    }

    public override void Movement()
    {
        if (movementInput.magnitude > 0)
        {
            //Move in direction of input, rotate to face desired direction.
            rBody.velocity = movementInput * speed;
            float angle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            rBody.velocity = Vector2.zero;
        }
    }
}
