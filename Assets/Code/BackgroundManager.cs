using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private GameObject bg1, bg2, bg3, bg4;
    [SerializeField] private Sprite iceberg, iceBackground, deepCaveBackground1, deepCaveBackground2, deepCaveBackground3, kelpBackground1, kelpBackground2, kelpBackground3, kelpBackground4;
    private SpriteRenderer bg1s, bg2s, bg3s, bg4s;
    // Start is called before the first frame update
    void Start()
    {
        bg1s = bg1.GetComponent<SpriteRenderer>();
        bg2s = bg2.GetComponent<SpriteRenderer>();
        bg3s = bg3.GetComponent<SpriteRenderer>();
        bg4s = bg4.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeBackground(int mapNum)
    {
        // ice
        if (mapNum == 0)
        {
            bg1.SetActive(true);
            bg2.SetActive(false);
            bg3.SetActive(false);
            bg4.SetActive(true);

            bg1s.sortingOrder = -9;
            bg4s.sortingOrder = -10;

            bg1s.color = new Color(1f, 1f, 1f, 0.45f);
            bg4s.color = new Color(1f, 1f, 1f, 1f);

            bg1s.sprite = iceberg;
            bg4s.sprite = iceBackground;
        }
        // kelp
        else if (mapNum == 1)
        {
            bg1.SetActive(true);
            bg2.SetActive(true);
            bg3.SetActive(true);
            bg4.SetActive(true);

            bg1s.sortingOrder = -10;
            bg2s.sortingOrder = -9;
            bg3s.sortingOrder = -8;
            bg4s.sortingOrder = -7;

            bg1s.color = new Color(1f, 1f, 1f, 1f);
            bg2s.color = new Color(1f, 1f, 1f, 1f);
            bg3s.color = new Color(1f, 1f, 1f, 1f);
            bg4s.color = new Color(1f, 1f, 1f, 0.3f);

            bg1s.sprite = kelpBackground3;
            bg2s.sprite = kelpBackground2;
            bg3s.sprite = kelpBackground1;
            bg4s.sprite = kelpBackground4;
        }
        // seabed
        else if (mapNum == 2)
        {

        }
        // shallow
        else if (mapNum == 3)
        {

        }
        // deep
        else if (mapNum == 4)
        {
            bg1.SetActive(true);
            bg2.SetActive(true);
            bg3.SetActive(true);
            bg4.SetActive(true);

            bg1s.sortingOrder = -10;
            bg2s.sortingOrder = -9;
            bg3s.sortingOrder = -8;
            bg4s.sortingOrder = -7;

            bg1s.color = new Color(0.3f, 0.3f, 0.3f, 1f);
            bg2s.color = new Color(1f, 1f, 1f, 1f);
            bg3s.color = new Color(1f, 1f, 1f, 1f);
            bg4s.color = new Color(0.3f, 0.3f, 0.3f, 0.3f);

            bg1s.sprite = deepCaveBackground3;
            bg2s.sprite = deepCaveBackground2;
            bg3s.sprite = deepCaveBackground1;
            bg4s.sprite = iceBackground;

        }
        // abyss
        else if (mapNum == 5)
        {

        }
    }
}
