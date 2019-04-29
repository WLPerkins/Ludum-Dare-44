using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Camera cam;
    public float playSize;
    public float smoothSizeSpeed;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    public bool followingPlayer;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Invoke("StartFollowingPlayer", 3.5f);
    }

    private void FixedUpdate()
    {
        if (followingPlayer)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            float desiredSize = playSize;
            float smoothedSize = Mathf.Lerp(cam.orthographicSize, desiredSize, smoothSizeSpeed);
            cam.orthographicSize = smoothedSize;
        }


        
    }

    void StartFollowingPlayer()
    {
        followingPlayer = true;
    }
}
