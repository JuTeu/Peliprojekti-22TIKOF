using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeabedExitControl : MonoBehaviour
{
    public CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GameObject.FindWithTag("Player").GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(characterController.boostMode == true)
        {
            GetComponent<Collider2D>().isTrigger = true;
        }
    }
}
