using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRefresher : MonoBehaviour
{
    public void Refresh()
    {
        if (transform.childCount == 3)
        {
            GetComponent<RefrigeratorBehaviour>().Refresh();
        }
        else if (transform.childCount == 4)
        {
            GetComponent<ChestOpener>().Refresh();
        }
    }
}
