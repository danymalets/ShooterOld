using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class GameInput : MonoBehaviour
{
    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }
    public bool Jump { get; private set; }
    public bool Shoot { get; private set; }

    private bool _cancelJump;
    private bool _cancelShoot;

    public void OnMove(InputAction.CallbackContext context)
    {
        Move = context.ReadValue<Vector2>();
    }
    
    public void OnLook(InputAction.CallbackContext context)
    {
        Look = context.ReadValue<Vector2>();
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _cancelJump = false;
            Jump = true;
        }
        else if (context.canceled)
        {
            _cancelJump = true;
        }
    }
    
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _cancelShoot = false;
            Shoot = true;
        }
        else if (context.canceled)
        {
            _cancelShoot = true;
        }
    }

    private void LateUpdate()
    {
        if (_cancelJump)
            Jump = false;
        if (_cancelShoot)
            Shoot = false;
    }
}
