using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow_level_2 : MonoBehaviour
{
    public Transform target; // Reference to the player object
    public float smoothSpeed = 0.125f; // Speed of camera movement

    public Vector3 offset = new Vector3(0, -1.6f, 0); // Offset of the camera from the player

    void Update()
    {
        // Calculate the desired position of the camera
        Vector3 desiredPosition = new Vector3(offset.x, target.position.y + offset.y, transform.position.z);

        // Update the camera's position
        transform.position = desiredPosition;
    }
}