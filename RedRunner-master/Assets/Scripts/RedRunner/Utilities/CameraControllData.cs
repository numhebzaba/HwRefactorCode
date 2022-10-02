using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllData
{
    public Vector3 cameraPosition;
    public Vector3 targetPosition;
    public float speed;

    public CameraControllData(Vector3 cameraPosition, Vector3 targetPosition, float speed)
    {
        this.cameraPosition = cameraPosition;
        this.targetPosition = targetPosition;   
        this.speed = speed;
    }

}
