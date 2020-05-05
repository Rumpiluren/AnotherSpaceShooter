using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput
{
    /* This class handles all input and hands them to the Character raw.
     * The input is further handed over to the player state, which then processess it.
     */

    //Declare inputs
    PlayerControls controls;

    //Declare delegates
    public event Action<bool> ShootEvent;
    public event Action<Vector2> MovementEvent;

    public PlayerInput(GameObject newOwner)
    {
        SetupInputs();
        EnableInputs();
    }

    private void SetupInputs()
    {
        //Assign controls.
        controls = new PlayerControls();
        
        //Assign input to events.
        controls.Gameplay.Shoot.performed += ctx => ShootEvent(true);
        controls.Gameplay.Shoot.canceled += ctx => ShootEvent(false);
        controls.Gameplay.Movement.performed += ctx => MovementEvent(ctx.ReadValue<Vector2>());
        controls.Gameplay.Movement.canceled += ctx => MovementEvent(ctx.ReadValue<Vector2>());
    }

    void EnableInputs()
    {
        //Enable inputs
        controls.Gameplay.Enable();
    }

    public void DisableInputs()
    {

        //Disable inputs
        controls.Gameplay.Disable();
    }
}