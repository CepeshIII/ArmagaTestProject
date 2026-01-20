using System;
using UnityEngine.InputSystem;
using UnityEngine;


public class InputManager : Singleton<InputManager>
{
    public static InputSystem_Actions actions;

    // BoardCells events
    public event Action<Vector3> BoardClicked_Started;
    public event Action<Vector3> BoardClicked_Ended;
    public event Action<Vector3> BoardClicked_Performed;
    public event Action<Vector2> BoardMouseMoved;

    public static event Action<InputActionMap> ActionMapChanged;

    // Gameplay events
    public event Action<Vector3> GameplayClicked;
    public event Action<Vector2> GameplayMouseMoved;


    public Vector2 GetMousePosition() => actions.GlobalActions.MousePosition.ReadValue<Vector2>();


    new public void Awake()
    {
        base.Awake();
        actions = new InputSystem_Actions();
    }


    private void OnEnable()
    {
        actions.BoardManageMode.LeftMouseClick.started += LeftMouseClick_Start;
        actions.BoardManageMode.LeftMouseClick.performed += LeftMouseClick_Performed;
        actions.BoardManageMode.LeftMouseClick.canceled += LeftMouseClick_End;
        
        actions.BoardManageMode.MousePosition.performed += MouseMove;
    }


    private void Start()
    {
        // Enable the default action map for data which shared between modes, for example mouse coordinate
        actions.GlobalActions.Enable();
        ToggleActionMap(actions.BoardManageMode);
    }


    public void ToBoardMode()
    {
        ToggleActionMap(actions.BoardManageMode);
    }


    public void ToGameMode()
    {
        ToggleActionMap(actions.GameMode);
    }


    public void ToIdle()
    {
        DisableAllActionMaps();
    }


    private void LeftMouseClick_Start(InputAction.CallbackContext ctx)
    {
        BoardClicked_Started?.Invoke(GetWorldMousePosition());
    }


    private void LeftMouseClick_Performed(InputAction.CallbackContext ctx)
    {
        BoardClicked_Performed?.Invoke(GetWorldMousePosition());

    }


    private void LeftMouseClick_End(InputAction.CallbackContext ctx)
    {
        BoardClicked_Ended?.Invoke(GetWorldMousePosition());

    }


    private void MouseMove(InputAction.CallbackContext ctx)
    {
        BoardMouseMoved?.Invoke(ctx.ReadValue<Vector2>());
    }


    private void HandleGameplayClick(InputAction.CallbackContext ctx)
    {
        Vector3 worldPos = GetWorldMousePosition();
        GameplayClicked?.Invoke(worldPos);
    }


    private Vector3 GetWorldMousePosition()
    {
        Vector2 mousePos = actions.GlobalActions.MousePosition.ReadValue<Vector2>();
        return Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane + 1f));
    }


    private static void ToggleActionMap(InputActionMap newActionMap)
    {
        if(newActionMap.enabled)
            return;

        actions.Disable();
        ActionMapChanged?.Invoke(newActionMap);
        newActionMap.Enable();
        
        // Enable the default action map for data which shared between modes, for example mouse coordinate
        actions.GlobalActions.Enable();

    }


    private void DisableAllActionMaps()
    {
        actions.Disable();
        ActionMapChanged?.Invoke(null);
        actions.GlobalActions.Disable();
    }


    private void OnDisable()
    {
        if (actions == null) return;

        actions.Disable();

        actions.BoardManageMode.LeftMouseClick.started -= LeftMouseClick_Start;
        actions.BoardManageMode.LeftMouseClick.performed -= LeftMouseClick_Performed;
        actions.BoardManageMode.LeftMouseClick.canceled -= LeftMouseClick_End;

        actions.BoardManageMode.MousePosition.performed -= MouseMove;
    }

}

