using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    private Vector3 velocity = Vector3.zero;
    public Bounds cameraBounds;
    private Camera cam;
    private Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    public bool devCam = false;
    void Update()
    {
        // transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.position.x, player.position.y, -10), ref velocity, Time.deltaTime);
        //targetPosition = new Vector3(player.position.x, player.position.y, -10);
        if (!devCam)
        {
            transform.position = new Vector3(
                Mathf.Clamp(player.position.x, cameraBounds.min.x, cameraBounds.max.x),
                Mathf.Clamp(player.position.y, cameraBounds.min.y, cameraBounds.max.y), -10);
        }
        else
        {
            transform.position = new Vector3(player.position.x, player.position.y, -10);
        }
    }
}
