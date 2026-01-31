using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }
    public bool Player1LeftInput { get; private set; }
    public bool Player2LeftInput { get; private set; }
    public bool Player1RightInput { get; private set; }
    public bool Player2RightInput { get; private set; }
    public bool Player1SlideInput { get; private set; }
    public bool Player2SlideInput { get; private set; }
    public event Action Player1JumpInput;
    public event Action Player2JumpInput;
    public event Action Player1ChooseMask1Input;
    public event Action Player2ChooseMask1Input;
    public event Action Player1ChooseMask2Input;
    public event Action Player2ChooseMask2Input;
    public event Action Player1ChooseMask3Input;
    public event Action Player2ChooseMask3Input;
    public event Action Player1PrevMaskInput;
    public event Action Player2PrevMaskInput;
    public event Action Player1NextMaskInput;
    public event Action Player2NextMaskInput;
    public event Action Player1UseInput;
    public event Action Player2UseInput;
    public event Action PauseInput;
    private PlayerInputActions inputActions;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        inputActions = new PlayerInputActions();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();

        RegisterInputAction(inputActions.Player.LeftPlayer1, OnPlayer1LeftInput);
        RegisterInputAction(inputActions.Player.LeftPlayer2, OnPlayer2LeftInput);
        RegisterInputAction(inputActions.Player.RightPlayer1, OnPlayer1RightInput);
        RegisterInputAction(inputActions.Player.RightPlayer2, OnPlayer2RightInput);
        RegisterInputAction(inputActions.Player.JumpPlayer1, OnPlayer1JumpInput);
        RegisterInputAction(inputActions.Player.JumpPlayer2, OnPlayer2JumpInput);
        RegisterInputAction(inputActions.Player.SlidePlayer1, OnPlayer1SlideInput);
        RegisterInputAction(inputActions.Player.SlidePlayer2, OnPlayer2SlideInput);
        RegisterInputAction(inputActions.Player.RightPlayer1, OnPlayer1RightInput);
        RegisterInputAction(inputActions.Player.Mask1Player1, OnPlayer1ChooseMask1Input);
        RegisterInputAction(inputActions.Player.Mask1Player2, OnPlayer2ChooseMask1Input);
        RegisterInputAction(inputActions.Player.Mask2Player1, OnPlayer1ChooseMask2Input);
        RegisterInputAction(inputActions.Player.Mask2Player2, OnPlayer2ChooseMask2Input);
        RegisterInputAction(inputActions.Player.Mask3Player1, OnPlayer1ChooseMask3Input);
        RegisterInputAction(inputActions.Player.Mask3Player2, OnPlayer2ChooseMask3Input);
        RegisterInputAction(inputActions.Player.PrevMaskPlayer1, OnPlayer1PrevMaskInput);
        RegisterInputAction(inputActions.Player.PrevMaskPlayer2, OnPlayer2PrevMaskInput);
        RegisterInputAction(inputActions.Player.NextMaskPlayer1, OnPlayer1NextMaskInput);
        RegisterInputAction(inputActions.Player.NextMaskPlayer2, OnPlayer2NextMaskInput);
        RegisterInputAction(inputActions.Player.UseMaskPlayer1, OnPlayer1UseInput);
        RegisterInputAction(inputActions.Player.UseMaskPlayer2, OnPlayer2UseInput);
        RegisterInputAction(inputActions.Player.Pause, OnPauseInput);
    }


    private void OnDisable()
    {
        UnregisterInputAction(inputActions.Player.LeftPlayer1, OnPlayer1LeftInput);
        UnregisterInputAction(inputActions.Player.LeftPlayer2, OnPlayer2LeftInput);
        UnregisterInputAction(inputActions.Player.RightPlayer1, OnPlayer1RightInput);
        UnregisterInputAction(inputActions.Player.RightPlayer2, OnPlayer2RightInput);
        UnregisterInputAction(inputActions.Player.JumpPlayer1, OnPlayer1JumpInput);
        UnregisterInputAction(inputActions.Player.JumpPlayer2, OnPlayer2JumpInput);
        UnregisterInputAction(inputActions.Player.SlidePlayer1, OnPlayer1SlideInput);
        UnregisterInputAction(inputActions.Player.SlidePlayer2, OnPlayer2SlideInput);
        UnregisterInputAction(inputActions.Player.RightPlayer1, OnPlayer1RightInput);
        UnregisterInputAction(inputActions.Player.Mask1Player1, OnPlayer1ChooseMask1Input);
        UnregisterInputAction(inputActions.Player.Mask1Player2, OnPlayer2ChooseMask1Input);
        UnregisterInputAction(inputActions.Player.Mask2Player1, OnPlayer1ChooseMask2Input);
        UnregisterInputAction(inputActions.Player.Mask2Player2, OnPlayer2ChooseMask2Input);
        UnregisterInputAction(inputActions.Player.Mask3Player1, OnPlayer1ChooseMask3Input);
        UnregisterInputAction(inputActions.Player.Mask3Player2, OnPlayer2ChooseMask3Input);
        UnregisterInputAction(inputActions.Player.PrevMaskPlayer1, OnPlayer1PrevMaskInput);
        UnregisterInputAction(inputActions.Player.PrevMaskPlayer2, OnPlayer2PrevMaskInput);
        UnregisterInputAction(inputActions.Player.NextMaskPlayer1, OnPlayer1NextMaskInput);
        UnregisterInputAction(inputActions.Player.NextMaskPlayer2, OnPlayer2NextMaskInput);
        UnregisterInputAction(inputActions.Player.UseMaskPlayer1, OnPlayer1UseInput);
        UnregisterInputAction(inputActions.Player.UseMaskPlayer2, OnPlayer2UseInput);
        UnregisterInputAction(inputActions.Player.Pause, OnPauseInput);

        inputActions.Player.Disable();
    }


    private void RegisterInputAction(InputAction action, Action<InputAction.CallbackContext> callback)
    {
        action.performed += callback;
        action.canceled += callback;
    }

    private void UnregisterInputAction(InputAction action, Action<InputAction.CallbackContext> callback)
    {
        action.performed -= callback;
        action.canceled -= callback;
    }

    private void OnPlayer1LeftInput(InputAction.CallbackContext context)
    {
        Player1LeftInput = context.ReadValue<float>() > 0f;
    }

    private void OnPlayer2LeftInput(InputAction.CallbackContext context)
    {
        Player2LeftInput = context.ReadValue<float>() > 0f;
    }

    private void OnPlayer1RightInput(InputAction.CallbackContext context)
    {
        Player1RightInput = context.ReadValue<float>() > 0f;
    }
    private void OnPlayer2RightInput(InputAction.CallbackContext context)
    {
        Player2RightInput = context.ReadValue<float>() > 0f;
    }
    private void OnPlayer1SlideInput(InputAction.CallbackContext context)
    {
        Player1SlideInput = context.ReadValue<float>() > 0f;
    }
    private void OnPlayer2SlideInput(InputAction.CallbackContext context)
    {
        Player2SlideInput = context.ReadValue<float>() > 0f;
    }
    private void OnPlayer1JumpInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Player1JumpInput?.Invoke();
        }
    }
    private void OnPlayer2JumpInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Player2JumpInput?.Invoke();
        }
    }
    private void OnPlayer1ChooseMask1Input(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Player1ChooseMask1Input?.Invoke();
        }
    }
    private void OnPlayer2ChooseMask1Input(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Player2ChooseMask1Input?.Invoke();
        }
    }
    private void OnPlayer1ChooseMask2Input(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Player1ChooseMask2Input?.Invoke();
        }
    }
    private void OnPlayer2ChooseMask2Input(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Player2ChooseMask2Input?.Invoke();
        }
    }
    private void OnPlayer1ChooseMask3Input(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Player1ChooseMask3Input?.Invoke();
        }
    }
    private void OnPlayer2ChooseMask3Input(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Player2ChooseMask3Input?.Invoke();
        }
    }
    private void OnPlayer1PrevMaskInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Player1PrevMaskInput?.Invoke();
        }
    }
    private void OnPlayer2PrevMaskInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Player2PrevMaskInput?.Invoke();
        }
    }
    private void OnPlayer1NextMaskInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Player1NextMaskInput?.Invoke();
        }
    }
    private void OnPlayer2NextMaskInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Player2NextMaskInput?.Invoke();
        }
    }
    private void OnPlayer1UseInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Player1UseInput?.Invoke();
        }
    }
    private void OnPlayer2UseInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Player2UseInput?.Invoke();
        }
    }
    private void OnPauseInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PauseInput?.Invoke();
        }
    }

}