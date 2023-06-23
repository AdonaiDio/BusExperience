using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine.UI;
using TMPro;

public class CameraScript : MonoBehaviour
{

    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerControls playerControls;

    private InputAction zoomScrollCameraAction;
    private InputAction zoomPinchCameraAction;
    private Coroutine zoomCoroutine;

    public float cameraSpeed = 0.01f;
    public float cameraPinchSpeed = 9f;

    [SerializeField] private float clampZoomIn;
    [SerializeField] private float clampZoomOut;

    //DEBUG UI
    [SerializeField] private TextMeshProUGUI txtTouch1Pos;
    [SerializeField] private TextMeshProUGUI txtTouch2Pos;
    [SerializeField] private TextMeshProUGUI txtTouch2Contact;

    private void Awake()
    {
        playerControls = new PlayerControls();
        zoomScrollCameraAction = playerInput.actions["Zoom"];
        //zoomPinchCameraAction = playerInput.actions["SecondaryTouchContact"];
    }
    private void Start()
    {
        Debug.Log(zoomScrollCameraAction);
        Debug.Log(zoomPinchCameraAction);
        playerControls.Gaming.SecondaryTouchContact.started += _ => ZoomPinchStart();
        playerControls.Gaming.SecondaryTouchContact.canceled += _ => ZoomPinchEnd();
    }

    private void OnEnable()
    {
        zoomScrollCameraAction.performed += CameraZoom;
        //zoomPinchCameraAction.started += _ => ZoomPinchStart();
        //zoomPinchCameraAction.canceled += _ => ZoomPinchEnd();
        playerControls.Enable();
    }


    private void OnDisable()
    {
        zoomScrollCameraAction.performed -= CameraZoom;
        //zoomPinchCameraAction.started -= _ => ZoomPinchStart();
        //zoomPinchCameraAction.canceled -= _ => ZoomPinchEnd();
        playerControls.Disable();
    }

    private void Update()
    {
    }
    private void ZoomPinchStart()
    {
        Debug.Log("touch_2 contact: true");
        txtTouch2Contact.text = "touch_2 contact: true";
        zoomCoroutine = StartCoroutine(ZoomPinchDetection());
    }
    private void ZoomPinchEnd()
    {
        Debug.Log("touch_2 contact: false");
        txtTouch2Contact.text = "touch_2 contact: false";
        StopCoroutine(zoomCoroutine);
    }
    IEnumerator ZoomPinchDetection()
    {
        float prevDistance = 0f;
        float currDistance = 0f;
        while (true)
        {
            txtTouch1Pos.text = "touch_1Pos: " + playerInput.actions["PrimaryFingerPosition"].ReadValue<Vector2>();
            txtTouch1Pos.text = "touch_1Pos: " + playerInput.actions["SecondaryFingerPosition"].ReadValue<Vector2>();
            currDistance = Vector2.Distance(playerInput.actions["PrimaryFingerPosition"].ReadValue<Vector2>(),
                playerInput.actions["SecondaryFingerPosition"].ReadValue<Vector2>());
            if (currDistance > prevDistance) //Zoom Out
            {
                Vector3 nextPosition = transform.position;
                nextPosition.y += 1;
                transform.position = Vector3.Slerp(transform.position, 
                                                   nextPosition,
                                                   Time.deltaTime * cameraPinchSpeed);
            }else if (currDistance < prevDistance) //Zoom In
            {
                Vector3 nextPosition = transform.position;
                nextPosition.y -= 1;
                transform.position = Vector3.Slerp(transform.position,
                                                   nextPosition,
                                                   Time.deltaTime * cameraPinchSpeed);
            }
            prevDistance = currDistance;
            yield return null;
        }
    }

    private void CameraZoom(InputAction.CallbackContext context)
    {
        this.transform.position = new Vector3(transform.position.x,
                                            Mathf.Clamp(transform.position.y + context.ReadValue<Vector2>().y * cameraSpeed *-1, clampZoomIn, clampZoomOut),
                                            transform.position.z);
    }

}
