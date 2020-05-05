using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowOffset : MonoBehaviour
{
    //Place this on anything that is a shadow.
    public GameObject owner;
    public Vector2 shadowOffset = new Vector2(0.05f, -0.1f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = owner.transform.position + (Vector3)shadowOffset;
    }
}
