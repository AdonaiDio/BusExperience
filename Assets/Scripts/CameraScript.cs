using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class CameraScript : MonoBehaviour
{

    [SerializeField] private PlayerInput playerInput; //PlayerInput do InputManager que cuida do InputSystem

    private InputAction zoomScrollAction;

    private InputAction touch0Contact;
    private InputAction touch1Contact;
    private InputAction touch0Position;
    private InputAction touch1Position;

    private int touchCount = 0;
    private float prevDistance = 0;

    public float cameraSpeed = 0.01f;

    [SerializeField] private float clampZoomIn;
    [SerializeField] private float clampZoomOut;

    private void Awake()
    {
        //Mouse Scroll
        zoomScrollAction = playerInput.actions["Zoom"];

        //Touchscreen Pinch
        touch0Contact = playerInput.actions["Touch0Contact"];
        touch0Position = playerInput.actions["Touch0Position"];

        touch1Contact = playerInput.actions["Touch1Contact"];
        touch1Position = playerInput.actions["Touch1Position"];
    }
    private void Start()
    {
        ScrollInput();
        PinchInput();
    }

    private void OnEnable()
    {
        zoomScrollAction.Enable();
        touch0Contact.Enable();
        touch0Position.Enable();
        touch1Contact.Enable();
        touch1Position.Enable();
    }
    private void OnDisable()
    {
        zoomScrollAction.Disable();
        touch0Contact.Disable();
        touch0Position.Disable();
        touch1Contact.Disable();
        touch1Position.Disable();
    }
    private void ScrollInput()
    {
        zoomScrollAction.performed += ctx => CameraZoom(-ctx.ReadValue<Vector2>().y);
    }
    private void PinchInput()
    {
        touch0Contact.performed += _ => touchCount++;
        touch0Contact.canceled += _ => { touchCount--; prevDistance = 0; };

        touch1Contact.performed += _ => touchCount++;
        touch1Contact.canceled += _ => { touchCount--; prevDistance = 0; };

        touch1Position.performed += _ =>
        {
            if (touchCount < 2)
                return;
            float distance = (touch0Position.ReadValue<Vector2>() - touch1Position.ReadValue<Vector2>()).magnitude;
            if (prevDistance == 0)
                prevDistance = distance;
            float difference = distance - prevDistance;
            prevDistance = distance;
            CameraZoom(-difference);
        };
    }
    private void CameraZoom(float increment)
    {
        this.transform.position = new Vector3(transform.position.x,
                                            Mathf.Clamp(transform.position.y + increment * cameraSpeed, clampZoomIn, clampZoomOut),
                                            transform.position.z);
    }
}
