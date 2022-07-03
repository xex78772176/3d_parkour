using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    public Transform target;
    public Vector3 offset;
    public float speed;

    private void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        Vector3 newPos = target.position + offset;

        transform.position = Vector3.Slerp(transform.position, newPos, speed);
    }
}
