using System;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Lrw_Input
{
    [CreateAssetMenu(fileName = "InputSO", menuName = "Input/InputSO")]
    public class InputSO : ScriptableObject, Controler.IPlayerActions
    {
        public Vector2 MousePos { get; private set; }
        public Vector2 MouseScreenPos { get; private set; }
        public Vector2 MoveDir { get; private set; }
        public event Action OnJumpKeyPress;
        public event Action OnMousePress;
        public event Action OnMouseReleas;
        private Controler _controler;
        public bool MouseClick { get; private set; } = false;
        private void OnEnable()
        {
            if (_controler == null)
            {
                _controler = new Controler();
                _controler.Player.SetCallbacks(this);
            }
            _controler.Player.Enable();
        }
        
        private void OnDisable()
        {
            _controler.Player.Disable();
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
                OnMousePress?.Invoke();
                MouseClick = true;
            }
            if (context.canceled)
            {
                OnMouseReleas?.Invoke();
                MouseClick = false;
            }
        }

        public void OnMouse(InputAction.CallbackContext context)
        {
            MouseScreenPos = context.ReadValue<Vector2>();
            MousePos = Camera.main.ScreenToWorldPoint(MouseScreenPos);
        }

    }
}

