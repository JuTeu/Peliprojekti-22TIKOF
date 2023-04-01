using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableChild : MonoBehaviour
{
    [SerializeField] GameObject child;
    public void Enable(bool toggle)
    {
        child.SetActive(toggle);
    }
}
