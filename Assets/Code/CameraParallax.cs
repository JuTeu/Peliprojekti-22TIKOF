using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraParallax : MonoBehaviour
{
    [SerializeField] float speed = 0.5f;
    [SerializeField] float yOffset;
    [SerializeField] GameObject cam;
    private Transform tr;

    void Start()
    {
        tr = GetComponent<Transform>();
    }
    void LateUpdate()
    {
        tr.position = new Vector3(cam.transform.position.x * speed, cam.transform.position.y * speed + yOffset, transform.position.z);
    }
}
