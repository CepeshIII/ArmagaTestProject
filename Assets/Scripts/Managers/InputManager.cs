using System;
using UnityEngine.InputSystem;
using UnityEngine;


public class InputManager : Singleton<InputManager>
{
    public static InputSystem_Actions actions;

    // Board events
    public event Action<Vector3> OnBoardClick;
    public event Action<Vector2> OnBoardMouseMove;

    public static event Action<InputActionMap> OnActionMapChanged;

    // Gameplay events
    public event Action<Vector3> OnGameplayClick;
    public event Action<Vector2> OnGameplayMouseMove;


    public Vector2 GetMousePosition() => actions.GlobalActions.MousePosition.ReadValue<Vector2>();

    new public void Awake()
    {
        base.Awake();
        actions = new InputSystem_Actions();

        // BoardManageMode bindings
        actions.BoardManageMode.LeftMouseClick.started += _ => HandleBoardClick();
        actions.BoardManageMode.MousePosition.performed += ctx => OnBoardMouseMove?.Invoke(ctx.ReadValue<Vector2>());

        // GamePlayMode bindings
        actions.GameMode.LeftMouseClick.started += _ => HandleGameplayClick();
        actions.GameMode.MousePosition.performed += ctx => OnGameplayMouseMove?.Invoke(ctx.ReadValue<Vector2>());
    }


    private void Start()
    {
        // Enable the default action map for data which shared between modes, for example mouse position
        actions.GlobalActions.Enable();
        ToggleActionMap(actions.BoardManageMode);
    }


    private void HandleBoardClick()
    {
        Vector3 worldPos = GetWorldMousePosition();
        OnBoardClick?.Invoke(worldPos);
    }


    private void HandleGameplayClick()
    {
        Vector3 worldPos = GetWorldMousePosition();
        OnGameplayClick?.Invoke(worldPos);
    }


    private Vector3 GetWorldMousePosition()
    {
        Vector2 mousePos = actions.GlobalActions.MousePosition.ReadValue<Vector2>();
        return Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane + 1f));
    }


    public static void ToggleActionMap(InputActionMap newActionMap)
    {
        if(newActionMap.enabled)
            return;

        actions.Disable();
        OnActionMapChanged?.Invoke(newActionMap);
        newActionMap.Enable();
        
        // Enable the default action map for data which shared between modes, for example mouse position
        actions.GlobalActions.Enable();

    }

}

