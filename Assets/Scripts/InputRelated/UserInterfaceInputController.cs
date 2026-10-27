using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UserInterfaceInputController : MonoBehaviour
{
    public static UserInterfaceInputController Instance { get; private set; }
    private InputSystem_Actions inputActions;
    InputSystem_Actions.UIActions inputActUI;
    public event EventHandler OnCancel;
    public event EventHandler OnCloseAll;

    private void Awake()
    {
        Instance = this;
        inputActions = new InputSystem_Actions();
         inputActUI = inputActions.UI;


    }
    private void OnEnable()
    {
        inputActUI.Enable();
        inputActUI.Cancel.performed += Cancel_performed;
        inputActUI.CloseAll.performed += CloseAll_performed;
    }

    private void CloseAll_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Inside UI COntroller. fireing close all");
        OnCloseAll?.Invoke(this, EventArgs.Empty);
    }

    private void Cancel_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnCancel?.Invoke(this, EventArgs.Empty);
    }
    private void OnDisable()
    {
        inputActUI.Disable();// watch this come bac later
        inputActUI.Cancel.performed -= Cancel_performed;
        inputActUI.CloseAll.performed -= CloseAll_performed;
    }

}
