using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChopper : Player
{
    public override void Movement()
    {
        //Rotate chopper towards mouse position
        Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 reformedMousePosition = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        transform.right = reformedMousePosition - transform.position;


        if (movementInput.magnitude > 0)
        {
            currentSpeed = rBody.velocity.magnitude;

            //Add input to current velocity, then clamp it. Move in direction of input.
            Vector2 newVelocity = movementInput * speed + rBody.velocity;
            newVelocity = Vector2.ClampMagnitude(newVelocity, 2f);
            rBody.velocity = newVelocity;
        }
        else
        {
            //Smooth out movement when player does not give input.
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.fixedDeltaTime);
            rBody.velocity = rBody.velocity.normalized * currentSpeed;
        }
    }
}
