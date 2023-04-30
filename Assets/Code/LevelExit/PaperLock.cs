using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperLock : MonoBehaviour
{
    void Start()
    {

    }
    public void Open()
    {
        if (transform.parent.gameObject.name == "SubmarineHatch" && GameManager.currentFloor == 2) 
        {
            transform.parent.gameObject.GetComponent<HatchBehaviour>().Open();
        }
        else if (transform.parent.gameObject.tag == "SpawnedObject")
        {
            LevelExitBehaviour exiter;
            exiter = GetComponent<LevelExitBehaviour>();
            exiter.Open();
        }
    }
}
