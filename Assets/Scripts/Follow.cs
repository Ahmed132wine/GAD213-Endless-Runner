using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Follow : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;       
    public float xOffset = 2f;     // How far ahead on the X axis the camera looks
    public float smooth = 8f;      // How smoothly the camera follows

    private float fixedY;
    private float fixedZ;

    void Start()
    {
        // Lock the Y and Z position from the camera's initial position
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (!target) return;

        
        float desiredX = target.position.x + xOffset;

        
        float newX = Mathf.Lerp(transform.position.x, desiredX, smooth * Time.deltaTime);

        // Apply the position while locking Y and Z
        transform.position = new Vector3(newX, fixedY, fixedZ);
    }
}
