using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private Vector2 mousePosition;
    private bool leftMouseInput;

    public void MousePosition(InputAction.CallbackContext context)
    {
        mousePosition = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }
    public void OnLeftMouse(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            leftMouseInput = true;
        }
        else if (context.canceled)
        {
            leftMouseInput = false;
        }
    }
    public Vector2 GetMousePosition() {return mousePosition;}
    public bool GetLeftMouse() {return leftMouseInput;}
}
