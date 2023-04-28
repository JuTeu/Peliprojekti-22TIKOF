using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperLock : MonoBehaviour
{
    public void Open()
    {
        if (transform.parent.gameObject.name == "SubmarineHatch") transform.parent.gameObject.GetComponent<HatchBehaviour>().Open();
    }
}
