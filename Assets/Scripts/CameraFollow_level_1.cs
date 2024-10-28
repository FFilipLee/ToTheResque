using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow_level_1 : MonoBehaviour
{
    public Transform target; // Reference to the player object
    public float smoothSpeed = 0.125f; // Speed of camera movement

    public Vector3 offset = new Vector3(0, 4, 0); // Offset of the camera from the player

    void FixedUpdate()
    {
        // Calculate the desired position of the camera
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, offset.y, transform.position.z);

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position
        transform.position = smoothedPosition;
    }
}
