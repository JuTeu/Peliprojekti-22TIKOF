using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgro : MonoBehaviour
{
    [field: SerializeField]
    public bool PlayerInArea { get; private set; }
    public Transform Player { get; private set; }
    [SerializeField] private string detectionTag = "Player";

    [SerializeField] private float speed = 1f;
    [SerializeField] private GameObject target;

    public float delta = 1.5f;  // Amount to move left and right from the start point
    public float idleSpeed = 2.0f;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerInArea)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        else
        {
            Vector3 v = startPos;
            v.x += delta * Mathf.Sin(Time.time * idleSpeed);
            transform.position = v;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag))
        {
            PlayerInArea = true;
            Player = collision.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag))
        {
            PlayerInArea = false;
            Player = null;
        }
    }
}
