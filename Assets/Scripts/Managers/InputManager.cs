using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;

    private InputAction pressAction;
    private InputAction positionAction;

    [SerializeField] private Camera cam;

    [SerializeField] private GameObject cube;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        pressAction = playerInput.actions["Press"];
        positionAction = playerInput.actions["Position"];
    }

    private void OnEnable()
    {
        pressAction.performed += TouchPressed;
    }

    private void OnDisable()
    {
        pressAction.performed -= TouchPressed;
    }

    private void Start()
    {
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
        Debug.Log(value);

        Vector3 position = cam.ScreenToWorldPoint(new Vector3(
                                                        positionAction.ReadValue<Vector2>().x,
                                                        positionAction.ReadValue<Vector2>().y,
                                                        10));
        cube.transform.position = position;

        Debug.Log("vec2 > " + positionAction.ReadValue<Vector2>());
        Debug.Log("vec3 > " + position);
    }

}
