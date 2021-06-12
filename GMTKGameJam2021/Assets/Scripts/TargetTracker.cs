using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTracker : MonoBehaviour
{
    public Transform target;

    public float smoothTime = 0.3f;
    public float xThreshold = 0.01f;
    public float yThreshold = 0.01f;
    public float yVelocity = 0.0f;
    public float xVelocity = 0.0f;

    void FixedUpdate()
    {
        var newY = Mathf.SmoothDamp(transform.position.y, target.position.y, ref yVelocity, smoothTime * Time.smoothDeltaTime);
        var newX = Mathf.SmoothDamp(transform.position.x, target.position.x, ref xVelocity, smoothTime * Time.smoothDeltaTime);

        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    } 
}
