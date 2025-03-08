using Assets.Scripts.Weapons;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


[CreateAssetMenu(menuName = "InputReader")]
public class InputReader : ScriptableObject, GameInput.ICharacterActions, GameInput.IMovementActions, GameInput.IShooterActions
{
    private GameInput _gameInput;

    public event UnityAction Ability1Event;
    public event UnityAction Ability2Event;
    public event UnityAction Ability3Event;

    public event UnityAction JumpEvent;
    public event UnityAction<Vector2> MoveEvent;
    public event UnityAction<Vector2> RotateEvent;

    public event UnityAction FireEvent;
    public event UnityAction FireCanceledEvent;
    public event UnityAction AltFireEvent;
    public event UnityAction ReloadEvent;
    public event UnityAction<WeaponTypes> ChangeWeaponEvent;

    private void OnEnable()
    {
        if (_gameInput == null)
        {
            _gameInput = new GameInput();

            _gameInput.Movement.Enable();
            _gameInput.Shooter.Enable();
            _gameInput.Character.Enable();

            _gameInput.Movement.SetCallbacks(this);
            _gameInput.Shooter.SetCallbacks(this);
            _gameInput.Character.SetCallbacks(this);
        }
    }

    private void OnDisable()
    {
        if (_gameInput != null)
        {
            _gameInput.Movement.Disable();
            _gameInput.Shooter.Disable();
            _gameInput.Character.Disable();
        }
    }

    // Player Actions
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("OnMove performed");
            MoveEvent?.Invoke(context.ReadValue<Vector2>());
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            Debug.Log("OnMove canceled");
            MoveEvent?.Invoke(context.ReadValue<Vector2>());
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            JumpEvent?.Invoke();
        }
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            RotateEvent?.Invoke(context.ReadValue<Vector2>());
        }
    }

    // Shooter Actions
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            FireEvent?.Invoke();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            FireCanceledEvent?.Invoke();
        }
    }

    public void OnAltFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            AltFireEvent?.Invoke();
        }
    }

    public void OnTakeMain(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("OnTakeMain");
            ChangeWeaponEvent?.Invoke(WeaponTypes.MainWeapon);
        }
    }

    public void OnTakeSecondary(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("OnTakeSecondary");
            ChangeWeaponEvent?.Invoke(WeaponTypes.SecondaryWeapon);
        }
    }

    public void OnTakeMelee(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("OnTakeMelee");
            ChangeWeaponEvent?.Invoke(WeaponTypes.Melee);
        }
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ReloadEvent?.Invoke();
        }
    }

    // Character Actions
    public void OnAbility1(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Ability1Event?.Invoke();
        }
    }

    public void OnAbility2(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Ability2Event?.Invoke();
        }
    }

    public void OnAbility3(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Ability3Event?.Invoke();
        }
    }
}