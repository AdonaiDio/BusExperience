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
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    private void Update()
    {
    }

    public void SwitchActionMapUIGaming(string actionMapName)
    {
        playerInput.SwitchCurrentActionMap(actionMapName);
    }
}
