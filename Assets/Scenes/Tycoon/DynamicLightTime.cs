using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLightTime : MonoBehaviour
{
    [Header("Day Cycle Time")]
    [Tooltip("The time it takes for the day cycle to complete in minutes.")]
    [Range(1.0f, 120.0f)]
    public double dayCycleTime = 1.0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        // Set initial x and y rotation
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the current time of day as a value between 0 and 1
        double currentTimeOfDay = (System.DateTime.Now.TimeOfDay.TotalMinutes / dayCycleTime) % 1;

        // Calculate the angle of rotation for the sun based on the current time of day
        float rotationAngle = (float)(360 * currentTimeOfDay);

        // Calculate the x and y rotation using trigonometry with x between 0 and 360 and y between -90 and 90
        float xRotation = Mathf.Sin(rotationAngle * Mathf.Deg2Rad) * 360;

        // Apply the rotation to the sun's transform
        transform.rotation = Quaternion.Euler(transform.rotation.x + xRotation, transform.rotation.y, transform.rotation.z);
    }
}
