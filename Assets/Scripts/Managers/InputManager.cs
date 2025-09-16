using System;
using UnityEngine.InputSystem;
using UnityEngine;


public class InputManager : Singleton<InputManager>
{
    public static InputSystem_Actions actions;

    // Board events
    public event Action<Vector3> OnBoardClicked_Start;
    public event Action<Vector3> OnBoardClicked_End;
    public event Action<Vector3> OnBoardClicked_Performed;

    public event Action<Vector2> OnBoardMouseMove;

    public static event Action<InputActionMap> OnActionMapChanged;

    // Gameplay events
    public event Action<Vector3> OnGameplayClicked;
    public event Action<Vector2> OnGameplayMouseMove;


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
        // Enable the default action map for data which shared between modes, for example mouse position
        actions.GlobalActions.Enable();
        ToggleActionMap(actions.BoardManageMode);
    }


    private void LeftMouseClick_Start(InputAction.CallbackContext ctx)
    {
        OnBoardClicked_Start?.Invoke(GetWorldMousePosition());
    }


    private void LeftMouseClick_Performed(InputAction.CallbackContext ctx)
    {
        OnBoardClicked_Performed?.Invoke(GetWorldMousePosition());

    }


    private void LeftMouseClick_End(InputAction.CallbackContext ctx)
    {
        OnBoardClicked_End?.Invoke(GetWorldMousePosition());

    }


    private void MouseMove(InputAction.CallbackContext ctx)
    {
        OnBoardMouseMove?.Invoke(ctx.ReadValue<Vector2>());
    }


    private void HandleGameplayClick(InputAction.CallbackContext ctx)
    {
        Vector3 worldPos = GetWorldMousePosition();
        OnGameplayClicked?.Invoke(worldPos);
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


    private void OnDisable()
    {
        actions.BoardManageMode.LeftMouseClick.started -= LeftMouseClick_Start;
        actions.BoardManageMode.LeftMouseClick.performed -= LeftMouseClick_Performed;
        actions.BoardManageMode.LeftMouseClick.canceled -= LeftMouseClick_End;

        actions.BoardManageMode.MousePosition.performed -= MouseMove;
    }

}

