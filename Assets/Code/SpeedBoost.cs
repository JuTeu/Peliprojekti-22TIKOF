using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public float boostAmount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            /*CharacterController characterController = other.GetComponent<CharacterController>();
            characterController.SetSpeed(characterController.speed + boostAmount);*/

            GameManager.flippersUnlocked = true;
            GameManager.flippersEquipped = true;
        }
    }
}