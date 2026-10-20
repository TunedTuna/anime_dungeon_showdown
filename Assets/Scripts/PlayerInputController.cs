using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// this script handles all input keys (except movement)
/// -default keys-
/// Special 1: Q
/// SPecial 2: R
/// SPecial 3: T
/// Ult? : TBA
/// Swap Characters: F
/// interact: E
/// 
/// </summary>
public class PlayerInputController : MonoBehaviour
{
    public static PlayerInputController instance {  get; private set; }
    private InputSystem_Actions inputActions;
    public event EventHandler OnBasicAttack;
    public event EventHandler OnSpecialOne;
    public event EventHandler OnSpecialTwo;
    public event EventHandler OnSpecialThree;
    public event EventHandler OnInteract;
    public event EventHandler OnCharactersSwapped;

    private void Awake()
    {
        instance = this;
         inputActions= new InputSystem_Actions();
        inputActions.Player.Enable();
        //combat
        inputActions.Player.Attack.performed += Attack_performed;
        inputActions.Player.SpecialOne.performed += SpecialOne_performed;
        inputActions.Player.SpecialTwo.performed += SpecialTwo_performed;
        inputActions.Player.SpecialThree.performed += SpecialThree_performed;
        //other
        inputActions.Player.Interact.performed += Interact_performed;
        inputActions.Player.SwapCharacters.performed += SwapCharacters_performed;
        //pause?
        //close?
        //why am i doubling the events
    }

    private void SwapCharacters_performed(InputAction.CallbackContext obj)
    {
        OnCharactersSwapped?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke(this, EventArgs.Empty);
    }

    private void Attack_performed(InputAction.CallbackContext obj)
    {
        OnBasicAttack?.Invoke(this, EventArgs.Empty);
    }

    private void SpecialThree_performed(InputAction.CallbackContext obj)
    {
        OnSpecialThree?.Invoke(this, EventArgs.Empty);
    }

    private void SpecialTwo_performed(InputAction.CallbackContext obj)
    {
        OnSpecialTwo?.Invoke(this, EventArgs.Empty);
    }

    private void SpecialOne_performed(InputAction.CallbackContext obj)
    {
        OnSpecialOne?.Invoke(this, EventArgs.Empty);
    }

    private void OnDisable()
    {
        inputActions.Player.Attack.performed -= Attack_performed;
        inputActions.Player.SpecialOne.performed -= SpecialOne_performed;
        inputActions.Player.SpecialTwo.performed -= SpecialTwo_performed;
        inputActions.Player.SpecialThree.performed -= SpecialThree_performed;
        //other
        inputActions.Player.Interact.performed -= Interact_performed;
    }


}
