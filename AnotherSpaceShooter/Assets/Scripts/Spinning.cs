using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    // Place this on propellers to make them spin
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, -1440 * Time.deltaTime));
    }
}
