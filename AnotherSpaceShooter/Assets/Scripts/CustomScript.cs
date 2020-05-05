using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomScript
{
    //Script for bootleg functions that can be reused. Everything here is static.

    public static Vector2 FindPosAtDistance(Vector2 currentPos, Vector2 targetPos, float distance)
    {
        //Finds position between two vectors at set distance to currentPos.
        Vector2 direction = currentPos - targetPos;
        direction.Normalize();

        Vector2 desiredPosition = targetPos + (direction * distance);
        return desiredPosition;
    }

    public static Vector2 FindRandomInsideCircle(Vector2 circleCentre, float distance)
    {
        //Finds random position within circle
        Vector2 targetPosition = circleCentre + (Random.insideUnitCircle * distance);
        return targetPosition;
    }
}
