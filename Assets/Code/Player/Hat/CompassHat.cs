using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassHat : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    GameObject[] arrows;
    public Transform[] arrowRotations;
    public Vector2[] chestPositions;
    ChestOpener[] chests;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetArrows()
    {
        if (GameManager.equippedHat != 3) return;
        if (arrows != null)
        {
            foreach (GameObject arrow in arrows)
            {
                Destroy(arrow);
            }
        }
        chests = Object.FindObjectsOfType<ChestOpener>();
        arrows = new GameObject[chests.Length];
        arrowRotations = new Transform[chests.Length];
        chestPositions = new Vector2[chests.Length];
        for (int i = 0; i < chests.Length; i++)
        {
            chests[i].chestId = i;
            chestPositions[i] = chests[i].GetComponent<Transform>().position;
            arrows[i] = Instantiate(arrowPrefab, gameObject.transform);
            arrowRotations[i] = arrows[i].GetComponent<Transform>();
        }
    }

    public void HideArrow(int id)
    {
        if (arrows == null) return;
        arrows[id].GetComponent<Animator>().Play("AlphaFadeOut");
    }

    public void HideAllArrows()
    {
        if (arrows == null) return;
        foreach (GameObject arrow in arrows)
        {
            if (arrow.GetComponent<SpriteRenderer>().color == Color.white) arrow.GetComponent<Animator>().Play("AlphaFadeOut");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (arrowRotations == null) return;
        for (int i = 0; i < arrowRotations.Length; i++)
        {
            arrowRotations[i].eulerAngles = new Vector3(0, 0, Vector2.SignedAngle(Vector2.right, (Vector3) chestPositions[i] - transform.position));
        }
    }
}
