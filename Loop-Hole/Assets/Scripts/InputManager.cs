using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{

    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event StartTouch OnEndTouch;

    public PlayerInput playerInput;
    private Camera mainCamera;

    private Vector2 startPosition;
    private Vector2 endPosition;

    bool touching = false;

    public Vector2 StartPosition
    {
        get
        {
            return startPosition;
        }
    }

    public Vector2 EndPosition
    {
        get
        {
            return endPosition;
        }
    }

    public bool Touching
    {
        get
        {
            return touching;
        }
    }

    private void Awake()
    {
        playerInput = new PlayerInput();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    void Start()
    {
        playerInput.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        playerInput.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        touching = true;
        startPosition = Utils.ScreenToWorld(mainCamera, playerInput.Touch.PrimaryPosition.ReadValue<Vector2>());
        //Debug.Log("start:" + Utils.ScreenToWorld(mainCamera, playerInput.Touch.PrimaryPosition.ReadValue<Vector2>()));
        //if (OnStartTouch != null) OnStartTouch(Utils.ScreenToWorld(mainCamera, playerInput.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        endPosition = PrimaryPosition();
        touching = false;
        //Debug.Log("end:" +  Utils.ScreenToWorld(mainCamera, playerInput.Touch.PrimaryPosition.ReadValue<Vector2>()));
        //if (OnEndTouch != null) OnEndTouch(Utils.ScreenToWorld(mainCamera, playerInput.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
    }

    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(mainCamera, playerInput.Touch.PrimaryPosition.ReadValue<Vector2>());
    }

}
