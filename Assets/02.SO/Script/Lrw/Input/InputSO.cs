using System;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Lrw_Input
{
    [CreateAssetMenu(fileName = "InputSO", menuName = "Input/InputSO")]
    public class InputSO : ScriptableObject, InputSystem_Actions.IPlayerActions
    {
        public Vector2 MousePos { get; private set; }
        public Vector2 MoveDir { get; private set; }
        public event Action OnJumpKeyPress;
        public event Action OnAttackKeyPress;

        private InputSystem_Actions _inputSystem_Actions;

        private void OnEnable()
        {
            if (_inputSystem_Actions == null)
            {
                _inputSystem_Actions = new InputSystem_Actions();
                _inputSystem_Actions.Player.SetCallbacks(this);
            }
            _inputSystem_Actions.Player.Enable();
        }
        
        private void OnDisable()
        {
            _inputSystem_Actions.Player.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveDir = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnJumpKeyPress?.Invoke();
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnAttackKeyPress?.Invoke();
            }
        }

        public void OnMouse(InputAction.CallbackContext context)
        {
            MousePos = context.ReadValue<Vector2>();
        }

    }
}

