using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperLock : MonoBehaviour
{
    public void Open()
    {
        if (transform.parent.gameObject.name == "SubmarineHatch" && GameManager.currentFloor == 2) transform.parent.gameObject.GetComponent<HatchBehaviour>().Open();
        if (gameObject.name == "LevelExitMain") GetComponent<LevelExitBehaviour>().Open();
    }
}
