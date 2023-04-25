using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.U2D;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    private Vector3 velocity = Vector3.zero;
    public Bounds cameraBounds;
    private Camera cam;
    private float transitionMenuTop;
    //private PixelPerfectCamera pixCam;
    private Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        //pixCam = GetComponent<PixelPerfectCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.position.x, player.position.y, -10), ref velocity, Time.deltaTime);
        //targetPosition = new Vector3(player.position.x, player.position.y, -10);
        if (GameManager.cameraMode == 0)
        {
            transform.position = new Vector3(
                Mathf.Clamp(player.position.x, cameraBounds.min.x, cameraBounds.max.x),
                Mathf.Clamp(player.position.y, cameraBounds.min.y, cameraBounds.max.y), -10);
        }
        else if (GameManager.cameraMode == 1)
        {
            /*transform.position = new Vector3(
                Mathf.Clamp(player.position.x, cameraBounds.min.x, cameraBounds.max.x),
                -15, -10);*/
            transform.position = new Vector3(player.position.x, player.position.y, -10);
        }
        else if (GameManager.cameraMode == 2)
        {
            transform.position = new Vector3(
                player.position.x,
                Mathf.Clamp(player.position.y, 250f, 320f), -10);
        }
        else if (GameManager.cameraMode == 3)
        {
            //ok
        }
        else if (GameManager.cameraMode == 100)
        {
            /*transform.position = new Vector3(
                Mathf.Clamp(player.position.x, cameraBounds.min.x, cameraBounds.max.x),
                Mathf.MoveTowards(transform.position.y, cameraBounds.max.y, 20 * Time.deltaTime), -10);*/
            transform.position = new Vector3(player.position.x, player.position.y, -10);
            SmoothlyChangeSize(GameManager.cameraPlaySize, 0);
        }
        else if (GameManager.cameraMode == 101)
        {
            /*targetResolution = 5;

            nextResolution = Mathf.MoveTowards(cam.orthographicSize, targetResolution, 2 * Time.deltaTime);
            if (nextResolution < targetResolution) nextResolution = targetResolution;
            cam.orthographicSize = nextResolution;*/

            /*nextResolution = (int) Mathf.MoveTowards(pixCam.refResolutionY, targetResolution, 0.1f * Time.deltaTime);
            if (nextResolution < targetResolution) nextResolution = targetResolution;
            pixCam.refResolutionX = nextResolution;
            pixCam.refResolutionY = nextResolution;*/
            //if (nextResolution == targetResolution) GameManager.cameraMode = 1;
            //GameManager.cameraMode = 1;
            transform.position = new Vector3(
                Mathf.Clamp(player.position.x, cameraBounds.min.x, cameraBounds.max.x),
                Mathf.MoveTowards(transform.position.y, -15, 20 * Time.deltaTime), -10);
            SmoothlyChangeSize(5, 1);
        }
        else
        {
            transform.position = new Vector3(player.position.x, player.position.y, -10);
        }
    }


    private float targetResolution, nextResolution;
    private bool targetLarger, sizeChecked;
    void SmoothlyChangeSize(float target, int mode)
    {
        targetResolution = target;
        if (!sizeChecked)
        {
            targetLarger = targetResolution > cam.orthographicSize ? true : false;
            sizeChecked = true;
        }
        
        nextResolution = Mathf.MoveTowards(cam.orthographicSize, targetResolution, 2 * Time.deltaTime);
        if ((nextResolution < targetResolution && !targetLarger)
        || (nextResolution > targetResolution && targetLarger)) nextResolution = targetResolution;
        cam.orthographicSize = nextResolution;
        if (nextResolution == targetResolution)
        {
            sizeChecked = false;
            GameManager.cameraMode = mode;
        }
    }
}
