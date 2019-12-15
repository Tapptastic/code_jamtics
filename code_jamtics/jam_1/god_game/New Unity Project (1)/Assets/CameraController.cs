using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    
    public Transform targetToFollow;
    public Vector3 offsetFromTarget;

    // Update is called once per frame
    void Update()
    {
        transform.position = targetToFollow.position + offsetFromTarget;
    }
}
