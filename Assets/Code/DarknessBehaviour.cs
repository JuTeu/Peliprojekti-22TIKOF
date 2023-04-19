using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject darkness, lightMask;
    float speed, target, sequence, exponentialSequence;
    public bool startSequence = false;
    GameObject player;
    Camera mainCamera;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        mainCamera = Camera.main;
        float darknessHeight = 50f;
        float darknessWidth = darknessHeight * mainCamera.aspect;
        darkness.transform.localScale = new Vector3(darknessWidth, darknessHeight, 1);
        LightSize(0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        if (startSequence) ChangeLightSize();
    }

    public void ChangeLight(float initialSize, float targetSize, float changeSpeed)
    {
        if (initialSize >= 0) LightSize(initialSize);
        speed = changeSpeed;
        target = targetSize;
        sequence = 0f;
        exponentialSequence = 0f;
        startSequence = true;
    }

    void LightSize(float size)
    {
        lightMask.transform.localScale = new Vector3(size, size, 1);
    }

    void ChangeLightSize()
    {
        sequence += speed * Time.deltaTime;
        if (exponentialSequence < 10000f)
        {
            exponentialSequence = Mathf.Pow(2, sequence);
            LightSize(Mathf.MoveTowards(lightMask.transform.localScale.x, target, (exponentialSequence / 1000)));

            if (exponentialSequence > 10000f)
            {
                LightSize(target);
                startSequence = false;
            }
        }
    }
}
