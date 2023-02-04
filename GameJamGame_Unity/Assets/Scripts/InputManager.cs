using UnityEngine;
using UnityEngine.InputSystem;

public delegate void OnInputActionTriggeredDelegate(InputActionType actionType, bool bIsClicked);

public enum InputActionType
{
    None,
    LMC,
    RMC,
    MMC,
    Escape,
    Action
}

public class InputManager : MonoBehaviour
{
    public OnInputActionTriggeredDelegate OnInputTriggered;

    public Vector2 movementVector { get;private set; }
    public Vector2 mousePosition { get; private set; }

    private void OnMove(InputValue value)
    {
        movementVector = value.Get<Vector2>();
    }

    private void OnMousePosition(InputValue value)
    {
        mousePosition = value.Get<Vector2>();
    }    

    private void OnLMC(InputValue value)
    {
        OnInputTriggered?.Invoke(InputActionType.LMC, value.isPressed);
    }

    private void OnRMC(InputValue value)
    {
        OnInputTriggered?.Invoke(InputActionType.RMC, value.isPressed);
    }

    private void OnMMC(InputValue value)
    {
        OnInputTriggered?.Invoke(InputActionType.MMC, value.isPressed);
    }

    private void OnEscape(InputValue value)
    {
        OnInputTriggered?.Invoke(InputActionType.Escape, value.isPressed);
    }

    private void OnAction(InputValue value)
    {
        OnInputTriggered?.Invoke(InputActionType.Action, value.isPressed);
        Helper.InternalDebugLog("Called Action");
    }
}