using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    private Transform levelExit;
    private Vector3 velocity = Vector3.zero;
    public Bounds cameraBounds;
    private Camera cam;
    private float transitionMenuTop, sequence = 0f;
    private Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.cameraMode == 0)
        {
            transform.position = new Vector3(
                Mathf.Clamp(player.position.x, cameraBounds.min.x, cameraBounds.max.x),
                Mathf.Clamp(player.position.y, cameraBounds.min.y, cameraBounds.max.y), -10);
        }
        else if (GameManager.cameraMode == 1)
        {
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
            transform.position = new Vector3(player.position.x, player.position.y, -10);
            SmoothlyChangeSize(GameManager.cameraPlaySize, 0);
        }
        else if (GameManager.cameraMode == 101)
        {
            transform.position = new Vector3(
                Mathf.Clamp(player.position.x, cameraBounds.min.x, cameraBounds.max.x),
                Mathf.MoveTowards(transform.position.y, -15, 20 * Time.deltaTime), -10);
            SmoothlyChangeSize(5, 1);
        }
        else if (GameManager.cameraMode == 127)
        {
            sequence += 10 * Time.deltaTime;
            transform.position = new Vector3(
                Mathf.MoveTowards(transform.position.x, Mathf.Clamp(player.position.x, cameraBounds.min.x, cameraBounds.max.x), sequence),
                Mathf.MoveTowards(transform.position.y, Mathf.Clamp(player.position.y, cameraBounds.min.y, cameraBounds.max.y), sequence), -10);
            if (Vector2.Distance(transform.position, new Vector2(Mathf.Clamp(player.position.x, cameraBounds.min.x, cameraBounds.max.x), Mathf.Clamp(player.position.y, cameraBounds.min.y, cameraBounds.max.y))) < 0.1f)
            {
                sequence = 0f;
                GameManager.cameraMode = 0;
                GameManager.PauseWorld(false);
                GameObject.Find("Darkness").GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else if (GameManager.cameraMode == 128)
        {
            levelExit = GameObject.FindWithTag("PaperLocked").GetComponent<Transform>();
            sequence = 0f;
            GameObject.Find("Darkness").GetComponent<SpriteRenderer>().enabled = false;
            GameManager.PauseWorld(true);
            GameManager.cameraMode = 129;
        }
        else if (GameManager.cameraMode == 129)
        {
            sequence += 10 * Time.deltaTime;
            transform.position = new Vector3(
                Mathf.MoveTowards(transform.position.x, Mathf.Clamp(levelExit.position.x, cameraBounds.min.x, cameraBounds.max.x), sequence),
                Mathf.MoveTowards(transform.position.y, Mathf.Clamp(levelExit.position.y, cameraBounds.min.y, cameraBounds.max.y), sequence), -10);
            if (Vector2.Distance(transform.position, new Vector2(Mathf.Clamp(levelExit.position.x, cameraBounds.min.x, cameraBounds.max.x), Mathf.Clamp(levelExit.position.y, cameraBounds.min.y, cameraBounds.max.y))) < 0.1f)
            {
                sequence = 0f;
                GameManager.cameraMode = 130;
            }
        }
        else if (GameManager.cameraMode == 130)
        {
            sequence += Time.deltaTime;
            if (sequence > 1 && sequence < 2)
            {
                sequence = 2.1f;
                GameObject.FindWithTag("PaperLocked").GetComponent<PaperLock>().Open();
            }
            if (sequence > 5)
            {
                sequence = 0;
                GameManager.cameraMode = 127;
            }
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
