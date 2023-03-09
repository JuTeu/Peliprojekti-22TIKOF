using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.position.x, player.position.y, -10), ref velocity, Time.deltaTime);
        transform.position = new Vector3(player.position.x, player.position.y, -10);
    }
}
