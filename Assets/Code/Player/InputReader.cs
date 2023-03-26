using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private Vector2 mousePosition;
    private bool leftMouseInput;
    private bool kKey;
    
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
    public void OnKKey(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            kKey = true;
        }
        else if (context.canceled)
        {
            kKey = false;
        }
    }
    public Vector2 GetMousePosition() {return mousePosition;}
    public bool GetLeftMouse() {return leftMouseInput;}
    public bool GetK() {return kKey;}
}
